﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PointyPointy.Data
{
    public interface IRepository<T> where T : class
    {
        T Create(T entity);
        
        T Update(T entity);
       
        void Delete(T entity);

        T Get(int id);
        
        T Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> Table
        {
            get;
        }

        IQueryable<T> TableIncluding(params string[] includes);

        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate, params string[] includes);

        IEnumerable<T> Fetch(Expression<Func<T, bool>> predicate);

        bool Exists(Expression<Func<T, bool>> predicate);
    }
}