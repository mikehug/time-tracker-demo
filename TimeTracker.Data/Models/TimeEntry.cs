using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Data.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime ClockInTime { get; set; }

        public DateTime? ClockOutTime { get; set; }

        public TimeSpan? TotalHours => ClockOutTime.HasValue ? ClockOutTime.Value - ClockInTime : null;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public EntryType EntryType { get; set; } = EntryType.Regular;

        [MaxLength(100)]
        public string? Project { get; set; }

        [MaxLength(100)]
        public string? Task { get; set; }

        public bool IsApproved { get; set; }

        public DateTime? ApprovedDate { get; set; }

        [MaxLength(50)]
        public string? ApprovedBy { get; set; }

        // Navigation property
        public virtual Employee Employee { get; set; } = null!;
    }

    public enum EntryType
    {
        Regular = 0,
        Overtime = 1,
        PaidTimeOff = 2,
        SickLeave = 3,
        Holiday = 4
    }
} 