using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class AcademicProgram : IEntity
    {
        public string ProgramId { get; set; } = string.Empty;
        public string ProgramName { get; set; } = string.Empty;
        public string CollegeId { get; set; } = string.Empty;

        [JsonIgnore]
        public string Id
        {
            get => ProgramId;
            set => ProgramId = value;
        }
    }
}
