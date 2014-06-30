using System.Data.Entity;
using MyMediaCatalog.Domain;

namespace MyMediaCatalog.Data
{
    public class AppContext : DbContext
    {
        public AppContext() : base("AppContext")
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<PersonAddress> PersonAddresses { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<PersonPhone> PersonPhones { get; set; }
        public DbSet<CompanyPhone> CompanyPhones { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MediaStaffMember>().HasRequired(x => x.StaffMember).WithRequiredDependent().WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}