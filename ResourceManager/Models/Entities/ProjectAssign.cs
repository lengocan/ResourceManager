using ResourceManager.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceManager.Models.Entities
{
    public class ProjectAssign
    {
        [Key]
        public Guid ProjectUserId { get; set; }
        public Guid ProjectId { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        
        public Project project { get; set; }
        
        public UserEmployee user { get; set; }  

    }
}
