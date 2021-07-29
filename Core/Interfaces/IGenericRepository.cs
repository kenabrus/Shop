using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
         Task<T> GetById(int id);
         Task<IReadOnlyList<T>> GetAll();

         void Add(T entity);
         void Update(T entity);
         void Delete(T entity);
    }
}