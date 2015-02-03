using System;
using CodePeace.Common;

namespace PointyPointy.Data.DataAccess
{
    public interface ILifeTimeScope : IDependency, IDisposable
    {
        ILifeTimeScope BeginLifetimeScope();

        T Resolve<T>();
    }
}