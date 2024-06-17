
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using ResourceManager.Areas.Identity.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ResourceManager.Data;

public class UserIdentityContext : IdentityDbContext<IdentityUser>
{
    public UserIdentityContext(DbContextOptions<UserIdentityContext> options)
        : base(options)
    {
    }

    public override DatabaseFacade Database => base.Database;

    public override ChangeTracker ChangeTracker => base.ChangeTracker;

    public override IModel Model => base.Model;

    public override DbContextId ContextId => base.ContextId;

    public override DbSet<IdentityUser> Users { get => base.Users; set => base.Users = value; }
    public override DbSet<IdentityUserClaim<string>> UserClaims { get => base.UserClaims; set => base.UserClaims = value; }
    public override DbSet<IdentityUserLogin<string>> UserLogins { get => base.UserLogins; set => base.UserLogins = value; }
    public override DbSet<IdentityUserToken<string>> UserTokens { get => base.UserTokens; set => base.UserTokens = value; }
    public override DbSet<IdentityUserRole<string>> UserRoles { get => base.UserRoles; set => base.UserRoles = value; }
    public override DbSet<IdentityRole> Roles { get => base.Roles; set => base.Roles = value; }
    public override DbSet<IdentityRoleClaim<string>> RoleClaims { get => base.RoleClaims; set => base.RoleClaims = value; }

    protected override Version SchemaVersion => base.SchemaVersion;

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        return base.Add(entity);
    }

    public override EntityEntry Add(object entity)
    {
        return base.Add(entity);
    }

    public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
    {
        return base.AddAsync(entity, cancellationToken);
    }

    public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
    {
        return base.AddAsync(entity, cancellationToken);
    }

    public override void AddRange(params object[] entities)
    {
        base.AddRange(entities);
    }

    public override void AddRange(IEnumerable<object> entities)
    {
        base.AddRange(entities);
    }

    public override Task AddRangeAsync(params object[] entities)
    {
        return base.AddRangeAsync(entities);
    }

    public override Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
    {
        return base.AddRangeAsync(entities, cancellationToken);
    }

    public override EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
    {
        return base.Attach(entity);
    }

    public override EntityEntry Attach(object entity)
    {
        return base.Attach(entity);
    }

    public override void AttachRange(params object[] entities)
    {
        base.AttachRange(entities);
    }

    public override void AttachRange(IEnumerable<object> entities)
    {
        base.AttachRange(entities);
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    public override ValueTask DisposeAsync()
    {
        return base.DisposeAsync();
    }

    public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
    {
        return base.Entry(entity);
    }

    public override EntityEntry Entry(object entity)
    {
        return base.Entry(entity);
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override object? Find([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] Type entityType, params object?[]? keyValues)
    {
        return base.Find(entityType, keyValues);
    }

    public override TEntity? Find<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>(params object?[]? keyValues) where TEntity : class
    {
        return base.Find<TEntity>(keyValues);
    }

    public override ValueTask<object?> FindAsync([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] Type entityType, params object?[]? keyValues)
    {
        return base.FindAsync(entityType, keyValues);
    }

    public override ValueTask<object?> FindAsync([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] Type entityType, object?[]? keyValues, CancellationToken cancellationToken)
    {
        return base.FindAsync(entityType, keyValues, cancellationToken);
    }

    public override ValueTask<TEntity?> FindAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>(params object?[]? keyValues) where TEntity : class
    {
        return base.FindAsync<TEntity>(keyValues);
    }

    public override ValueTask<TEntity?> FindAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>(object?[]? keyValues, CancellationToken cancellationToken) where TEntity : class
    {
        return base.FindAsync<TEntity>(keyValues, cancellationToken);
    }

    public override IQueryable<TResult> FromExpression<TResult>(Expression<Func<IQueryable<TResult>>> expression)
    {
        return base.FromExpression(expression);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
    {
        return base.Remove(entity);
    }

    public override EntityEntry Remove(object entity)
    {
        return base.Remove(entity);
    }

    public override void RemoveRange(params object[] entities)
    {
        base.RemoveRange(entities);
    }

    public override void RemoveRange(IEnumerable<object> entities)
    {
        base.RemoveRange(entities);
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>()
    {
        return base.Set<TEntity>();
    }

    public override DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>(string name)
    {
        return base.Set<TEntity>(name);
    }

    public override string? ToString()
    {
        return base.ToString();
    }

    public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
    {
        return base.Update(entity);
    }

    public override EntityEntry Update(object entity)
    {
        return base.Update(entity);
    }

    public override void UpdateRange(params object[] entities)
    {
        base.UpdateRange(entities);
    }

    public override void UpdateRange(IEnumerable<object> entities)
    {
        base.UpdateRange(entities);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserEmployee>(entity =>
        {
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.dob).HasMaxLength(50);
            entity.Property(e => e.address).HasMaxLength(150);
            entity.Property(e => e.dayJoin).HasMaxLength(50);
            entity.Property(e => e.team).HasMaxLength(50);
            entity.Property(e => e.IsActive);
        });
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
