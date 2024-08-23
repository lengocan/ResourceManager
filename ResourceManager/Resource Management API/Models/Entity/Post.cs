using System.ComponentModel.DataAnnotations;

namespace RMAPI.Models.Entity
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        public string? createdBy { get; set; }
        public string? content { get; set; }

        public string? created { get; set; }

    }
}
