using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.ComponentModel;

namespace StoreOnLine.DataBase.Entities
{
    public class DbSetHelp<T> where T : DataBaseId
    {
        public DbSet<T> Dbset { get; set; }

        public StoreOnLineContext Context { get; private set; }

        public Type ClassType { get; private set; }

        public DbSetHelp(StoreOnLineContext context, DbSet<T> dbset)
        {
            Context = context;
            Dbset = dbset;
            ClassType = typeof(T);
        }

        public void Remove(T type, bool isEliminadoFisico = false)
        {
            var elementToDelete = Dbset.Find(type.Id);
            if (elementToDelete == null)
            {
                return;
            }

            if (!isEliminadoFisico)
            {
                elementToDelete.IsDeleted = true;
                Alter(elementToDelete);
            }
            else
            {
                Dbset.Remove(elementToDelete);
                Context.SaveChanges();
            }
        }

        public void Alter(T fieldChanged)
        {
            if (!(fieldChanged.Id >= 1))
            {
                throw new Exception("El campo Id no puede ser null");
            }

            var elementoDb = Dbset.Find(fieldChanged.Id);

            if (elementoDb == null)
            {
                throw new Exception("No existe el Id en la BD");
            }

            for (var i = 0; i < TypeDescriptor.GetProperties(fieldChanged).Count; i++)
            {
                var property = TypeDescriptor.GetProperties(fieldChanged)[i];
                if (!property.Name.Equals("Id") && property.GetValue(fieldChanged) != null)
                {
                    property.SetValue(elementoDb, property.GetValue(fieldChanged));
                }
            }
            Context.Entry(elementoDb).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public int Add(T element)
        {
            Dbset.Add(element);
            Context.SaveChanges();
            return element.Id;
        }

        public T Find(int id, bool logicDelete = true)
        {
            try
            {
                var a = Dbset.Find(id);
                if (a.IsDeleted && !logicDelete)
                {
                    return null;
                }
                return a;
            }
            catch
            {
                return null;
            }
        }

        public bool Any(Func<T, bool> predicate, bool incluyeEliminadoLogico = false)
        {
            try
            {
                return Dbset.Where(predicate).Any(p => !p.IsDeleted || incluyeEliminadoLogico);
            }
            catch
            {
                return false;
            }
        }

        public T One(Func<T, bool> predicate, bool incluyeEliminadoLogico = false)
        {
            try
            {
                return Dbset.Where(predicate).FirstOrDefault(p => !p.IsDeleted || incluyeEliminadoLogico);
            }
            catch
            {
                return null;
            }
        }

        public List<T> Where(Func<T, bool> predicate, bool logicDelete = false)
        {
            try
            {
                return Dbset.Where(predicate).Where(p => !p.IsDeleted || logicDelete).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<T> ToList(bool logicDelete = false)
        {
            var res = Dbset.ToList();
            var ans = new List<T>();

            foreach (var item in res)
            {
                if (logicDelete)
                {
                    ans.Add(item);
                }
                else
                {
                    if (!item.IsDeleted)
                    {
                        ans.Add(item);
                    }
                }
            }
            return ans;
        }

        public List<T> Include(Expression<Func<T, DataBaseId>> path)
        {
            return Dbset.Include(path).ToList();
        }

    }
}
