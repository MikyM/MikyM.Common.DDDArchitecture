using MikyM.Common.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MikyM.Common.DataAccessLayer.Helpers
{
    public static class UoFCache
    {
        public static List<Type> CachedTypes { get; }

        static UoFCache()
        {
            CachedTypes ??= AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes()
                    .Where(t => t.BaseType == typeof(Repository<>) || t.BaseType == typeof(ReadOnlyRepository<>) || t == typeof(ReadOnlyRepository<>)))
                .ToList();
        }
    }
}
