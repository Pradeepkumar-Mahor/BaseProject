﻿namespace DataAccess.Context
{
    internal interface IGenericRepository<T>
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        bool Add(T entity);

        bool Update(T entity);

        bool Delete(T entity);
    }
}