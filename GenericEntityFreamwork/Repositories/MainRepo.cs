﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayer.Repositories
{
    public class MainRepo<TEntity> : IMainRepo<TEntity> where TEntity : class
    {
        private DadashiEnglishContext _context;
        private DbSet<TEntity> _dbSet;

        public MainRepo(DadashiEnglishContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual EntityEntry Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual bool Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return true;

        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                    _dbSet.Attach(entity);
                _dbSet.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool DeleteById(object id)
        {
            try
            {
                TEntity entity = _dbSet.Find(id);
                return entity != null && Delete(entity);
            }
            catch
            {
                return false;
            }
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includes = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (where != null)
                query = query.Where(where);
            if (orderBy != null)
                query = orderBy(query);
            if (includes != null)
                foreach (string i in includes.Split(','))
                    query = query.Include(i);
            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
