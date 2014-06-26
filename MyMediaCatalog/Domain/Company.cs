using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class Company
    {
        public int Id { get; set; }
        [Display(Name = "Company")]
        public string Name { get; set; }
    }
}