using System;
using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Models
{
    public class MediaViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string ISBN { get; set; }
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Display(Name = "Receipt")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [UIHint("Date")]
        public DateTime? ReceiptDate { get; set; }
        [UIHint("Boolean")]
        public bool? Review { get; set; }
        public bool? Purchased { get; set; }
        public bool? Donate { get; set; }
        public bool? Active { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int? CompanyId { get; set; }
        [Required]
        [Display(Name = "Media Type")]
        public int? MediaTypeId { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        [Display(Name = "Media Type")]
        public string MediaTypeName { get; set; }
    }
}