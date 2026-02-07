using Microsoft.EntityFrameworkCore;
using SP_API_MCP.DTOs;

namespace SP_API_MCP.Repository
{
    public class DocsDBContext : DbContext
    {
        public DbSet<Doc> Docs { get; set; }

        public DocsDBContext(DbContextOptions<DocsDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doc>().ToTable("docs");
            modelBuilder.Entity<Doc>().HasKey(d => d.Id);
            modelBuilder.Entity<Doc>().Property(d => d.Path).IsRequired();
            modelBuilder.Entity<Doc>().Property(d => d.Title).IsRequired();
            modelBuilder.Entity<Doc>().Property(d => d.Content).IsRequired();
        }
    }
}
