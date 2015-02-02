using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointyPointy.Data.DataAccess
{
    public interface IDAO<T>  where T : class
    {
        Guid ContextId { get; }

        IQueryable<T> Table { get; }
    }
}
