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
            throw new NotImplementedException();
        }

        public void AddRange(List<T> item)
        {
            throw new NotImplementedException();
        }

        public bool Any(System.Linq.Expressions.Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<T> item)
        {
            throw new NotImplementedException();
        }

        public void Destroy(T item)
        {
            throw new NotImplementedException();
        }

        public void DestroyRange(List<T> item)
        {
            throw new NotImplementedException();
        }

        public T Find(int id)
        {
            throw new NotImplementedException();
        }

        public T FindFirstData()
        {
            throw new NotImplementedException();
        }

        public T FindLastData()
        {
            throw new NotImplementedException();
        }

        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public List<T> GetActives()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<T> GetModifieds()
        {
            throw new NotImplementedException();
        }

        public List<T> GetPassives()
        {
            throw new NotImplementedException();
        }

        public object Select(System.Linq.Expressions.Expression<Func<T, object>> exp)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<T> item)
        {
            throw new NotImplementedException();
        }

        public List<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }
    }
}
