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

            modelBuilder.Entity<ProjectAssign>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId });

            modelBuilder.Entity<ProjectAssign>()
                .HasOne(pu => pu.project)
                .WithMany()
                .HasForeignKey(pu => pu.ProjectId);

            modelBuilder.Entity<ProjectAssign>()
                .HasOne(pu => pu.user)
                .WithMany()
                .HasForeignKey(pu => pu.UserId);
        }
    }
}
