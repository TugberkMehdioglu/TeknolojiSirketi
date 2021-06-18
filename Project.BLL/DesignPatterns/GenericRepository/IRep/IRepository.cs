using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.IRep
{
    public interface IRepository<T> where T : BaseEntity
    {
        //List Commands
        List<T> GetAll();
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetModifieds();

        //Modifieds Commands
        void Add(T item);
        void AddRange(List<T> item);
        void Delete(T item);
        void DeleteRange(List<T> item);
        void Update(T item);
        void UpdateRange(List<T> item);
        void Destroy(T item);
        void DestroyRange(List<T> item);

        //Expression Commands
        List<T> Where(Expression<Func<T, bool>> exp);
        T FirstOrDefault(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, object>> exp);

        //Find Commands
        T Find(int id);
        T FindFirstData();
        T FindLastData();
    }
}
