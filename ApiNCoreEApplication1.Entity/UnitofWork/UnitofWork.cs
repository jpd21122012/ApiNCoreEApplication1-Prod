using ApiNCoreEApplication1.Entity.Context;
using ApiNCoreEApplication1.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiNCoreEApplication1.Entity.UnitofWork
{


    public interface IUnitOfWork : IDisposable
    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;

        ApiNCoreEApplication1Context Context { get; }
        int Save();
        Task<int> SaveAsync();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
    }

    public class UnitOfWork : IUnitOfWork
    {
        public ApiNCoreEApplication1Context Context { get; }

        private Dictionary<Type, object> _repositoriesAsync;
        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitOfWork(ApiNCoreEApplication1Context context)
        {
            Context = context;
            _disposed = false;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(this);
            return (IRepository<TEntity>)_repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositoriesAsync = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositoriesAsync.ContainsKey(type)) _repositoriesAsync[type] = new RepositoryAsync<TEntity>(this);
            return (IRepositoryAsync<TEntity>)_repositoriesAsync[type];
        }

        public int Save()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }
        public async Task<int> SaveAsync()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
