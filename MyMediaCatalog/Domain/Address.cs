namespace MyMediaCatalog.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public virtual State State { get; set; }

    }
}