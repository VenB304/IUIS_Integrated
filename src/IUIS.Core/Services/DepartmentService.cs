using IUIS.Core.Data;
using IUIS.Core.Models;

namespace IUIS.Core.Services
{
    /// <summary>
    /// CRUD for the shared "departments" node.
    /// </summary>
    public class DepartmentService : FirebaseRepository<Department>
    {
        protected override string Node     => "departments";
        protected override string IdPrefix => "DEP";

        protected override string GetId(Department entity)          => entity.DepartmentId;
        protected override void   SetId(Department entity, string id) => entity.DepartmentId = id;

        /// <summary>Returns all departments sorted by name, ready for display.</summary>
        public override async Task<List<Department>> GetAllAsync()
        {
            var list = await base.GetAllAsync();
            return list.OrderBy(d => d.Name).ToList();
        }
    }
}
