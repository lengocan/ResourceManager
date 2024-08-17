using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class Notice
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid projectId { get; set; }
        public string? Content { get; set; }

    }
}
