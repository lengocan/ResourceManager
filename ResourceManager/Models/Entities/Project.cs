using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Models.Entities
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;


        public string dob { get; set; } = null!;
        public bool IsActive { get; set; }
        public string dayJoin { get; set; } = null!;
        public string team { get; set; } = null!;
        public string role { get; set; } = null!;


    }
}
