using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class Course : IEntity
    {
        public string SubjectCode { get; set; } = string.Empty;
        public string SubjectDescription { get; set; } = string.Empty;
        public int Units { get; set; }
        public List<string> Prerequisites { get; set; } = new List<string>();

        [JsonIgnore]
        public string Id
        {
            get => SubjectCode;
            set => SubjectCode = value;
        }
    }
}
