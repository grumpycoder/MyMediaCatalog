namespace MyMediaCatalog.Domain
{
    public class StaffMember
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public int RoleId { get; set; }
        public int MediaId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Role Role { get; set; }
        public virtual Media Media { get; set; }

    }
}