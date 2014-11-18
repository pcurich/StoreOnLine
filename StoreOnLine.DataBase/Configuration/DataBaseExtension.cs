using System.ComponentModel;
using System.Data.Entity;

namespace StoreOnLine.DataBase.Entities
{
    public static class DataBaseExtension
    {
        public static int SaveInContext(this StoreOnLineContext context)
        {
            return context.SaveChanges();
        }

        public static void Alter(this StoreOnLineContext context, DbSet objecDbSet, DataBaseId alter)
        {
            var elementoDb = objecDbSet.Find(alter.Id);

            for (var i = 0; i < TypeDescriptor.GetProperties(alter).Count; i++)
            {
                var property = TypeDescriptor.GetProperties(alter)[i];
                var value = property.GetValue(alter);
                if (!"Id".Equals(property.Name) && value != null)
                {
                    property.SetValue(elementoDb, value);
                }
            }
            context.Entry(elementoDb).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
