using StefaniniDotNetReactChallenge.Domain.Entities;

namespace StefaniniDotNetReactChallenge.Application.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task<Person> CreateAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    Task DeleteByIdAsync(int id);
}
