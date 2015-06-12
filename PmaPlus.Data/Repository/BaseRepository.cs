using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace PmaPlus.Data.Repository
{
    public abstract class RepositoryBase<T> where T : class
    {
        private DataBaseContext _dataBaseContext;
        private readonly IDbSet<T> _dbset;
        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected DataBaseContext DataContext
        {
            get { return _dataBaseContext ?? (_dataBaseContext = DatabaseFactory.Get()); }
        }
        public virtual T Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            var newEntity = _dbset.Add(entity);
            _dataBaseContext.SaveChanges();
            return newEntity;
        }

        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataBaseContext.Entry(entity).State = EntityState.Modified;
            _dataBaseContext.SaveChanges();
        }
        public virtual void Update(T entity,int id)
        {
            if (entity == null || id <= 0)
            {
                return;
            }
            var oldEntity = _dbset.Find(id);
            if (oldEntity == null)
            {
                return;
            }
            
            _dataBaseContext.Entry(oldEntity).CurrentValues.SetValues(entity);
            _dataBaseContext.SaveChanges();
        }

        public virtual void AddOrUpdate(T[] entities)
        {
            _dbset.AddOrUpdate(entities);
            _dataBaseContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                return;
            }
            _dbset.Remove(entity);
            _dataBaseContext.SaveChanges();
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IQueryable<T> objects = _dbset.Where<T>(where).AsQueryable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
            _dataBaseContext.SaveChanges();

        }
        public virtual T GetById(int id)
        {
            return _dbset.Find(id);
        }
       
        public virtual IQueryable<T> GetAll()
        {
            return _dbset.AsQueryable();
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).AsQueryable();
        }

       

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }
    }
}
