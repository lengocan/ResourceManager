using Microsoft.AspNetCore.Identity;
using ResourceManager.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResourceManager.Areas.Identity.Data
{
    public class UserEmployee:IdentityUser
    {      
        public string FullName { get; set; } = null!;
        public string dob { get; set; } = null!;
        public string address { get; set; } = null!;

        public string dayJoin { get; set; } = null!;
        public string team { get; set; } = null!;
        public bool IsActive { get; set; }

        

    }
}
