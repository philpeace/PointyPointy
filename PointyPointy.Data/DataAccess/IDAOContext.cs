using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointyPointy.Data.DataAccess
{
    public interface IDAOContext : IDisposable
    {
        Guid Id { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
