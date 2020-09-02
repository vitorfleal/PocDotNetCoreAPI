using AE.HealthSystem.Domain.Interfaces;
using AE.HealthSystem.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AE.HealthSystem.Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected HealthSystemContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(HealthSystemContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
            Db.SaveChanges();
        }

        public void Atualizar(TEntity obj)
        {
            DbSet.Update(obj);
            Db.SaveChanges();
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public TEntity ObterPorId(long id)
        {
            var entity = DbSet.Find(id);
            Db.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public IEnumerable<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public void Remover(long id)
        {
            DbSet.Remove(DbSet.Find(id));
            Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
