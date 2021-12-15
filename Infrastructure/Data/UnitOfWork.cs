using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        private Hashtable _reposietories;
        private bool disposedValue;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complite()
            => await _context.SaveChangesAsync();

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            if(_reposietories == null) _reposietories = new Hashtable();
            var type = typeof(T).Name;
            if(!_reposietories.Contains(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _reposietories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>) _reposietories[type];
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}