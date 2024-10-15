using ClientProductManager.Core;
using ClientProductManager.Core.Repositories.Contract;
using ClientProductManager.Infrastructure.Data;
using ClientProductManager.Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientProductManager.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private Hashtable _repositories;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
                => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repo = new Repository<TEntity>(_dbContext);
                _repositories.Add(key, repo);
            }
            return _repositories[key] as IRepository<TEntity>;
        }
    }
}
