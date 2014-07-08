using System;

namespace MyMediaCatalog.Domain
{
    public class Address : SoftDelete
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string PostalCode { get; set; }
        public int? CountryId { get; set; }

        public virtual State State { get; set; }
        public virtual Country Country { get; set; }
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public string CreatedUser { get; set; }
        public string ModifiedUser { get; set; }

    }
}