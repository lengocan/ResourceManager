using System.ComponentModel.DataAnnotations;

namespace RMAPI.Models.Entity
{
    public class Banner
    {
        [Key]
        public Guid Id { get; set; }
        public string? content { get; set; } 

        public string? color { get; set; }

        public string? effect {  get; set; }

        public bool isUse { get; set; } = false;
    }
}
