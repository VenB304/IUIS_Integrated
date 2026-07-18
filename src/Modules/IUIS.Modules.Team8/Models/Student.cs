using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class BirthDateInfo
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
    }

    public class AddressInfo
    {
        public string AddressLine { get; set; } = string.Empty;
        public string Barangay { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }

    public class SectionDetail
    {
        public string SectionId { get; set; } = string.Empty;
        public int YearLevel { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }

    public class SectionContainer
    {
        public SectionDetail Section { get; set; } = new SectionDetail();
    }

    public class SectionsConverter : JsonConverter<List<SectionContainer>>
    {
        public override List<SectionContainer> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new List<SectionContainer>();
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                return list;
            }

            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                foreach (var element in doc.RootElement.EnumerateArray())
                {
                    // Check if it is the new format: {"Section": {"SectionId": "...", "YearLevel": 1, ...}}
                    if (element.TryGetProperty("Section", out JsonElement secElement) && secElement.ValueKind == JsonValueKind.Object)
                    {
                        var container = JsonSerializer.Deserialize<SectionContainer>(element.GetRawText(), options);
                        if (container != null)
                        {
                            list.Add(container);
                        }
                    }
                    else
                    {
                        // It is the old format: {"Section": "IT2102", "StartYear": "2026", "EndYear": "2027"}
                        string sectionId = "";
                        if (element.TryGetProperty("Section", out JsonElement sId))
                        {
                            sectionId = sId.GetString() ?? "";
                        }

                        int startYear = 0;
                        if (element.TryGetProperty("StartYear", out JsonElement sYear))
                        {
                            if (sYear.ValueKind == JsonValueKind.Number)
                            {
                                startYear = sYear.GetInt32();
                            }
                            else if (sYear.ValueKind == JsonValueKind.String && int.TryParse(sYear.GetString(), out int sy))
                            {
                                startYear = sy;
                            }
                        }

                        int endYear = 0;
                        if (element.TryGetProperty("EndYear", out JsonElement eYear))
                        {
                            if (eYear.ValueKind == JsonValueKind.Number)
                            {
                                endYear = eYear.GetInt32();
                            }
                            else if (eYear.ValueKind == JsonValueKind.String && int.TryParse(eYear.GetString(), out int ey))
                            {
                                endYear = ey;
                            }
                        }

                        // Parse YearLevel from section code (e.g. IT2102 -> 2)
                        int yearLevel = 1;
                        if (!string.IsNullOrEmpty(sectionId))
                        {
                            foreach (char c in sectionId)
                            {
                                if (char.IsDigit(c))
                                {
                                    yearLevel = c - '0';
                                    break;
                                }
                            }
                        }

                        list.Add(new SectionContainer
                        {
                            Section = new SectionDetail
                            {
                                SectionId = sectionId,
                                YearLevel = yearLevel,
                                StartYear = startYear,
                                EndYear = endYear
                            }
                        });
                    }
                }
            }

            return list;
        }

        public override void Write(Utf8JsonWriter writer, List<SectionContainer> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }

    public class Student : IEntity
    {
        public string StudentId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public AddressInfo Address { get; set; } = new AddressInfo();
        public string CollegeId { get; set; } = string.Empty;
        public string ProgramId { get; set; } = string.Empty;
        public string MajorId { get; set; } = string.Empty;
        public int YearLevel { get; set; }

        [JsonConverter(typeof(SectionsConverter))]
        public List<SectionContainer> Sections { get; set; } = new List<SectionContainer>();
        public string Status { get; set; } = string.Empty;
        public DateTime DateRegistered { get; set; } = DateTime.Now;

        [JsonIgnore]
        public string Id
        {
            get => StudentId;
            set => StudentId = value;
        }

        [JsonIgnore]
        public string FullName => string.IsNullOrWhiteSpace(MiddleName) 
            ? $"{FirstName} {LastName}" 
            : $"{FirstName} {MiddleName} {LastName}";

        [JsonIgnore]
        public string DisplayText => $"{StudentId} - {FullName} ({ProgramId})";

        public void Normalize()
        {
            // Normalize CollegeId: CICS -> CLG001
            if (string.Equals(CollegeId, "CICS", StringComparison.OrdinalIgnoreCase))
            {
                CollegeId = "CLG001";
            }

            // Normalize ProgramId
            if (string.Equals(ProgramId, "BSCS", StringComparison.OrdinalIgnoreCase))
            {
                ProgramId = "PRG001";
            }
            else if (string.Equals(ProgramId, "BSIT", StringComparison.OrdinalIgnoreCase))
            {
                ProgramId = "PRG002";
            }
            else if (string.Equals(ProgramId, "BSCPE", StringComparison.OrdinalIgnoreCase))
            {
                ProgramId = "PRG003";
            }
        }
    }
}
