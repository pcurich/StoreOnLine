using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Entities
{
    public partial class StoreOnLineContext
    {
        private class PersonConfiguration : EntityTypeConfiguration<Person>
        {
            public PersonConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                HasRequired(p => p.Address);
                HasRequired(p => p.User);
                HasRequired(p => p.ContactNumber);
                HasRequired(p => p.Document);
                HasRequired(p => p.Role);
                ToTable("People");
            }
        }
        private class AddressConfiguration : EntityTypeConfiguration<Address>
        {
            public AddressConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                HasRequired(p => p.Ubigeo);
                ToTable("Address");
            }
        }
        private class UbigeoConfiguration : EntityTypeConfiguration<Ubigeo>
        {
            public UbigeoConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("Ubigeo");
            }
        }
        private class DocumentConfiguration : EntityTypeConfiguration<Document>
        {
            public DocumentConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                HasRequired(p => p.DocumentType);
                ToTable("Document");
            }
        }
        private class DocumentTypeConfiguration : EntityTypeConfiguration<DocumentType>
        {
            public DocumentTypeConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("DocumentType");
            }
        }
        private class ContactNumberConfiguration : EntityTypeConfiguration<ContactNumber>
        {
            public ContactNumberConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("ContactNumber");
            }
        }
        private class UserConfiguration : EntityTypeConfiguration<User>
        {
            public UserConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("User");
            }
        }
        private class RoleConfiguration : EntityTypeConfiguration<Role>
        {
            public RoleConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("Role");
            }
        }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new DocumentConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new ContactNumberConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new DocumentTypeConfiguration());
            modelBuilder.Configurations.Add(new UbigeoConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
