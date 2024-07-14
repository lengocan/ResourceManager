using Microsoft.EntityFrameworkCore;

namespace ResourceManager.Models.Entities
{
    public class ResourceContext: DbContext
    {     
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAssign> ProjectAssigns { get; set; }
        public ResourceContext()
        {
        }

        public ResourceContext(DbContextOptions<ResourceContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("name=Default");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectAssign>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.UserEmployeeId });
            });
        }

    }
}
