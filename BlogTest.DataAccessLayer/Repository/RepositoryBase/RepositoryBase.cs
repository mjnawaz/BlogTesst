﻿using BlogTest.DataAccessLayer.Interface.IRepositoryBase;
using BlogTest.ModelMigrations.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogTest.DataAccessLayer.Repository.RepositoryBase
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext _context { get; set; }
        public RepositoryBase(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IQueryable<T> FindAll()
        {
            return this._context.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>()
                .Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            this._context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this._context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }
    }
}
