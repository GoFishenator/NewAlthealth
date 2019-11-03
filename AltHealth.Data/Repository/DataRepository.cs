using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AltHealth.Data.Repository
{
    public class DataRepository<T> :
        IRepository<T> where T : class
    {
        AltHealthEntitiesContainer _db = new AltHealthEntitiesContainer();
        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _db.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _db.Set<T>();
            return query;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
