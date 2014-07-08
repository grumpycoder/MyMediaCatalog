using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyMediaCatalog.Domain;

namespace MyMediaCatalog.Models
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Company")]
        public string Name { get; set; }

        [Display(Name = "Website")]
        public string WebsiteUrl { get; set; }
        [UIHint("Email")]
        public string Email { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        //public virtual ICollection<Media> Media { get; set; }
        //public virtual ICollection<CompanyPhone> CompanyPhones { get; set; }
        //public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }
    }
}