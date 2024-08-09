using ResourceManager.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceManager.Models.Entities
{
    public class ProjectAssign
    {
       
        public Guid ProjectId { get; set; }
        

        public Guid UserEmployeeId { get; set; }


    }
}
