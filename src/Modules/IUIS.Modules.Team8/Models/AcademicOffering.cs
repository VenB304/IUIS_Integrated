using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class AcademicOffering : IEntity
    {
        public string OfferingID { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public string SchoolYear { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        [JsonIgnore]
        public string Id
        {
            get => OfferingID;
            set => OfferingID = value;
        }
    }
}
