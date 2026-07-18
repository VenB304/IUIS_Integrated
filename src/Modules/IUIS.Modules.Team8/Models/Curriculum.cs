using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class Curriculum : IEntity
    {
        public string CurriculumID { get; set; } = string.Empty;
        public string ProgramId { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public int YearLevel { get; set; }
        public string Semester { get; set; } = string.Empty;

        [JsonIgnore]
        public string Id
        {
            get => CurriculumID;
            set => CurriculumID = value;
        }
    }
}
