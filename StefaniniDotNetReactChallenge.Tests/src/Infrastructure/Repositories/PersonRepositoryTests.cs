using FluentAssertions;
using StefaniniDotNetReactChallenge.Infrastructure.Repositories;
using StefaniniDotNetReactChallenge.Infrastructure.Data;
using StefaniniDotNetReactChallenge.Tests.Helpers;
using Microsoft.EntityFrameworkCore;

public class PersonRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllData()
    {
        using var context = new TestDbContext();
        var repository = new PersonRepository(context);
        int total = await context.People.CountAsync();

        var result = await repository.GetAllAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(total);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsPerson_WhenExists()
    {
        using var context = new TestDbContext();
        var repository = new PersonRepository(context);
        var person = PersonFactory.CreatePerson("Test", "00099900099");
        context.People.Add(person);
        await context.SaveChangesAsync();

        var result = await repository.GetByIdAsync(person.Id);

        result.Should().NotBeNull();
        result.Name.Should().Be("Test");
    }

    [Fact]
    public async Task AddAsync_AddsPersonToDatabase()
    {
        using var context = new TestDbContext();
        var repository = new PersonRepository(context);
        var person = PersonFactory.CreatePerson("Test", "00099900099");

        await repository.AddAsync(person);
        await context.SaveChangesAsync();

        context.People.Should().Contain(person);
    }

    [Fact]
    public async Task UpdateAsync_UpdateOneField()
    {
        using var context = new TestDbContext();
        var repository = new PersonRepository(context);
        var person = PersonFactory.CreatePerson("Test_Update", "00099900099");
        context.People.Add(person);
        await context.SaveChangesAsync();

        var newName = Guid.NewGuid().ToString();
        person.Name = newName;

        var result = await repository.UpdateAsync(person);
        await context.SaveChangesAsync();

        result.Should().NotBeNull();
        result.Name.Should().Be(newName);
        result.Id.Should().Be(person.Id);
    }

    [Fact]
    public async Task DeleteByIdAsync_DeletePerson()
    {
        using var context = new TestDbContext();
        var repository = new PersonRepository(context);
        var person = PersonFactory.CreatePerson("Test_Delete", "00099900099");
        context.People.Add(person);
        await context.SaveChangesAsync();

        await repository.DeleteByIdAsync(person.Id);

        context.People.Should().NotContain(person);
    }
}