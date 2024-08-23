using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMAPI.Models.Entity;

public partial class Employee
{
    [Key]
    public Guid Id { get; set; }
}
