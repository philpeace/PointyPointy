using System;
using System.Data.Entity;

namespace PointyPointy.Data.DataAccess
{
    public interface IDAOContext : IDisposable
    {
        Guid Id { get; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}