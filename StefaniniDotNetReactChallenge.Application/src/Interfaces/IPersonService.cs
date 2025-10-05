using StefaniniDotNetReactChallenge.Application.DTOs;
using StefaniniDotNetReactChallenge.Domain.Entities;

namespace StefaniniDotNetReactChallenge.Application.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);

    Task<Person> CreateAsync(PersonCreateDtoV1 dto);
    Task<Person> CreateAsync(PersonCreateDtoV2 dto);

    Task<Person> UpdateAsync(int Id, PersonUpdateDtoV1 dto);
    Task<Person> UpdateAsync(int Id, PersonUpdateDtoV2 dto);

    Task DeleteByIdAsync(int id);
}
