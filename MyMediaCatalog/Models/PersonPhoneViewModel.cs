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
        [UIHint("Phone")]
        [RegularExpression(@"\+[0-9]+", ErrorMessage = "Phone number can only contain digits 0-9. ")]
        public string Number { get; set; }

    }
}