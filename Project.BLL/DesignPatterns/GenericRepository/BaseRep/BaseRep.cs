using Project.BLL.DesignPatterns.GenericRepository.IRep;
using Project.BLL.DesignPatterns.SingletonPattern;
using Project.DAL.ContextClasses;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.BaseRep
{
    public abstract class BaseRep<T> : IRepository<T> where T : BaseEntity
    {
        protected MyContext _db;

        public BaseRep()
        {
            _db = DBTool.DBInstance;
        }

        protected void Save()
        {
            _db.SaveChanges();
        }

        public void Add(T item)
        {
            _db.Set<T>().Add(item);
            Save();
        }

        public void AddRange(List<T> item)
        {
            _db.Set<T>().AddRange(item);
            Save();
        }

        public bool Any(System.Linq.Expressions.Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Any(exp);
        }

        public void Delete(T item)
        {
            item.DeleteDate = DateTime.Now;
            item.Status = ENTITIES.Enums.DataStatus.Deleted;
            Save();
        }

        public void DeleteRange(List<T> item)
        {
            foreach (T element in item)
            {
                Delete(element);
            }
        }

        public void Destroy(T item)
        {
            _db.Set<T>().Remove(item);
            Save();
        }

        public void DestroyRange(List<T> item)
        {
            foreach (T element in item)
            {
                Destroy(element);
            }
        }

        public T Find(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public T FindFirstData()
        {
            return _db.Set<T>().OrderBy(x => x.CreatedDate).FirstOrDefault();
        }

        public T FindLastData()
        {
            return _db.Set<T>().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().FirstOrDefault(exp);
        }

        public List<T> GetActives()
        {
            return Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted);
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public List<T> GetModifieds()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Updated);
        }

        public List<T> GetPassives()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Deleted);
        }

        public object Select(System.Linq.Expressions.Expression<Func<T, object>> exp)
        {
            return _db.Set<T>().Select(exp).ToList();
        }

        public void Update(T item)
        {
            item.Status = ENTITIES.Enums.DataStatus.Updated;
            item.ModifiedDate = DateTime.Now;
            T toBeUpdated = Find(item.ID);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(item);
            Save();
        }

        public void UpdateRange(List<T> item)
        {
            foreach (T element in item)
            {
                Update(element);
            }
        }

        public List<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> exp)
        {
            return _db.Set<T>().Where(exp).ToList();
        }
    }
}
