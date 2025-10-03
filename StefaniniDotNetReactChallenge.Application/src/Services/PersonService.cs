using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Domain.Interfaces;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Application.DTOs;
using StefaniniDotNetReactChallenge.Application.Mappers;

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

    public async Task<Person> CreateAsync(PersonCreateDtoV1 dto)
    {
        Person entity = PersonMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity;
    }
    public async Task<Person> CreateAsync(PersonCreateDtoV2 dto)
    {
        Person entity = PersonMapper.ToEntity(dto);
        await _repository.AddAsync(entity);
        return entity;
    }

    public async Task<Person> UpdateAsync(PersonUpdateDtoV1 dto)
    {
        Person entity = PersonMapper.ToEntity(dto);
        return await _repository.UpdateAsync(entity);
    }
    public async Task<Person> UpdateAsync(PersonUpdateDtoV2 dto)
    {
        Person entity = PersonMapper.ToEntity(dto);
        return await _repository.UpdateAsync(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _repository.DeleteByIdAsync(id);
    }
}
