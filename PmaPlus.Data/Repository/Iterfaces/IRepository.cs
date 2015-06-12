using System;
using System.Linq;
using System.Linq.Expressions;

namespace PmaPlus.Data.Repository.Iterfaces
{

    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Update(T entity, int id);
        void AddOrUpdate(T[] entities);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int id);

        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);

    }

}
