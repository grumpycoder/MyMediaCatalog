namespace MyMediaCatalog.Domain
{
    public class PersonPhone
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int PhoneId { get; set; }
        public int PhoneTypeId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual PhoneType PhoneType { get; set; }

    }
}