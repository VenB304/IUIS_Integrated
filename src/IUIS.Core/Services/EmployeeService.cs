using IUIS.Core.Data;
using IUIS.Core.Models;

namespace IUIS.Core.Services
{
    /// <summary>
    /// CRUD for the shared "employees" node.
    ///
    /// Lives in Core rather than in Team 6's module because other modules need to
    /// read staff records — that sharing is the point of the integrated system.
    /// </summary>
    public class EmployeeService : FirebaseRepository<Employee>
    {
        protected override string Node     => "employees";
        protected override string IdPrefix => "EMP";

        protected override string GetId(Employee entity)          => entity.EmployeeId;
        protected override void   SetId(Employee entity, string id) => entity.EmployeeId = id;

        /// <summary>Returns all employees sorted by name, ready for display.</summary>
        public override async Task<List<Employee>> GetAllAsync()
        {
            var list = await base.GetAllAsync();

            return list
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToList();
        }

        /// <summary>Returns only employees whose employment status is Active.</summary>
        public async Task<List<Employee>> GetActiveAsync()
        {
            var list = await GetAllAsync();

            return list
                .Where(e => e.EmploymentStatus.Equals("Active", StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>Returns every employee assigned to the given department.</summary>
        public async Task<List<Employee>> GetByDepartmentAsync(string departmentId)
        {
            var list = await GetAllAsync();
            return list.Where(e => e.DepartmentId == departmentId).ToList();
        }
    }
}
