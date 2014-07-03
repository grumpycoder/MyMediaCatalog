using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class Person : SoftDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [UIHint("EmailAddress")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<PersonPhone> PersonPhones { get; set; }
        public virtual ICollection<PersonAddress> PersonAddresses { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public string CreatedUser { get; set; }
        public string ModifiedUser { get; set; }

        //public bool IsDeleted { get; set; }
        //public DateTime? DateDeleted { get; set; }
        //public string DeletedUser { get; set; }
    }
}