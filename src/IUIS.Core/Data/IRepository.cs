namespace IUIS.Core.Data
{
    /// <summary>
    /// The storage contract every entity service exposes.
    ///
    /// Modules depend on this interface rather than on a concrete Firebase class,
    /// so the backing store can be swapped without touching a single form.
    /// </summary>
    /// <typeparam name="T">The model type being persisted.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>Returns every record in the node.</summary>
        Task<List<T>> GetAllAsync();

        /// <summary>Returns the record with this primary key, or null if absent.</summary>
        Task<T?> GetByIdAsync(string id);

        /// <summary>Persists a new record and returns its generated primary key.</summary>
        Task<string> AddAsync(T entity);

        /// <summary>Overwrites the stored record with this entity's current state.</summary>
        Task UpdateAsync(T entity);

        /// <summary>Removes the record with this primary key.</summary>
        Task DeleteAsync(string id);
    }
}
