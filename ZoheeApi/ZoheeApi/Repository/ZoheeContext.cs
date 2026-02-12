using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using ZoheeApi.Entities;

namespace ZoheeApi.Repository
{
    public class ZoheeContext : DbContext
    {
        public ZoheeContext(DbContextOptions<ZoheeContext> options)
            : base(options)
        {   
            

        }

        public DbSet<Documents> documents => Set<Documents>();
        public DbSet<User> users => Set<User>();
        public DbSet<Template> templates => Set<Template>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Documents)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
