using StefaniniDotNetReactChallenge.Domain.Entities;

namespace StefaniniDotNetReactChallenge.Domain.Interfaces;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task AddAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    Task DeleteByIdAsync(int id);
}
