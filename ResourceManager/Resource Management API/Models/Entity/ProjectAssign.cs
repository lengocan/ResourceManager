using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMAPI.Models.Entity;

[PrimaryKey("ProjectId", "UserEmployeeId")]
public partial class ProjectAssign
{
    [Key]
    public Guid ProjectId { get; set; }

    [Key]
    public Guid UserEmployeeId { get; set; }
}
