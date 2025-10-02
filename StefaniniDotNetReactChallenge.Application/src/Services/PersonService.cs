using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Domain.Interfaces;
using StefaniniDotNetReactChallenge.Application.Interfaces;

namespace StefaniniDotNetReactChallenge.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;

    public PersonService(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Person> CreateAsync(Person person)
    {
        // Here you could add domain validations (e.g., CPF unique, email format)
        person.CreatedAt = DateTime.UtcNow;
        person.UpdatedAt = DateTime.UtcNow;
        await _repository.AddAsync(person);
        return person;
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        person.UpdatedAt = DateTime.UtcNow;
        return await _repository.UpdateAsync(person);
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _repository.DeleteByIdAsync(id);
    }
}
