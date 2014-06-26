using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Display(Name = "Receipt")]
        public DateTime? ReceiptDate { get; set; }
        public bool? Review { get; set; }
        public bool? Purchased { get; set; }
        public bool? Donate { get; set; }
        public bool? Active { get; set; }

        public DateTime? DateDeleted { get; set; }

        public int? CompanyId { get; set; }
        public int? MediaTypeId { get; set; }

        [Display(Name = "Company")]
        public virtual Company Company { get; set; }
        [Display(Name = "Media Type")]
        public virtual MediaType MediaType { get; set; }

        public virtual ICollection<StaffMember> StaffMembers { get; set; }

    }
}