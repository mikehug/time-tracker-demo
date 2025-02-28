using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data.Models;

namespace TimeTracker.Data.Repositories
{
    public class ScheduleRepository
    {
        private readonly TimeTrackerDbContext _context;

        public ScheduleRepository(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Schedules
                .Include(s => s.Employee)
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetSchedulesByEmployeeIdAsync(int employeeId)
        {
            return await _context.Schedules
                .Where(s => s.EmployeeId == employeeId && s.IsActive)
                .OrderBy(s => s.DayOfWeek)
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetSchedulesForDayOfWeekAsync(DayOfWeek dayOfWeek)
        {
            return await _context.Schedules
                .Include(s => s.Employee)
                .Where(s => s.DayOfWeek == dayOfWeek && s.IsActive)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<Schedule?> GetScheduleByIdAsync(int id)
        {
            return await _context.Schedules
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Schedule> AddScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<bool> UpdateScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
                return false;

            schedule.IsActive = false;
            schedule.ExpirationDate = DateTime.Now;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Schedule>> GetSchedulesForEmployeeOnDateAsync(int employeeId, DateTime date)
        {
            return await _context.Schedules
                .Where(s => s.EmployeeId == employeeId &&
                            s.DayOfWeek == date.DayOfWeek &&
                            s.IsActive &&
                            s.EffectiveDate <= date &&
                            (s.ExpirationDate == null || s.ExpirationDate >= date))
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetAllEmployeeSchedulesForWeekAsync(DateTime weekStartDate)
        {
            var weekEndDate = weekStartDate.AddDays(6);
            
            return await _context.Schedules
                .Include(s => s.Employee)
                .Where(s => s.IsActive &&
                            s.EffectiveDate <= weekEndDate &&
                            (s.ExpirationDate == null || s.ExpirationDate >= weekStartDate))
                .OrderBy(s => s.EmployeeId)
                .ThenBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetSchedulesInRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Schedules
                .Include(s => s.Employee)
                .Where(s => s.IsActive &&
                            s.EffectiveDate <= endDate &&
                            (s.ExpirationDate == null || s.ExpirationDate >= startDate))
                .OrderBy(s => s.EmployeeId)
                .ThenBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();
        }
    }
} 