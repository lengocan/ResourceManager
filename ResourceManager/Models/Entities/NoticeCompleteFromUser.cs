using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class NoticeCompleteFromUser
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserIdReceived { get; set; }
        
        public Guid projectId { get; set; }
        public string? Content { get; set; }

        public string? TimeCreate { get; set; }
    }
}
