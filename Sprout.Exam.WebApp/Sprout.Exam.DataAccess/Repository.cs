using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _entities;

        public Repository(ApplicationDbContext dbContext)
        {
            _entities = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Delete(int id)
        {
            T entity = _entities.Find(id);
            _entities.Remove(entity);
        }

        public async Task<T> GetByQuery(Expression<Func<T, bool>> expression)
        {
            return await _entities.FirstOrDefaultAsync(expression);
        }
    }
}
