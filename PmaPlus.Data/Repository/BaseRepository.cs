using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Data
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
            _dataBaseContext.SaveChanges();
        }
        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
            _dataBaseContext.SaveChanges();
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
            _dataBaseContext.SaveChanges();

        }
        public virtual T GetById(int id)
        {
            return _dbset.Find(id);
        }
       
        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

       

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }
    }
}
