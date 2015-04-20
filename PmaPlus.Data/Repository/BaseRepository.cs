using System;
using System.Data.Entity;
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
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
            _dataBaseContext.SaveChanges();
        }
        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataBaseContext.Entry(entity).State = EntityState.Modified;
            _dataBaseContext.SaveChanges();
        }
        public virtual void Delete(T entity)
        {
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
