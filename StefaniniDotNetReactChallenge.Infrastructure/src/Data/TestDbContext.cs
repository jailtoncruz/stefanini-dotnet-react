using Microsoft.EntityFrameworkCore;

namespace StefaniniDotNetReactChallenge.Infrastructure.Data;

public class TestDbContext : AppDbContext
{
    public TestDbContext()
        : base(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}