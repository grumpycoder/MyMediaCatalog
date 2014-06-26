using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyMediaCatalog.Domain;

namespace MyMediaCatalog.Models
{
    public class StaffMemberViewModel
    {
        public int PersonId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public int MediaId { get; set; }

        public Role Role { get; set; }
        public Media Media { get; set; }
    }
}