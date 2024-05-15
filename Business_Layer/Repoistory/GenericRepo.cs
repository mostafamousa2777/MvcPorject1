using Business_Layer.Interfaces;
using DataAccess_Layer.Context;
using DataAccess_Layer.models;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer.Repoistory
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly CompanydbContext _context;

        public GenericRepo(CompanydbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
           await  _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T?> GetAsync(int id) => await _context.Set<T>().FindAsync(id);

        public  async Task< IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(e=>e.Department).ToListAsync();
            
            }
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        public void update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
