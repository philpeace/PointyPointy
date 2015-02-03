using Autofac;

namespace PointyPointy.Data.DataAccess
{
    /// <summary>
    ///     Wrapper for autofac lifetimescop so we don't have to reference it
    ///     in all our libraries
    /// </summary>
    public class LifeTimeScope : ILifeTimeScope
    {
        private readonly ILifetimeScope _scope;

        public LifeTimeScope(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public ILifeTimeScope BeginLifetimeScope()
        {
            return new LifeTimeScope(_scope.BeginLifetimeScope());
        }

        public T Resolve<T>()
        {
            return _scope.Resolve<T>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}