﻿namespace Business_Layer.Interfaces
{
    public interface IGenericRepo<T>
    {
       Task< IEnumerable<T> >GetAllAsync();
       Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
