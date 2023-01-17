using Microsoft.EntityFrameworkCore;
using SSRentApi.Models;

namespace SSRentApi.Data
{
    public class ApiDbContext
        : DbContext
    {
        protected readonly IConfiguration Configuration;
        public ApiDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("SsRentApiConn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                       new Category { Id = 1, Name = "Cargo", ImageUrl = "cargo.png" },
                       new Category { Id = 2, Name = "Cruise", ImageUrl = "cruise.png" },
                       new Category { Id = 3, Name = "Voyager", ImageUrl = "voyager.png" }
            );

            modelBuilder.Entity<Port>().HasData(
                       new Port { Id = 1, Name = "Science Base 1", Location = "Jupiter Ganymede" },
                       new Port { Id = 2, Name = "Transfer Alpha", Location = "Juipter IO" },
                       new Port { Id = 3, Name = "Deep Horizon", Location = "Mars Phobos" }
            );

            modelBuilder.Entity<User>().HasData(
              new User { Id = 1, Name = "skati", Email = "skati@email.com", Password = "skati@1234", },
              new User { Id = 2, Name = "deyna", Email = "deyna@email.com", Password = "deyna@1234", },
              new User { Id = 3, Name = "ddata", Email = "data@email.com", Password = "data@1234", }
            );
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}