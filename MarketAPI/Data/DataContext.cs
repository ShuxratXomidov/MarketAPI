using Bogus;
using MarketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var faker = new Faker<User>("en")
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Age, f => f.Random.Number())
                .RuleFor(u => u.City, f => f.Address.City());

            List<User> users = faker.Generate(100);

            int id = 1;
            foreach (User user in users)
            {
                user.Id = id;
                id++;
            }

            modelBuilder.Entity<User>().HasData(users);
            base.OnModelCreating(modelBuilder);
        }
    }
}
