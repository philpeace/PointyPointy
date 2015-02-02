using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodePeace.Common;

namespace PointyPointy.Data.DataAccess
{
    public interface ILifeTimeScope : IDependency, IDisposable
    {
        ILifeTimeScope BeginLifetimeScope();
        T Resolve<T>();
    }
}
