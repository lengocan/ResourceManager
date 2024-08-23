using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RMAPI.Models.Entity;

[Index("NormalizedEmail", Name = "EmailIndex")]
public partial class AspNetUser
{
    [Key]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    public string FullName { get; set; } = null!;

    [Column("dob")]
    [StringLength(50)]
    public string Dob { get; set; } = null!;

    [Column("address")]
    [StringLength(150)]
    public string Address { get; set; } = null!;

    [Column("dayJoin")]
    [StringLength(50)]
    public string DayJoin { get; set; } = null!;

    [Column("team")]
    [StringLength(50)]
    public string Team { get; set; } = null!;

    public bool IsActive { get; set; }

    [StringLength(256)]
    public string? UserName { get; set; }

    [StringLength(256)]
    public string? NormalizedUserName { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    [StringLength(256)]
    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
