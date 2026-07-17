using Firebase.Database.Query;

namespace IUIS.Core.Data
{
    /// <summary>
    /// Generic Firebase Realtime Database repository.
    ///
    /// Subclasses supply four things — the node name, the ID prefix, and how to
    /// read/write the ID on the entity — and inherit all five CRUD operations
    /// plus sequential ID generation. Previously each service re-implemented
    /// "read all, find the highest number, add one, PUT" by hand; that logic
    /// now lives here once.
    /// </summary>
    /// <typeparam name="T">The model type stored in this node.</typeparam>
    public abstract class FirebaseRepository<T> : IRepository<T> where T : class
    {
        /// <summary>The database node name, e.g. "employees".</summary>
        protected abstract string Node { get; }

        /// <summary>Primary-key prefix, e.g. "EMP" produces "EMP-0001".</summary>
        protected abstract string IdPrefix { get; }

        /// <summary>Reads the primary key off an entity.</summary>
        protected abstract string GetId(T entity);

        /// <summary>Writes the primary key onto an entity.</summary>
        protected abstract void SetId(T entity, string id);

        /// <summary>The shared authenticated client.</summary>
        protected static Firebase.Database.FirebaseClient Client => FirebaseClientProvider.Client;

        // ── Read ──────────────────────────────────────────────────────────

        /// <inheritdoc/>
        public virtual async Task<List<T>> GetAllAsync()
        {
            var result = await Client.Child(Node).OnceAsync<T>();

            return result
                .Select(item =>
                {
                    // The key is the node name, not part of the value — map it
                    // back onto the object so callers see a populated ID.
                    SetId(item.Object, item.Key);
                    return item.Object;
                })
                .ToList();
        }

        /// <inheritdoc/>
        public virtual async Task<T?> GetByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return null;

            var entity = await Client.Child(Node).Child(id).OnceSingleAsync<T>();
            if (entity is null) return null;

            SetId(entity, id);
            return entity;
        }

        // ── Write ─────────────────────────────────────────────────────────

        /// <inheritdoc/>
        public virtual async Task<string> AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var newId = await GenerateNextIdAsync();
            SetId(entity, newId);

            await Client.Child(Node).Child(newId).PutAsync(entity);
            return newId;
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var id = GetId(entity);
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new InvalidOperationException(
                    $"Cannot update a {typeof(T).Name} that has no ID. Use AddAsync for new records.");
            }

            await Client.Child(Node).Child(id).PutAsync(entity);
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("An ID is required to delete a record.", nameof(id));

            await Client.Child(Node).Child(id).DeleteAsync();
        }

        // ── ID generation ─────────────────────────────────────────────────

        /// <summary>
        /// Produces the next sequential key, e.g. "EMP-0007".
        /// </summary>
        /// <remarks>
        /// This reads the node and takes max+1, so two clients adding a record at
        /// the same instant can pick the same ID and one will overwrite the other.
        /// Acceptable for a classroom demo; a real system would use a Firebase
        /// transaction on a counter node.
        /// </remarks>
        protected virtual async Task<string> GenerateNextIdAsync()
        {
            var all = await GetAllAsync();
            var prefix = $"{IdPrefix}-";

            var highest = all
                .Select(GetId)
                .Where(id => id.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .Select(id => int.TryParse(id.AsSpan(prefix.Length), out var n) ? n : 0)
                .DefaultIfEmpty(0)
                .Max();

            return $"{prefix}{highest + 1:D4}";
        }
    }
}
