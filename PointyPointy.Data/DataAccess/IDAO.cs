using System;
using System.Linq;

namespace PointyPointy.Data.DataAccess
{
    public interface IDAO<T> where T : class
    {
        Guid ContextId { get; }

        IQueryable<T> Table { get; }
    }
}