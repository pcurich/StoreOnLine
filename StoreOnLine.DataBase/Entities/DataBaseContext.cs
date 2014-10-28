using StoreOnLine.DataBase.CMS;
using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Products;
using System;
using System.Data.Entity;
using StoreOnLine.DataBase.Model.Providers;
using StoreOnLine.DataBase.Model.Resources;
using StoreOnLine.DataBase.Model.Security;


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
        public DbSet<Ubigeo> Ubigeos { get; set; }
        public DbSet<ContactNumber> ContactNumbers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleDetail> ScheduleDetails { get; set; }




        public DbSet<Pages> Pages { get; set; }
        public DbSet<PageModules> PageModules { get; set; }
        //public DbSet<ModuleDefinitions> ModuleDefinitions { get; set; }
        public DbSet<LocalizeResources> LocalizeResources { get; set; }
        public DbSet<LocalizeProperties> LocalizeProperties { get; set; }
        //public DbSet<SiteSettings> SiteSettings { get; set; }
        //public DbSet<Languages> Languages { get; set; }
        //public DbSet<Users> Users { get; set; }
        //public DbSet<Helps> Helps { get; set; }
        //public DbSet<Roles> Roles { get; set; }
        public DbSet<UserInRoles> UserInRoles { get; set; }
        //public DbSet<Log> Logs { get; set; }
        //public DbSet<ModuleSetting> ModuleSettings { get; set; }
        ////Modules
        //public DbSet<HtmlContents> HtmlContents { get; set; }
        //public DbSet<ContactForm> ContactForms { get; set; }

    }
}