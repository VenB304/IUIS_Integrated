using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IUIS.Modules.Team8.Models
{
    public class Enrollment : IEntity
    {
        public string EnrollmentID { get; set; } = string.Empty;
        public string StudentID { get; set; } = string.Empty;
        public string SchoolYear { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty; // "1st Semester" | "2nd Semester" | "Summer"
        public string CourseID { get; set; } = string.Empty;
        public string YearLevel { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public List<Subject> EnrolledSubjects { get; set; } = new List<Subject>();
        public int TotalUnits { get; set; }
        public TuitionAssessment TuitionFee { get; set; } = new TuitionAssessment();
        public string EnrollmentStatus { get; set; } = "Pending"; // "Enrolled" | "Pending" | "Dropped"
        public DateTime DateEnrolled { get; set; } = DateTime.Now;

        [JsonIgnore]
        public string Id
        {
            get => EnrollmentID;
            set => EnrollmentID = value;
        }
    }
}
