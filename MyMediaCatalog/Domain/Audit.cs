using System;

namespace MyMediaCatalog.Domain
{
    public class Audit
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public string CreatedUser { get; set; }
        public string ModifiedUser { get; set; }
    }
}