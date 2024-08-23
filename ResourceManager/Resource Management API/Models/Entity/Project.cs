using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMAPI.Models.Entity;

public partial class Project
{
    [Key]
    public Guid ProjectId { get; set; }

    public Guid AssigneeId { get; set; }

    [Column("projectName")]
    public string ProjectName { get; set; } = null!;

    [Column("projectNumber")]
    public string ProjectNumber { get; set; } = null!;

    [Column("status")]
    public string Status { get; set; } = null!;

    [Column("createDay")]
    public DateTime CreateDay { get; set; }

    [Column("dueDay")]
    public DateTime DueDay { get; set; }

    [Column("turntime")]
    public string Turntime { get; set; } = null!;

    public string Branch { get; set; } = null!;

    [Column("priority")]
    public string Priority { get; set; } = null!;

    [Column("instruction")]
    public string Instruction { get; set; } = null!;
}
