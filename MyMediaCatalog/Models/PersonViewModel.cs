using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        public string Email { get; set; }

        public string FullName { get { return string.Format("{0} {1}", Firstname, Lastname); } }
    }
}