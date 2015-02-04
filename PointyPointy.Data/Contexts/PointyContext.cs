using System;
using System.ComponentModel.DataAnnotations.Schema;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScrumInvite>().HasKey<int>(k => k.Id);
            modelBuilder.Entity<ScrumInvite>().Property<int>(p => p.UserId).IsRequired();
            modelBuilder.Entity<ScrumInvite>().Property<DateTime>(p => p.Created);

            modelBuilder.Entity<ScrumInviteUser>().HasKey<int>(k => k.Id);
            modelBuilder.Entity<ScrumInviteUser>().Property<int>(p => p.UserId).IsRequired();
            modelBuilder.Entity<ScrumInviteUser>().Property<DateTime>(p => p.Responded);
            modelBuilder.Entity<ScrumInviteUser>().Property<bool>(p => p.Accepted);
            modelBuilder.Entity<ScrumInviteUser>().HasRequired(s => s.ScrumInvite).WithRequiredDependent();
        }
    }
}