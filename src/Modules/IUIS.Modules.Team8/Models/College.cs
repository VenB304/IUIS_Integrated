using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class College : IEntity
    {
        public string CollegeId { get; set; } = string.Empty;
        public string CollegeName { get; set; } = string.Empty;

        [JsonIgnore]
        public string Id
        {
            get => CollegeId;
            set => CollegeId = value;
        }
    }
}
