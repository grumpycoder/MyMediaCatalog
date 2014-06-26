using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
    }
}