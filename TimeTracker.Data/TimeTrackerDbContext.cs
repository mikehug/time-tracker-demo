using Microsoft.EntityFrameworkCore;
using System;
using TimeTracker.Data.Models;

namespace TimeTracker.Data
{
    public class TimeTrackerDbContext : DbContext
    {
        public TimeTrackerDbContext(DbContextOptions<TimeTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some demo data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Phone = "555-123-4567",
                    EmployeeNumber = "EMP001",
                    HireDate = new DateTime(2021, 1, 15),
                    Department = "Engineering",
                    Position = "Software Developer",
                    IsActive = true
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Phone = "555-987-6543",
                    EmployeeNumber = "EMP002",
                    HireDate = new DateTime(2020, 6, 10),
                    Department = "Marketing",
                    Position = "Marketing Specialist",
                    IsActive = true
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Johnson",
                    Email = "michael.johnson@example.com",
                    Phone = "555-456-7890",
                    EmployeeNumber = "EMP003",
                    HireDate = new DateTime(2022, 3, 5),
                    Department = "Finance",
                    Position = "Financial Analyst",
                    IsActive = true
                },
                new Employee
                {
                    Id = 4,
                    FirstName = "Emily",
                    LastName = "Williams",
                    Email = "emily.williams@example.com",
                    Phone = "555-789-0123",
                    EmployeeNumber = "EMP004",
                    HireDate = new DateTime(2021, 8, 20),
                    Department = "Human Resources",
                    Position = "HR Coordinator",
                    IsActive = true
                },
                new Employee
                {
                    Id = 5,
                    FirstName = "David",
                    LastName = "Brown",
                    Email = "david.brown@example.com",
                    Phone = "555-234-5678",
                    EmployeeNumber = "EMP005",
                    HireDate = new DateTime(2019, 11, 12),
                    Department = "Operations",
                    Position = "Operations Manager",
                    IsActive = true
                }
            );

            // Seed TimeEntries (last 7 days of entries for each employee)
            var now = DateTime.Now;
            var today = now.Date;
            var startDate = today.AddDays(-7);

            for (int day = 0; day < 7; day++)
            {
                var currentDate = startDate.AddDays(day);
                
                // Skip weekends for some employees
                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                // Create entries for each employee
                for (int empId = 1; empId <= 5; empId++)
                {
                    // Some randomization for variety
                    var isPresent = empId != day % 5; // One employee will be absent each day

                    if (isPresent)
                    {
                        var clockInTime = currentDate.AddHours(8 + (empId % 2)); // Start between 8-9 AM
                        var clockOutTime = currentDate.AddHours(17 + (empId % 3)); // End between 5-7 PM
                        
                        modelBuilder.Entity<TimeEntry>().HasData(
                            new TimeEntry
                            {
                                Id = empId * 100 + day, // Unique ID generation
                                EmployeeId = empId,
                                ClockInTime = clockInTime,
                                ClockOutTime = clockOutTime,
                                Notes = $"Regular workday for employee {empId}",
                                EntryType = EntryType.Regular,
                                IsApproved = currentDate < today, // Past entries are approved
                                ApprovedDate = currentDate < today ? currentDate.AddDays(1) : null,
                                ApprovedBy = currentDate < today ? "System" : null
                            }
                        );
                    }
                    else if (day % 7 == 5) // Create PTO entries on some days
                    {
                        modelBuilder.Entity<TimeEntry>().HasData(
                            new TimeEntry
                            {
                                Id = empId * 100 + day,
                                EmployeeId = empId,
                                ClockInTime = currentDate.AddHours(9),
                                ClockOutTime = currentDate.AddHours(17),
                                Notes = "Paid Time Off",
                                EntryType = EntryType.PaidTimeOff,
                                IsApproved = true,
                                ApprovedDate = currentDate.AddDays(-5),
                                ApprovedBy = "Manager"
                            }
                        );
                    }
                }
            }

            // Seed Schedules
            modelBuilder.Entity<Schedule>().HasData(
                // Employee 1 Schedule
                new Schedule { Id = 1, EmployeeId = 1, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 2, EmployeeId = 1, DayOfWeek = DayOfWeek.Tuesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 3, EmployeeId = 1, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 4, EmployeeId = 1, DayOfWeek = DayOfWeek.Thursday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 5, EmployeeId = 1, DayOfWeek = DayOfWeek.Friday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                
                // Employee 2 Schedule
                new Schedule { Id = 6, EmployeeId = 2, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(8, 30, 0), EndTime = new TimeSpan(16, 30, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 7, EmployeeId = 2, DayOfWeek = DayOfWeek.Tuesday, StartTime = new TimeSpan(8, 30, 0), EndTime = new TimeSpan(16, 30, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 8, EmployeeId = 2, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(8, 30, 0), EndTime = new TimeSpan(16, 30, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 9, EmployeeId = 2, DayOfWeek = DayOfWeek.Thursday, StartTime = new TimeSpan(8, 30, 0), EndTime = new TimeSpan(16, 30, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 10, EmployeeId = 2, DayOfWeek = DayOfWeek.Friday, StartTime = new TimeSpan(8, 30, 0), EndTime = new TimeSpan(16, 30, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                
                // Employee 3 Schedule (works on Saturday)
                new Schedule { Id = 11, EmployeeId = 3, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 12, EmployeeId = 3, DayOfWeek = DayOfWeek.Tuesday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 13, EmployeeId = 3, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 14, EmployeeId = 3, DayOfWeek = DayOfWeek.Thursday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(18, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 15, EmployeeId = 3, DayOfWeek = DayOfWeek.Saturday, StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(14, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                
                // Employee 4 Schedule
                new Schedule { Id = 16, EmployeeId = 4, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 17, EmployeeId = 4, DayOfWeek = DayOfWeek.Tuesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 18, EmployeeId = 4, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 19, EmployeeId = 4, DayOfWeek = DayOfWeek.Thursday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 20, EmployeeId = 4, DayOfWeek = DayOfWeek.Friday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                
                // Employee 5 Schedule (part-time)
                new Schedule { Id = 21, EmployeeId = 5, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(12, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 22, EmployeeId = 5, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(12, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) },
                new Schedule { Id = 23, EmployeeId = 5, DayOfWeek = DayOfWeek.Friday, StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(12, 0, 0), IsActive = true, EffectiveDate = new DateTime(2023, 1, 1) }
            );
        }
    }
} 