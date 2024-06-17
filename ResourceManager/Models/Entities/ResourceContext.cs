using Microsoft.EntityFrameworkCore;

namespace ResourceManager.Models.Entities
{
    public class ResourceContext: DbContext
    {

        public DbSet<Employee> Employees {  get; set; }
        public DbSet<Project> Projects { get; set; }
        public ResourceContext()
        {
        }

        public ResourceContext(DbContextOptions<ResourceContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("name=Default");
    }
}
