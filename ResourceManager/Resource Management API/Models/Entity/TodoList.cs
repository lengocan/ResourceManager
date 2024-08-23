using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMAPI.Models.Entity;

[Table("TodoList")]
public partial class TodoList
{
    [Key]
    public Guid TodoListId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid UserId { get; set; }

    [Column("taskName")]
    public string? TaskName { get; set; }

    [Column("estimateHour")]
    public string? EstimateHour { get; set; }

    [Column("isCompleted")]
    public bool IsCompleted { get; set; }
}
