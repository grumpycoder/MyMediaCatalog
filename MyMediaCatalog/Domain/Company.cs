using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class Company : SoftDelete
    {
        public int Id { get; set; }
        [Display(Name = "Company")]
        public string Name { get; set; }

        [Display(Name = "Website")]
        public string WebsiteUrl { get; set; }
        [UIHint("Email")]
        public string Email { get; set; }
        
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<CompanyPhone> CompanyPhones { get; set; }
        public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public string CreatedUser { get; set; }
        public string ModifiedUser { get; set; }

        //public bool IsDeleted { get; set; }
        //public DateTime? DateDeleted { get; set; }
        //public string DeletedUser { get; set; }
    }
}