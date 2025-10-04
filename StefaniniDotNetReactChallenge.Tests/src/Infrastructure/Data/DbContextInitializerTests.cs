using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Infrastructure.Data;
using StefaniniDotNetReactChallenge.Infrastructure.Persistence;

public class DbContextInitializerTests : IDisposable
{
    private readonly AppDbContext _context;

    public DbContextInitializerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
    }

    [Fact]
    public async Task SeedAsync_ShouldAddPeople_WhenDatabaseIsEmpty()
    {
        await DbContextInitializer.SeedAsync(_context);

        var peopleCount = await _context.People.CountAsync();
        peopleCount.Should().Be(12); // You have 12 people in your seed data

        var firstPerson = await _context.People.FirstAsync();
        firstPerson.Name.Should().Be("Maria Silva");
        firstPerson.CPF.Should().Be("12345678901");
        firstPerson.Email.Should().Be("maria.silva@example.com");

        var lastPerson = await _context.People.OrderBy(p => p.Id).LastAsync();
        lastPerson.Name.Should().Be("Diego Santos");
        lastPerson.CPF.Should().Be("22334455667");
    }

    [Fact]
    public async Task SeedAsync_ShouldNotAddPeople_WhenDatabaseAlreadyHasData()
    {
        // Arrange - Add one person first
        var existingPerson = new Person
        {
            Id = 99,
            Name = "Existing Person",
            CPF = "99999999999",
            Email = "existing@test.com",
            BirthDay = new DateOnly(2000, 1, 1),
            Nationality = "Brasileiro",
            PlaceOfBirth = "Test City",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.People.Add(existingPerson);
        await _context.SaveChangesAsync();

        var initialCount = await _context.People.CountAsync();
        initialCount.Should().Be(1);

        // Act - Run seed
        await DbContextInitializer.SeedAsync(_context);

        // Assert - Should still have only the original person (seed should be skipped)
        var finalCount = await _context.People.CountAsync();
        finalCount.Should().Be(1);

        var person = await _context.People.FirstAsync();
        person.Name.Should().Be("Existing Person");
        person.CPF.Should().Be("99999999999");
    }

    [Fact]
    public async Task SeedAsync_ShouldEnsureDatabaseIsCreated()
    {
        await DbContextInitializer.SeedAsync(_context);

        var canConnect = await _context.Database.CanConnectAsync();
        canConnect.Should().BeTrue();

        var people = await _context.People.ToListAsync();
        people.Should().NotBeNull();
    }

    [Fact]
    public async Task SeedAsync_ShouldAddAllExpectedPeopleWithCorrectProperties()
    {
        await DbContextInitializer.SeedAsync(_context);

        var people = await _context.People.ToListAsync();

        people.Should().HaveCount(12);

        var maria = people.First(p => p.Id == 1);
        maria.Should().NotBeNull();
        maria.Name.Should().Be("Maria Silva");
        maria.Gender.Should().Be("Feminino");
        maria.Email.Should().Be("maria.silva@example.com");
        maria.BirthDay.Should().Be(new DateOnly(1999, 11, 20));
        maria.Nationality.Should().Be("Brasileira");
        maria.PlaceOfBirth.Should().Be("São Paulo");
        maria.CPF.Should().Be("12345678901");
        maria.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        maria.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

        var diego = people.First(p => p.Id == 12);
        diego.Name.Should().Be("Diego Santos");
        diego.CPF.Should().Be("22334455667");
    }

    [Fact]
    public async Task SeedAsync_ShouldSetCorrectIdsForAllPeople()
    {
        await DbContextInitializer.SeedAsync(_context);

        var people = await _context.People.OrderBy(p => p.Id).ToListAsync();

        people.Select(p => p.Id).Should().Equal(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}