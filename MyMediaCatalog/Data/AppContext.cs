using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
        public DbSet<Country> Countries { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MediaStaffMember>().HasRequired(x => x.StaffMember).WithRequiredDependent().WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var ctx = ((IObjectContextAdapter) this).ObjectContext;

            //ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted).ToList();

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted && e.Entity is SoftDelete))
            {
                entry.State = EntityState.Unchanged;
                ((SoftDelete) entry.Entity).DateDeleted = DateTime.Now;
                ((SoftDelete) entry.Entity).IsDeleted = true;
                ((SoftDelete) entry.Entity).DeletedUser = Environment.UserName;
            }

            return base.SaveChanges();

        }
    }
}