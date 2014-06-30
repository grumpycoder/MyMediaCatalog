using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class Company
    {
        public int Id { get; set; }
        [Display(Name = "Company")]
        public string Name { get; set; }

        [Display(Name = "Website")]
        public string WebsiteUrl { get; set; }

        public string Email { get; set; }

        public DateTime? DateDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public string CreatedUser { get; set; }
        public string ModifiedUser { get; set; }


        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<CompanyPhone> CompanyPhones { get; set; }
        public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }

    }
}