namespace MyMediaCatalog.Models
{
    public class CompanyAddressViewModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int AddressTypeId { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string PostalCode { get; set; }

    }
}