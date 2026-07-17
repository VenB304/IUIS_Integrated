using Newtonsoft.Json;

namespace IUIS.Core.Models
{
    /// <summary>
    /// A non-teaching employee. Owned by Team 6, but shared: any module that
    /// needs staff details (department heads, faculty assignment, clearances)
    /// reads this same node rather than storing its own copy.
    /// </summary>
    public class Employee
    {
        /// <summary>Primary key, formatted "EMP-0001". Held in memory, not in the node body.</summary>
        [JsonIgnore]
        public string EmployeeId { get; set; } = string.Empty;

        /// <summary>Foreign key to <see cref="Department.DepartmentId"/>.</summary>
        [JsonProperty("departmentId")]
        public string DepartmentId { get; set; } = string.Empty;

        [JsonProperty("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("middleName")]
        public string MiddleName { get; set; } = string.Empty;

        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonProperty("position")]
        public string Position { get; set; } = string.Empty;

        [JsonProperty("sex")]
        public string Sex { get; set; } = string.Empty;

        [JsonProperty("birthDate")]
        public string BirthDate { get; set; } = string.Empty;

        [JsonProperty("contactNumber")]
        public string ContactNumber { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("hourlyRate")]
        public double HourlyRate { get; set; }

        /// <summary>Stored as "yyyy-MM-dd".</summary>
        [JsonProperty("dateHired")]
        public string DateHired { get; set; } = string.Empty;

        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;

        [JsonProperty("employmentStatus")]
        public string EmploymentStatus { get; set; } = "Active";

        /// <summary>Display name as "Last, First M." — derived, never persisted.</summary>
        [JsonIgnore]
        public string FullName =>
            string.IsNullOrWhiteSpace(LastName) && string.IsNullOrWhiteSpace(FirstName)
                ? "(No Name)"
                : $"{LastName.Trim()}, {FirstName.Trim()}{(string.IsNullOrWhiteSpace(MiddleName) ? "" : " " + MiddleName.Trim().Substring(0, 1) + ".")}".Trim().TrimEnd(',').Trim();

        public override string ToString() => FullName;
    }
}
