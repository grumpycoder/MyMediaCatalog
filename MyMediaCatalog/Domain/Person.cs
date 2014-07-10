using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class Person : SoftDelete
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [UIHint("EmailAddress")]
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