using IUIS.Core.Data;
using IUIS.Core.Models;

namespace IUIS.Core.Services
{
    /// <summary>
    /// CRUD plus date/employee lookups for the shared "attendance" node.
    /// </summary>
    public class AttendanceService : FirebaseRepository<AttendanceRecord>
    {
        protected override string Node     => "attendance";
        protected override string IdPrefix => "ATT";

        protected override string GetId(AttendanceRecord entity)          => entity.RecordId;
        protected override void   SetId(AttendanceRecord entity, string id) => entity.RecordId = id;

        /// <summary>Returns every record logged on the given date ("yyyy-MM-dd").</summary>
        public async Task<List<AttendanceRecord>> GetByDateAsync(string date)
        {
            var all = await GetAllAsync();
            return all.Where(r => r.Date == date).ToList();
        }

        /// <summary>
        /// Returns this employee's record for the given date, or null if none exists.
        /// Used to stop a second Time In being logged for the same day.
        /// </summary>
        public async Task<AttendanceRecord?> GetByEmployeeAndDateAsync(string employeeId, string date)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(r => r.EmployeeId == employeeId && r.Date == date);
        }

        /// <summary>Returns every record for one employee, newest first.</summary>
        public async Task<List<AttendanceRecord>> GetByEmployeeAsync(string employeeId)
        {
            var all = await GetAllAsync();

            return all
                .Where(r => r.EmployeeId == employeeId)
                .OrderByDescending(r => r.Date)
                .ToList();
        }

        /// <summary>Returns records between two dates inclusive ("yyyy-MM-dd").</summary>
        /// <remarks>
        /// String comparison is safe here only because the format is zero-padded
        /// ISO order, where lexical and chronological sorting agree.
        /// </remarks>
        public async Task<List<AttendanceRecord>> GetByDateRangeAsync(string fromDate, string toDate)
        {
            var all = await GetAllAsync();

            return all
                .Where(r => string.CompareOrdinal(r.Date, fromDate) >= 0 &&
                            string.CompareOrdinal(r.Date, toDate)   <= 0)
                .OrderBy(r => r.Date)
                .ToList();
        }
    }
}
