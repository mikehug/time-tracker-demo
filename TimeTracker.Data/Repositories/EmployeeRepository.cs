using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data.Models;

namespace TimeTracker.Data.Repositories
{
    public class EmployeeRepository
    {
        private readonly TimeTrackerDbContext _context;

        public EmployeeRepository(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<Employee>> GetActiveEmployeesAsync()
        {
            return await _context.Employees
                .Where(e => e.IsActive)
                .ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.TimeEntries)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Email.ToLower() == email.ToLower());
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return false;

            employee.IsActive = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Employee>> SearchEmployeesAsync(string searchTerm)
        {
            return await _context.Employees
                .Where(e => 
                    e.FirstName.Contains(searchTerm) || 
                    e.LastName.Contains(searchTerm) || 
                    e.Email.Contains(searchTerm) || 
                    e.EmployeeNumber.Contains(searchTerm) ||
                    e.Department!.Contains(searchTerm) || 
                    e.Position!.Contains(searchTerm))
                .ToListAsync();
        }
    }
} 