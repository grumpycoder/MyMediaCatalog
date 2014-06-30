namespace MyMediaCatalog.Domain
{
    public class CompanyPhone
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int PhoneId { get; set; }
        public int PhoneTypeId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual PhoneType PhoneType { get; set; }
    }
}