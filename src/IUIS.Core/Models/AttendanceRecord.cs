using Newtonsoft.Json;

namespace IUIS.Core.Models
{
    /// <summary>
    /// One employee's attendance for one day. Referenced by clearance and
    /// payroll-style reporting in other modules.
    /// </summary>
    public class AttendanceRecord
    {
        /// <summary>Primary key, formatted "ATT-0001". Held in memory, not in the node body.</summary>
        [JsonIgnore]
        public string RecordId { get; set; } = string.Empty;

        /// <summary>Foreign key to <see cref="Employee.EmployeeId"/>.</summary>
        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; } = string.Empty;

        /// <summary>Resolved from the employee list for display. Never persisted —
        /// storing it would duplicate data the employees node already owns.</summary>
        [JsonIgnore]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>Stored as "yyyy-MM-dd".</summary>
        [JsonProperty("date")]
        public string Date { get; set; } = string.Empty;

        /// <summary>Stored as "HH:mm:ss" or "hh:mm tt". Empty if absent/on-leave.</summary>
        [JsonProperty("timeIn")]
        public string TimeIn { get; set; } = string.Empty;

        /// <summary>Stored as "HH:mm:ss" or "hh:mm tt". Empty until recorded.</summary>
        [JsonProperty("timeOut")]
        public string TimeOut { get; set; } = string.Empty;

        [JsonProperty("hoursRendered")]
        public double TotalHours { get; set; }

        /// <summary>One of <see cref="AttendanceStatus"/>.</summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("remarks")]
        public string Remarks { get; set; } = string.Empty;
    }

    /// <summary>The permitted values for <see cref="AttendanceRecord.Status"/>.</summary>
    public static class AttendanceStatus
    {
        public const string Present = "Present";
        public const string Late    = "Late";
        public const string Absent  = "Absent";
        public const string OnLeave = "On Leave";

        public static readonly string[] All = [Present, Late, Absent, OnLeave];
    }
}
