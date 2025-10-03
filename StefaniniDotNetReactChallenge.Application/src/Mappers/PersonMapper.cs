
using StefaniniDotNetReactChallenge.Application.DTOs;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Domain.Entities;

namespace StefaniniDotNetReactChallenge.Application.Mappers;

public static class PersonMapper
{
    public static PersonDto ToDto(Person entity)
    {
        return new PersonDto
        {
            Id = entity.Id,
            Name = entity.Name,
            CPF = entity.CPF,
            BirthDay = entity.BirthDay,
            Email = entity.Email,
            Gender = entity.Gender,
            Nationality = entity.Nationality,
            PlaceOfBirth = entity.PlaceOfBirth,
        };
    }
    public static Person ToEntity(IPersonBaseDto dto)
    {
        return new Person
        {
            Name = dto.Name,
            Email = dto.Email,
            CPF = dto.CPF,
            BirthDay = dto.BirthDay,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
