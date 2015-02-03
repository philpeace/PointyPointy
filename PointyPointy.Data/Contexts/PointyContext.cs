using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PointyPointy.Data.DataAccess;
using PointyPointy.Data.Entities;

namespace PointyPointy.Data.Contexts
{
    public class PointyContext : DbContext, IPointyContext, IDAOContext
    {
        public PointyContext()
            : this(ConfigurationManager.ConnectionStrings["PointyContext"].ConnectionString)
        {
        }

        public PointyContext(string connectionString)
            : this(connectionString, new PointyInitializer())
        {
        }

        public PointyContext(string connectionString, IDatabaseInitializer<PointyContext> databaseInitiliser)
            : base(connectionString)
        {
            Database.SetInitializer(databaseInitiliser);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            ((IObjectContextAdapter) this).ObjectContext.CommandTimeout = 3000;

            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IDbSet<ScrumInvite> ScrumInvite { get; set; }

        public IDbSet<ScrumInviteUser> ScrumInviteUser { get; set; }
    }
}