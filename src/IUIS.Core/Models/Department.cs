using Newtonsoft.Json;

namespace IUIS.Core.Models
{
    /// <summary>
    /// A university department. Referenced by <see cref="Employee.DepartmentId"/>.
    /// </summary>
    public class Department
    {
        /// <summary>Primary key, formatted "DEP-0001". Held in memory, not in the node body.</summary>
        [JsonIgnore]
        public string DepartmentId { get; set; } = string.Empty;

        [JsonProperty("departmentName")]
        public string Name { get; set; } = string.Empty;

        /// <summary>Foreign key to <see cref="Employee.EmployeeId"/>. Blank if unassigned.</summary>
        [JsonProperty("departmentHeadId")]
        public string DepartmentHeadId { get; set; } = string.Empty;

        [JsonProperty("location")]
        public string Location { get; set; } = string.Empty;

        public override string ToString() => Name;
    }
}
