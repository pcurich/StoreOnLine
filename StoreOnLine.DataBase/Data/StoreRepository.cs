using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using StoreOnLine.DataBase.Model;

namespace StoreOnLine.DataBase.Data
{
    /// <summary>
    ///     The EF-dependent, generic repository for data access
    /// </summary>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    public class StoreRepository<T> : IRepository<T> where T : DataBaseId
    {
        private  readonly string _user;
        public StoreRepository(DbContext dbContext,string user)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
            _user = user;
        
        }

        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            entity.UpdDate = Util.DateTime.DateTimeConvert.GetDateTimeNow();
            entity.AddDate = Util.DateTime.DateTimeConvert.GetDateTimeNow();
            entity.UpdUser = _user;
            entity.AddUser = _user;
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            var elementoDb = DbSet.Find(entity.Id);
            entity.UpdDate = Util.DateTime.DateTimeConvert.GetDateTimeNow();
            entity.UpdUser = _user;
            for (var i = 0; i < TypeDescriptor.GetProperties(entity).Count; i++)
            {
                var property = TypeDescriptor.GetProperties(entity)[i];
                if (!property.Name.Equals("Id") && property.GetValue(entity) != null)
                {
                    property.SetValue(elementoDb, property.GetValue(entity));
                }
            }

            DbEntityEntry dbEntityEntry = DbContext.Entry(elementoDb);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(elementoDb);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var elementoDb = DbSet.Find(entity.Id);
            DbEntityEntry dbEntityEntry = DbContext.Entry(elementoDb);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(elementoDb);
                DbSet.Remove(elementoDb);
            }
        }

        public virtual void Delete(int id)
        {
            T entity = GetById(id);
            if (entity == null) 
                return; // not found; assume already deleted.
            Delete(entity);
        }
    }
}