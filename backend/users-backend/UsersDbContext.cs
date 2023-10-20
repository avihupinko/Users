using Microsoft.EntityFrameworkCore;
using users_backend.Data;
using users_backend.Models;

namespace users_backend
{
    public class UsersDbContext: DbContext
    {

        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Define DB assemblys
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersTypeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
