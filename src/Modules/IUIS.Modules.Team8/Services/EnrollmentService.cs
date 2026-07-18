using System;
using System.Collections.Generic;
using System.Linq;
using IUIS.Modules.Team8.Models;
using IUIS.Modules.Team8.Repositories;

namespace IUIS.Modules.Team8.Services
{
    public class EnrollmentService
    {
        private readonly IRepository<Student> _studentRepo;
        private readonly IRepository<Enrollment> _enrollmentRepo;
        private readonly IRepository<EnrollmentHistory> _historyRepo;
        private readonly IRepository<College> _collegeRepo;
        private readonly IRepository<AcademicProgram> _programRepo;
        private readonly IRepository<Course> _courseRepo;
        private readonly IRepository<Curriculum> _curriculumRepo;
        private readonly IRepository<AcademicOffering> _offeringRepo;
        private readonly IRepository<AcademicRecord> _academicRecordRepo;

        public EnrollmentService()
        {
            _studentRepo = RepositoryFactory.Create<Student>("students.json", "students");
            _enrollmentRepo = RepositoryFactory.Create<Enrollment>("enrollment.json", "enrollments");
            _historyRepo = RepositoryFactory.Create<EnrollmentHistory>("history.json", "history");
            _collegeRepo = RepositoryFactory.Create<College>("colleges.json", "academicManagement/colleges");
            _programRepo = RepositoryFactory.Create<AcademicProgram>("programs.json", "academicManagement/programs");
            _courseRepo = RepositoryFactory.Create<Course>("courses.json", "academicManagement/courses");
            _curriculumRepo = RepositoryFactory.Create<Curriculum>("curriculum.json", "academicManagement/curriculum");
            _offeringRepo = RepositoryFactory.Create<AcademicOffering>("academicofferings.json", "academicManagement/academicofferings");
            _academicRecordRepo = RepositoryFactory.Create<AcademicRecord>("academic_records.json", "academic_records");

            // Automated migration of local students.json file if needed
            if (AppSettings.DbMode.Equals("Json", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var students = _studentRepo.LoadAll();
                    bool needsMigration = false;
                    foreach (var s in students)
                    {
                        if (s.CollegeId == "CICS" || s.ProgramId == "BSCS" || s.ProgramId == "BSIT" || s.ProgramId == "BSCPE")
                        {
                            s.Normalize();
                            needsMigration = true;
                        }
                    }
                    if (needsMigration)
                    {
                        _studentRepo.SaveAll(students);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Auto-migration of students.json failed: {ex.Message}");
                }
            }
        }

        public void ClearCache()
        {
            _studentRepo.ClearCache();
            _enrollmentRepo.ClearCache();
            _historyRepo.ClearCache();
            _collegeRepo.ClearCache();
            _programRepo.ClearCache();
            _courseRepo.ClearCache();
            _curriculumRepo.ClearCache();
            _offeringRepo.ClearCache();
            _academicRecordRepo.ClearCache();
        }

        // Student operations
        public List<Student> GetAllStudents()
        {
            var list = _studentRepo.LoadAll();
            foreach (var student in list)
            {
                student.Normalize();
            }
            return list;
        }

        public Student? GetStudentById(string studentId)
        {
            return GetAllStudents().FirstOrDefault(s => s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase));
        }

        // Subject operations
        public List<Subject> GetAllSubjects()
        {
            var courses = _courseRepo.LoadAll();
            var subjects = new List<Subject>();
            foreach (var course in courses)
            {
                subjects.Add(new Subject
                {
                    SubjectID = course.SubjectCode,
                    SubjectName = course.SubjectDescription,
                    Units = course.Units,
                    CourseID = "",
                    Schedule = GetMockSchedule(course.SubjectCode),
                    SchoolHours = GetMockSchoolHours(course.SubjectCode),
                    Instructor = GetMockInstructor(course.SubjectCode)
                });
            }
            return subjects;
        }

        public List<Subject> GetSubjectsByCourse(string programId, string schoolYear, string semester)
        {
            string mappedSemester = semester;
            if (semester.Equals("1st Semester", StringComparison.OrdinalIgnoreCase)) mappedSemester = "1st";
            else if (semester.Equals("2nd Semester", StringComparison.OrdinalIgnoreCase)) mappedSemester = "2nd";

            // 1. Load curriculum for this program
            var curriculumList = _curriculumRepo.LoadAll()
                .Where(c => c.ProgramId.Equals(programId, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // 2. Load open academic offerings for this term
            var offerings = _offeringRepo.LoadAll()
                .Where(o => o.SchoolYear.Equals(schoolYear, StringComparison.OrdinalIgnoreCase) &&
                            o.Semester.Equals(mappedSemester, StringComparison.OrdinalIgnoreCase) &&
                            o.Status.Equals("Open", StringComparison.OrdinalIgnoreCase))
                .ToList();

            // 3. Filter curriculum by offerings (fallback to all curriculum subjects if no offerings match)
            List<Curriculum> filteredCurriculum;
            if (offerings.Any())
            {
                var offeredCodes = offerings.Select(o => o.SubjectCode).ToHashSet(StringComparer.OrdinalIgnoreCase);
                filteredCurriculum = curriculumList.Where(c => offeredCodes.Contains(c.SubjectCode)).ToList();
            }
            else
            {
                filteredCurriculum = curriculumList;
            }

            // 4. Map courses to Subject model
            var courses = _courseRepo.LoadAll().ToDictionary(c => c.SubjectCode, StringComparer.OrdinalIgnoreCase);
            var subjects = new List<Subject>();

            foreach (var cur in filteredCurriculum)
            {
                if (courses.TryGetValue(cur.SubjectCode, out var course))
                {
                    subjects.Add(new Subject
                    {
                        SubjectID = course.SubjectCode,
                        SubjectName = course.SubjectDescription,
                        Units = course.Units,
                        CourseID = cur.ProgramId,
                        Schedule = GetMockSchedule(course.SubjectCode),
                        SchoolHours = GetMockSchoolHours(course.SubjectCode),
                        Instructor = GetMockInstructor(course.SubjectCode)
                    });
                }
            }

            return subjects;
        }

        private string GetMockSchedule(string code)
        {
            string cleanCode = code.Replace("-", "").ToUpper();
            if (cleanCode == "CS101" || cleanCode == "IT101") return "M/W";
            if (cleanCode == "CS201" || cleanCode == "IT302") return "T/TH";
            if (cleanCode == "CPE401") return "Fri";
            return "M/W";
        }

        private string GetMockSchoolHours(string code)
        {
            string cleanCode = code.Replace("-", "").ToUpper();
            if (cleanCode == "CS101") return "09:00 AM - 10:30 AM";
            if (cleanCode == "CS201") return "01:00 PM - 03:00 PM";
            if (cleanCode == "IT101") return "10:30 AM - 12:00 PM";
            if (cleanCode == "IT302") return "03:00 PM - 04:30 PM";
            if (cleanCode == "CPE401") return "09:00 AM - 01:00 PM";
            return "09:00 AM - 10:30 AM";
        }

        private string GetMockInstructor(string code)
        {
            string cleanCode = code.Replace("-", "").ToUpper();
            if (cleanCode == "CS101") return "Dr. Joselito M. Santos";
            if (cleanCode == "CS201") return "Prof. Maria Clara Reyes";
            if (cleanCode == "IT101") return "Engr. Richard P. Perez";
            if (cleanCode == "IT302") return "Prof. Angela S. Lim";
            if (cleanCode == "CPE401") return "Dr. Fernando C. Villanueva";
            return "Staff";
        }

        // Enrollment operations
        public List<Enrollment> GetAllEnrollments()
        {
            return _enrollmentRepo.LoadAll();
        }

        public Enrollment? GetEnrollmentById(string enrollmentId)
        {
            return _enrollmentRepo.LoadAll().FirstOrDefault(e => e.EnrollmentID.Equals(enrollmentId, StringComparison.OrdinalIgnoreCase));
        }

        public List<Enrollment> GetEnrollmentsByStudent(string studentId)
        {
            return _enrollmentRepo.LoadAll()
                .Where(e => e.StudentID.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Auto-generate ID: ENR-YYYY-XXXX
        public string GenerateEnrollmentId(string schoolYear)
        {
            string yearPart = "2026";
            if (!string.IsNullOrWhiteSpace(schoolYear))
            {
                var parts = schoolYear.Split('-');
                if (parts.Length > 0 && parts[0].Length == 4 && int.TryParse(parts[0], out _))
                {
                    yearPart = parts[0];
                }
            }

            var enrollments = _enrollmentRepo.LoadAll();
            string prefix = $"ENR-{yearPart}-";

            var matchingIds = enrollments
                .Select(e => e.EnrollmentID)
                .Where(id => id.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToList();

            int maxSeq = 0;
            foreach (var id in matchingIds)
            {
                string seqPart = id.Substring(prefix.Length);
                if (int.TryParse(seqPart, out int seq))
                {
                    if (seq > maxSeq)
                    {
                        maxSeq = seq;
                    }
                }
            }

            int nextSeq = maxSeq + 1;
            return $"{prefix}{nextSeq.ToString("D4")}";
        }

        public void SaveEnrollment(Enrollment enrollment)
        {
            // 1. Recalculate units and tuition fee assessment fields
            enrollment.TotalUnits = enrollment.EnrolledSubjects.Sum(s => s.Units);
            if (enrollment.TuitionFee == null)
            {
                enrollment.TuitionFee = new TuitionAssessment();
            }
            enrollment.TuitionFee.CalculateTotal(enrollment.TotalUnits);

            if (enrollment.EnrollmentStatus != null && enrollment.EnrollmentStatus.Equals("Voided", StringComparison.OrdinalIgnoreCase))
            {
                enrollment.TuitionFee.Balance = 0;
            }

            // 2. Validation
            ValidateEnrollment(enrollment);

            // 3. Save
            var existing = GetEnrollmentById(enrollment.EnrollmentID);
            if (existing != null)
            {
                _enrollmentRepo.Update(enrollment);
            }
            else
            {
                _enrollmentRepo.Add(enrollment);
            }
        }

        public void DeleteEnrollment(string enrollmentId)
        {
            _enrollmentRepo.Delete(enrollmentId);
        }

        private void ValidateEnrollment(Enrollment enrollment)
        {
            if (string.IsNullOrWhiteSpace(enrollment.StudentID))
            {
                throw new ArgumentException("Student ID must be specified.");
            }
            var student = GetStudentById(enrollment.StudentID);
            if (student == null)
            {
                throw new ArgumentException($"Student with ID '{enrollment.StudentID}' does not exist in the database.");
            }

            if (string.IsNullOrWhiteSpace(enrollment.SchoolYear))
            {
                throw new ArgumentException("School Year must be specified.");
            }
            if (string.IsNullOrWhiteSpace(enrollment.Semester))
            {
                throw new ArgumentException("Semester must be specified.");
            }

            if (enrollment.EnrolledSubjects == null || enrollment.EnrolledSubjects.Count == 0)
            {
                throw new ArgumentException("At least one subject must be selected for enrollment.");
            }

            var allEnrollments = _enrollmentRepo.LoadAll();
            var duplicate = allEnrollments.FirstOrDefault(e =>
                e.StudentID.Equals(enrollment.StudentID, StringComparison.OrdinalIgnoreCase) &&
                e.SchoolYear.Equals(enrollment.SchoolYear, StringComparison.OrdinalIgnoreCase) &&
                e.Semester.Equals(enrollment.Semester, StringComparison.OrdinalIgnoreCase) &&
                !e.EnrollmentID.Equals(enrollment.EnrollmentID, StringComparison.OrdinalIgnoreCase)
            );

            if (duplicate != null)
            {
                throw new InvalidOperationException($"Student is already enrolled for School Year '{enrollment.SchoolYear}' and Semester '{enrollment.Semester}' under Enrollment ID '{duplicate.EnrollmentID}'.");
            }
        }

        // History operations
        public List<EnrollmentHistory> GetAllHistory()
        {
            return _historyRepo.LoadAll();
        }

        public void SaveHistory(EnrollmentHistory history)
        {
            if (string.IsNullOrEmpty(history.HistoryID))
            {
                var rand = new Random();
                history.HistoryID = $"HST-{DateTime.Now:yyyyMMdd}-{rand.Next(1000, 9999)}";
            }
            _historyRepo.Add(history);
        }

        // Prerequisite validation operations
        private string CleanCode(string code)
        {
            return (code ?? string.Empty).Replace("-", "").Replace(" ", "").ToUpperInvariant();
        }

        public bool ValidateSubjectPrerequisite(string studentId, string subjectId, out string? errorMessage)
        {
            errorMessage = null;

            // 1. Find the course information to identify prerequisites
            var courses = _courseRepo.LoadAll().ToDictionary(c => CleanCode(c.SubjectCode), StringComparer.OrdinalIgnoreCase);
            string cleanSubject = CleanCode(subjectId);

            if (!courses.TryGetValue(cleanSubject, out var course) || course.Prerequisites == null || !course.Prerequisites.Any())
            {
                return true; // No prerequisites required
            }

            // 2. Load student passed records
            var passedSubjects = _academicRecordRepo.LoadAll()
                .Where(r => r.StudentID.Equals(studentId, StringComparison.OrdinalIgnoreCase) &&
                            r.Status.Equals("Passed", StringComparison.OrdinalIgnoreCase))
                .Select(r => CleanCode(r.SubjectCode))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var missing = new List<string>();
            foreach (var prereq in course.Prerequisites)
            {
                string cleanPrereq = CleanCode(prereq);
                if (!passedSubjects.Contains(cleanPrereq))
                {
                    missing.Add(prereq); // Add original representation for display
                }
            }

            if (missing.Any())
            {
                errorMessage = $"Missing prerequisite(s) for {subjectId}: {string.Join(", ", missing)}";
                return false;
            }

            return true;
        }

        public bool ValidatePrerequisites(string studentId, List<string> subjectIds, out List<string> errors)
        {
            errors = new List<string>();

            if (subjectIds == null || !subjectIds.Any())
            {
                return true;
            }

            foreach (var subjectId in subjectIds)
            {
                if (!ValidateSubjectPrerequisite(studentId, subjectId, out var error))
                {
                    if (error != null)
                    {
                        errors.Add(error);
                    }
                }
            }

            return errors.Count == 0;
        }
    }
}
