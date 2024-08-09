using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class ProjectAttachFile
    {
        [Key]
        public Guid ProjectId { get; set; }


        public Guid attachFileId { get; set; }

    }
}
