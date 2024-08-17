using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class Notice
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserIdReceivedDM { get; set; }
        public Guid UserIdSent { get; set; }
        public Guid projectId { get; set; }
        public string? Content { get; set; }

        public string? TimeCreate { get; set; } 
    }
}
