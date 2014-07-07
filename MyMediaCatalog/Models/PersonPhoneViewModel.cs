using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Models
{
    public class PersonPhoneViewModel
    {
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        public int PhoneTypeId { get; set; }
        [Required]
        public string Number { get; set; }

    }
}