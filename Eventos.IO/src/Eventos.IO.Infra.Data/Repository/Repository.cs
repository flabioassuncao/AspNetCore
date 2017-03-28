using Eventos.IO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Eventos.IO.Domain.Core.Models;
using Eventos.IO.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eventos.IO.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected EventosContext Db;
        protected DbSet<TEntity> Dbset;

        public Repository(EventosContext context)
        {
            Db = context;
            Dbset = Db.Set<TEntity>();
        }

        public virtual void Adicionar(TEntity obj)
        {
            Dbset.Add(obj);
        }

        public virtual void Atualizar(TEntity obj)
        {
            Dbset.Update(obj);
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return Dbset.AsNoTracking().Where(predicate);
        }

        public virtual TEntity ObterPorId(Guid Id)
        {
            return Dbset.AsNoTracking().FirstOrDefault(t => t.Id == Id);
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return Dbset.ToList();
        }

        public virtual void Remove(Guid Id)
        {
            Dbset.Remove(Dbset.Find(Id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
