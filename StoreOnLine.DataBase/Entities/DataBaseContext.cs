using System.Collections.Generic;
using System.IO;
using StoreOnLine.DataBase.Model.Products;
using System;
using System.Data.Entity;
using StoreOnLine.DataBase.Model.Providers;
using StoreOnLine.DataBase.Model.Resources;
using StoreOnLine.DataBase.Model.Security;
using StoreOnLine.Util.Xml;

namespace StoreOnLine.DataBase.Entities
{
    public partial class StoreOnLineContext : DbContext
    {
        public StoreOnLineContext()
        {
            Configuration.LazyLoadingEnabled = false; //http://sebys.com.ar/2011/06/01/entity-framework-4-1-cf-y-lazy-load/
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = true;
        }
        public StoreOnLineContext(String nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false; //http://sebys.com.ar/2011/06/01/entity-framework-4-1-cf-y-lazy-load/
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = true;
        }

        public DbSet<ProductBase> ProductBases { get; set; }
        public DbSet<ProductComposite> ProductComposites { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Imagen> Imagens { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


    }
}