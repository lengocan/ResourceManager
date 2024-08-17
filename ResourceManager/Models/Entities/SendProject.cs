using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class SendProject
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId {  get; set; }   
        public Guid projectId {  get; set; }   
     



        public string? timeSend { get; set; }

        
        public bool isAccept { get; set; } = false;
    }
}
