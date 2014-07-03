using System;
using System.Data.Entity.Migrations;
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

            //context.MediaTypes.AddOrUpdate(x => x.Name,
            //    new MediaType() { Name = "CD" },
            //    new MediaType() { Name = "Book" },
            //    new MediaType() { Name = "DVD" },
            //    new MediaType() { Name = "Magazine" }
            //    );

            //context.Companies.AddOrUpdate(x => x.Name,
            //    new Company() { Name = "HarperCollins" },
            //    new Company() { Name = "Corwin Press" },
            //    new Company() { Name = "Henry Holt and Company" },
            //    new Company() { Name = "New York University Press" }
            //    );

            //context.Roles.AddOrUpdate(x => x.Name,
            //    new Role() { Name = "Author" },
            //    new Role() { Name = "Assistant" },
            //    new Role() { Name = "Editor" },
            //    new Role() { Name = "Illustrator" },
            //    new Role() { Name = "VP" }
            //    );
            //context.SaveChanges();

            //context.Media.AddOrUpdate(x => x.Title,
            //    new Media()
            //    {
            //        Title = "Schools Where Everyone Belongs",
            //        ISBN = "0-87822-584-6",
            //        CompanyId = 1,
            //        Review = true,
            //        Donate = true,
            //        Purchased = true,
            //        MediaTypeId = 1,
            //        ReceiptDate = DateTime.Now.Date
            //    },
            //    new Media()
            //    {
            //        Title = "Cyberbullying and Cyberthreats",
            //        ISBN = "0-87822-537-4",
            //        CompanyId = 2,
            //        Review = true,
            //        Donate = true,
            //        Purchased = true,
            //        MediaTypeId = 2,
            //        ReceiptDate = DateTime.Now.Date
            //    }
            //    );

            //context.States.AddOrUpdate(x => x.Abbr,
            //    new State(){ Abbr = "AL", Name = "Alabama" },
            //    new State() { Abbr = "AZ", Name = "Arizona" },
            //    new State() { Abbr = "CA", Name = "California" },
            //    new State() { Abbr = "CO", Name = "Colorado" });

            //context.PhoneTypes.AddOrUpdate(x => x.Name,
            //    new PhoneType() { Name = "Home" },
            //    new PhoneType() { Name = "Office" },
            //    new PhoneType() { Name = "Cell" },
            //    new PhoneType() { Name = "Fax" }
            //    );

            //context.AddressTypes.AddOrUpdate(a => a.Name,
            //    new AddressType() { Name = "Home" },
            //    new AddressType() { Name = "Office" },
            //    new AddressType() { Name = "Shipping" }
            //    );

            //context.Persons.AddOrUpdate(p => p.Lastname, 
            //    new Person(){Firstname = "James", Lastname = "Butt"}, 
            //    new Person(){Firstname = "Josephine", Lastname = "Darakjy"},
            //    new Person(){Firstname = "Art", Lastname = "Venere"}, 
            //    new Person(){Firstname = "Lenna", Lastname = "Paprocki"} 
            //    );

        }
    }
}
