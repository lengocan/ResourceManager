using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class TodoList
    {
        [Key]
        public Guid TodoListId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }    

        public string? taskName { get; set; }
        public string? estimateHour { get; set;}
        public bool isCompleted { get; set; }
    }
}
