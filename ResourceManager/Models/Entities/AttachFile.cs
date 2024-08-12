using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class AttachFile
    {
        [Key]
        public Guid Id { get; set; }
        
        public string FileName { get; set; }
        public string FilePath { get; set; }
        

        
    }
}
