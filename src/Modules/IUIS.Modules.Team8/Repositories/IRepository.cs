using System.Collections.Generic;

namespace IUIS.Modules.Team8.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> LoadAll();
        void SaveAll(List<T> items);
        void Add(T item);
        void Update(T item);
        void Delete(string id);
        void ClearCache();
    }
}
