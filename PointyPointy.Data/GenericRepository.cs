﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PointyPointy.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;

        public GenericRepository(IDbContext context)
        {
            _context = context;
        }

        protected IDbSet<T> Entities
        {
            get { return _context.Set<T>(); }
        }

        public virtual T Get(int id)
        {
            return Entities.Find(id);
        }

        public virtual IQueryable<T> Table
        {
            get { return Entities.AsQueryable(); }
        }

        public IQueryable<T> TableIncluding(params string[] includes)
        {
            return includes.Aggregate(Table, (current, includeExpression) => current.Include(includeExpression));
        }

        public virtual T Create(T entity)
        {
            Entities.Add(entity);

            _context.SaveChanges();

            return entity;
        }

        public virtual T Update(T entity)
        {
            _context.SaveChanges();

            return entity;
        }

        public virtual void Delete(T entity)
        {
            Entities.Remove(entity);

            _context.SaveChanges();
        }

        IEnumerable<T> IRepository<T>.Fetch(Expression<Func<T, bool>> predicate)
        {
            return Fetch(predicate);
        }

        IEnumerable<T> IRepository<T>.Fetch(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            return Fetch(predicate, includes);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return Fetch(predicate).SingleOrDefault();
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Table.Any(predicate);
        }

        public virtual IQueryable<T> Fetch(Expression<Func<T, bool>> predicate, string[] includes)
        {
            IQueryable<T> set = Fetch(predicate);

            foreach (string includeExpression in includes)
            {
                set = set.Include(includeExpression);
            }

            return set;
        }

        public virtual IQueryable<T> Fetch(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate);
        }
    }
}