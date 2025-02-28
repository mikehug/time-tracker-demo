using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Data.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime EffectiveDate { get; set; } = DateTime.Now;

        public DateTime? ExpirationDate { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        // Navigation property
        public virtual Employee Employee { get; set; } = null!;
    }
} 