using System.ComponentModel.DataAnnotations;

namespace MyMediaCatalog.Domain
{
    public class MediaType
    {
        public int Id { get; set; }
        [Display(Name = "Type")]
        public string Name { get; set; }
    }
}