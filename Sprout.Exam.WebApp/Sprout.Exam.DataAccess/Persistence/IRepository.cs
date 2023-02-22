using Sprout.Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        Task<T> GetByQuery(Expression<Func<T, bool>> expression);
    }
}
