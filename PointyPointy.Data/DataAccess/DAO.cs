using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointyPointy.Data.DataAccess
{
    public class DAO<T> : IDAO<T> where T : class
    {
        public DAO(IDAOContext ctx)
        {
            ContextId = ctx.Id;
            Table = ctx.Set<T>();
        }

        public Guid ContextId { get; private set; }

        public IQueryable<T> Table { get; private set; }
    }
}
