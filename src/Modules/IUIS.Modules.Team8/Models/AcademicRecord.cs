using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class AcademicRecord : IEntity
    {
        public string RecordID { get; set; } = string.Empty;
        public string StudentID { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // e.g. "Passed", "Failed"

        [JsonIgnore]
        public string Id
        {
            get => RecordID;
            set => RecordID = value;
        }
    }
}
