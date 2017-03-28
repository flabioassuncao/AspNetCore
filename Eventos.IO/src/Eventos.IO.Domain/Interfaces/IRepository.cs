using Eventos.IO.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Eventos.IO.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        void Adicionar(TEntity obj);
        TEntity ObterPorId(Guid Id);
        IEnumerable<TEntity> ObterTodos();
        void Atualizar(TEntity obj);
        void Remove(Guid Id);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        int SaveChanges();
    }
}
