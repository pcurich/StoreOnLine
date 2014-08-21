using System.Collections.Generic;
using System.Data.Entity;
using StoreOnLine.DataBase.Model.Products;

namespace StoreOnLine.DataBase.Entities
{
    public class DataBaseInitializer
    {
        public class StoreOnLineInitializerDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<StoreOnLineContext>
        {
            protected override void Seed(StoreOnLineContext context)
            {
                // base.Seed(context);
            }

            public override void InitializeDatabase(StoreOnLineContext context)
            {
                if (context.Database.Exists())
                {
                    //context.Database.ExecuteSqlCommand("");
                }
            }
        }

        public class StoreOnLineInitializerDropCreateDatabaseAlways : DropCreateDatabaseAlways<StoreOnLineContext>
        {
            protected override void Seed(StoreOnLineContext context)
            {
                //base.Seed(context);
            }

            public override void InitializeDatabase(StoreOnLineContext context)
            {
                if (context.Database.Exists())
                {
                    //context.Database.ExecuteSqlCommand("");
                }
            }
        }

        public class StoreOnLineInitializerCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<StoreOnLineContext>
        {
            protected override void Seed(StoreOnLineContext context)
            {
                var pbs = new List<ProductBase>
                {
                    new ProductBase {ProductName = "Football", ProductBasePrice = 25M},
                    new ProductBase {ProductName = "Surf board", ProductBasePrice = 179M},
                    new ProductBase {ProductName = "Running shoes", ProductBasePrice = 95M}
                };

                foreach (var pb in pbs)
                {
                    context.ProductBases.Add(pb);
                }

            }

            public override void InitializeDatabase(StoreOnLineContext context)
            {
                if (context.Database.Exists())
                {
                    //context.Database.ExecuteSqlCommand("");
                }
            }
        }
    }
}
