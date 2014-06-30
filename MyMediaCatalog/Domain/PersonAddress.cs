namespace MyMediaCatalog.Domain
{
    public class PersonAddress
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public bool? IsPrimary { get; set; }

        public virtual Person Person { get; set; }
        public virtual Address Address { get; set; }
        public virtual AddressType AddressType { get; set; }

    }
}