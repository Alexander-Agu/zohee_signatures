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
    }
}
