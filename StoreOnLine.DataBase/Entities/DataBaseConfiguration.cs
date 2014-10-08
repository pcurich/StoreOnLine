using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using StoreOnLine.DataBase.Model.Companies;
using StoreOnLine.DataBase.Model.Security;

namespace StoreOnLine.DataBase.Entities
{
    public partial class StoreOnLineContext
    {
        class PersonConfiguration : EntityTypeConfiguration<Person>
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
        class AddressConfiguration : EntityTypeConfiguration<Address>
        {
            public AddressConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                HasRequired(p => p.Ubigeo);
                ToTable("Address");
            }
        }
        class UbigeoConfiguration : EntityTypeConfiguration<Ubigeo>
        {
            public UbigeoConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("Ubigeo");
            }
        }

        class DocumentTypeConfiguration : EntityTypeConfiguration<DocumentType>
        {
            public DocumentTypeConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("DocumentType");
            }
        }
        class ContactNumberConfiguration : EntityTypeConfiguration<ContactNumber>
        {
            public ContactNumberConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("ContactNumber");
            }
        }
        class UserConfiguration : EntityTypeConfiguration<User>
        {
            public UserConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("User");
            }
        }
        class RoleConfiguration : EntityTypeConfiguration<Role>
        {
            public RoleConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                ToTable("Role");
            }
        }
        class CompanyConfiguration : EntityTypeConfiguration<Company>
        {
            public CompanyConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                HasRequired(p => p.Address);
                HasRequired(p => p.ContactNumber);
                HasRequired(p => p.Person);
                //HasRequired(p => p.ContactNumber).WithRequiredDependent().WillCascadeOnDelete(false);
                //HasRequired(p => p.Address).WithRequiredDependent().WillCascadeOnDelete(false);
                ToTable("Companies");
            }
        }

        class ScheduleConfiguration : EntityTypeConfiguration<Schedule>
        {
            public ScheduleConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                //HasRequired(p => p.Person).WithRequiredDependent().WillCascadeOnDelete(false);
                ToTable("Schedule");
            }
        }
        class ScheduleDetailConfiguration : EntityTypeConfiguration<ScheduleDetail>
        {
            public ScheduleDetailConfiguration()
            {
                Property(p => p.AddedDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.ModificationDate).IsRequired().HasColumnType("datetime2");
                Property(p => p.TimeStart).IsRequired().HasColumnType("datetime2");
                Property(p => p.TimeEnd).IsRequired().HasColumnType("datetime2");
                //HasRequired(p => p.Person).WithRequiredDependent().WillCascadeOnDelete(false);
                ToTable("ScheduleDetail");
            }
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new ContactNumberConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new DocumentTypeConfiguration());
            modelBuilder.Configurations.Add(new UbigeoConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new ScheduleConfiguration());
            modelBuilder.Configurations.Add(new ScheduleDetailConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
