namespace MyMediaCatalog.Domain
{
    public class CompanyAddress
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Address Address { get; set; }
        public virtual AddressType AddressType { get; set; }
    }

}