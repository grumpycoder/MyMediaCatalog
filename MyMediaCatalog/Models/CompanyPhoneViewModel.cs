using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Models
{
    public class CompanyPhoneViewModel
    {
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public int PhoneTypeId { get; set; }
        [Required]
        public string Number { get; set; }

    }
}