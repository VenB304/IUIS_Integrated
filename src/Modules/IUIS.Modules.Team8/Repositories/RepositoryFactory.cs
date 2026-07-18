using System;
using IUIS.Modules.Team8.Models;
using IUIS.Modules.Team8.Services;

namespace IUIS.Modules.Team8.Repositories
{
    public static class RepositoryFactory
    {
        public static IRepository<T> Create<T>(string fileName, string firebasePath) where T : class, IEntity, new()
        {
            if (AppSettings.DbMode.Equals("Firebase", StringComparison.OrdinalIgnoreCase))
            {
                return new FirebaseRepository<T>(AppSettings.FirebaseUrl, firebasePath);
            }
            else
            {
                return new JsonRepository<T>(fileName);
            }
        }
    }
}
