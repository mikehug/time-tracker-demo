using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data.Models;

namespace TimeTracker.Data.Repositories
{
    public class TimeEntryRepository
    {
        private readonly TimeTrackerDbContext _context;

        public TimeEntryRepository(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeEntry>> GetAllTimeEntriesAsync()
        {
            return await _context.TimeEntries
                .Include(t => t.Employee)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesByEmployeeIdAsync(int employeeId)
        {
            return await _context.TimeEntries
                .Where(t => t.EmployeeId == employeeId)
                .OrderByDescending(t => t.ClockInTime)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesForDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TimeEntries
                .Include(t => t.Employee)
                .Where(t => t.ClockInTime.Date >= startDate.Date && t.ClockInTime.Date <= endDate.Date)
                .OrderByDescending(t => t.ClockInTime)
                .ToListAsync();
        }

        public async Task<List<TimeEntry>> GetTimeEntriesForEmployeeInDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _context.TimeEntries
                .Where(t => t.EmployeeId == employeeId && 
                            t.ClockInTime.Date >= startDate.Date && 
                            t.ClockInTime.Date <= endDate.Date)
                .OrderByDescending(t => t.ClockInTime)
                .ToListAsync();
        }

        public async Task<TimeEntry?> GetTimeEntryByIdAsync(int id)
        {
            return await _context.TimeEntries
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TimeEntry?> GetActiveTimeEntryForEmployeeAsync(int employeeId)
        {
            return await _context.TimeEntries
                .Where(t => t.EmployeeId == employeeId && t.ClockOutTime == null)
                .OrderByDescending(t => t.ClockInTime)
                .FirstOrDefaultAsync();
        }

        public async Task<TimeEntry> AddTimeEntryAsync(TimeEntry timeEntry)
        {
            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();
            return timeEntry;
        }

        public async Task<bool> UpdateTimeEntryAsync(TimeEntry timeEntry)
        {
            _context.TimeEntries.Update(timeEntry);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTimeEntryAsync(int id)
        {
            var timeEntry = await _context.TimeEntries.FindAsync(id);
            if (timeEntry == null)
                return false;

            _context.TimeEntries.Remove(timeEntry);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<TimeEntry>> GetUnapprovedTimeEntriesAsync()
        {
            return await _context.TimeEntries
                .Include(t => t.Employee)
                .Where(t => !t.IsApproved && t.ClockOutTime != null)
                .OrderByDescending(t => t.ClockInTime)
                .ToListAsync();
        }

        public async Task<bool> ApproveTimeEntryAsync(int id, string approvedBy)
        {
            var timeEntry = await _context.TimeEntries.FindAsync(id);
            if (timeEntry == null)
                return false;

            timeEntry.IsApproved = true;
            timeEntry.ApprovedDate = DateTime.Now;
            timeEntry.ApprovedBy = approvedBy;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ClockOutAsync(int id)
        {
            var timeEntry = await _context.TimeEntries.FindAsync(id);
            if (timeEntry == null || timeEntry.ClockOutTime != null)
                return false;

            timeEntry.ClockOutTime = DateTime.Now;
            return await _context.SaveChangesAsync() > 0;
        }

        // Alias for GetTimeEntriesForDateRangeAsync to match method name used in components
        public async Task<List<TimeEntry>> GetTimeEntriesInRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await GetTimeEntriesForDateRangeAsync(startDate, endDate);
        }
    }
} 