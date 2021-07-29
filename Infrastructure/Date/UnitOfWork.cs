// using System.Collections;
// using System.Threading.Tasks;
// using Core.Entities;
// using Core.Interfaces;
// using Infrastructure.Data;

// namespace Infrastructure.Date
// {
//     public class UnitOfWork : IUnitOfWork
//     {

//         private readonly ApplicationDbContext _context;
//         private Hashtable _reposietories;
//         private bool disposedValue;

//         public UnitOfWork(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<int> Complite()
//             => await _context.SaveChangesAsync();

//         public IGenericRepository<T> Repository<T>() where T : BaseEntity
//         {
//             if(_reposietories == null) _reposietories = new Hashtable();
//             var type = typeof(T).Name;
//             if(!_repositories.Contains(type))
//             {
//                 var repositoryType = typeof(GenericRepository<>);
//                 var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
//                 _repositories.Add(type, repositoryInstance);
//             }

//             return (IGenericRepository<TEntity>) _repositories[type];
//         }

//         public void Dispose()
//         {
//             _context.Dispose();
//         }
//     }
// }


    // public class UnitOfWork : IUnitOfWork
    // {
    //     private readonly StoreContext _context;
    //     private Hashtable _repositories;
    //     public UnitOfWork(StoreContext context)
    //     {
    //         _context = context;
    //     }

    //     public async Task<int> Complete()
    //     {
    //         return await _context.SaveChangesAsync();
    //     }

    //     public void Dispose()
    //     {
    //         _context.Dispose();
    //     }

    //     public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    //     {
    //         if(_repositories == null) _repositories = new Hashtable();

    //         var type = typeof(TEntity).Name;

    //         if(!_repositories.Contains(type))
    //         {
    //             var repositoryType = typeof(GenericRepository<>);
    //             var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
    //             _repositories.Add(type, repositoryInstance);
    //         }

    //         return (IGenericRepository<TEntity>) _repositories[type];
    //     }
    // }