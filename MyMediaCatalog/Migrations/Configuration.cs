using System;
using System.Data.Entity.Migrations;
using Antlr.Runtime.Tree;
using MyMediaCatalog.Domain;

namespace MyMediaCatalog.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Data.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data.AppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.MediaTypes.AddOrUpdate(x => x.Name,
                new MediaType() { Name = "CD" },
                new MediaType() { Name = "Book" },
                new MediaType() { Name = "DVD" },
                new MediaType() { Name = "Magazine" }
                );

            context.Companies.AddOrUpdate(x => x.Name,
                new Company() { Name = "HarperCollins" },
                new Company() { Name = "Corwin Press" },
                new Company() { Name = "Henry Holt and Company" },
                new Company() { Name = "New York University Press" }
                );

            context.Roles.AddOrUpdate(x => x.Name,
                new Role() { Name = "Author" },
                new Role() { Name = "Assistant" },
                new Role() { Name = "Editor" },
                new Role() { Name = "Illustrator" },
                new Role() { Name = "VP" }
                );
            context.SaveChanges();

            context.Media.AddOrUpdate(x => x.Title,
                new Media()
                {
                    Title = "Schools Where Everyone Belongs",
                    ISBN = "0-87822-584-6",
                    CompanyId = 1,
                    Active = true,
                    Review = true,
                    Donate = true,
                    Purchased = true,
                    MediaTypeId = 1,
                    ReceiptDate = DateTime.Now.Date
                },
                new Media()
                {
                    Title = "Cyberbullying and Cyberthreats",
                    ISBN = "0-87822-537-4",
                    CompanyId = 2,
                    Active = true,
                    Review = true,
                    Donate = true,
                    Purchased = true,
                    MediaTypeId = 2,
                    ReceiptDate = DateTime.Now.Date
                }
                );

        }
    }
}
