using System;
using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class EnrollmentHistory : IEntity
    {
        public string HistoryID { get; set; } = string.Empty;
        public string EnrollmentID { get; set; } = string.Empty;
        public string StudentID { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string PerformedBy { get; set; } = "admin";
        public string Remarks { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;

        [JsonIgnore]
        public string Id
        {
            get => HistoryID;
            set => HistoryID = value;
        }
    }
}
