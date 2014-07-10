using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Models
{
    public class CompanyAddressViewModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Address Type is required.")]
        public int AddressTypeId { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string StateAbbr { get; set; }
        public string StateFullName { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public int? CountryId { get; set; }
    }
}