using System.Linq;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.Data
{
    public interface IRepository<T> where T : DataBaseId
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}