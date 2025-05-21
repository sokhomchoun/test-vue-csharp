using Microsoft.EntityFrameworkCore;
using backend.Models.Entities;
namespace backend.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Test> tests { get; set; }
        public DbSet<User> users { get; set; }
    }
}
