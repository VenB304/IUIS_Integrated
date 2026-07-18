using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class Subject : IEntity
    {
        public string SubjectID { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public int Units { get; set; }
        public string CourseID { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public string SchoolHours { get; set; } = string.Empty;

        [JsonIgnore]
        public string Id
        {
            get => SubjectID;
            set => SubjectID = value;
        }

        [JsonIgnore]
        public string DisplayText => $"{SubjectID} - {SubjectName} ({Units} Units | {Schedule} {SchoolHours} | Inst: {Instructor})";
    }
}
