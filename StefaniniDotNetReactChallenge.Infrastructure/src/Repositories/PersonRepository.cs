using Microsoft.EntityFrameworkCore;
using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Domain.Interfaces;
using StefaniniDotNetReactChallenge.Infrastructure.Data;

namespace StefaniniDotNetReactChallenge.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.People
            .OrderByDescending(p => p.UpdatedAt)
            .ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.People.FindAsync(id);
    }

    public async Task AddAsync(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        _context.People.Update(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person is not null)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }
    }
}
