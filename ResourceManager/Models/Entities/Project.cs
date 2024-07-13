using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class Project
    {
        [Key]
        public Guid ProjectId { get; set; }
        public Guid AssigneeId { get; set; }    //gan khi vao detail
        public string projectName { get; set; } = null!;
        public string projectNumber { get; set; } = null!; //tu gan

        public string status { get; set; } = null!;
        public DateTime createDay { get; set; }
        public DateTime dueDay { get; set; } 
        public string turntime { get; set; } = null!; //tu gan

        public string Branch { get; set; }
        public string priority { get; set; } = null!;
        public string instruction { get; set; } = null!; //gan khi vao detail

        



    }
}
