using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class Media
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string ISBN { get; set; }
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Display(Name = "Receipt")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReceiptDate { get; set; }
        public bool? Review { get; set; }
        public bool? Purchased { get; set; }
        public bool? Donate { get; set; }
        public bool? Active { get; set; }

        public DateTime? DateDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public string CreatedUser { get; set; }
        public string ModifiedUser { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int? CompanyId { get; set; }
        [Required]
        [Display(Name = "Media Type")]
        public int? MediaTypeId { get; set; }

        [Display(Name = "Company")]
        public virtual Company Company { get; set; }
        [Display(Name = "Media Type")]
        public virtual MediaType MediaType { get; set; }

        public virtual ICollection<StaffMember> StaffMembers { get; set; }

    }
}