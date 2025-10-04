using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Application.DTOs;

namespace StefaniniDotNetReactChallenge.Tests.Helpers;

public class PersonFactory
{
    public static Person CreatePerson(string Name, string CPF)
    {
        return new Person
        {
            Id = 1,
            Name = Name,
            BirthDay = new DateOnly(),
            CPF = CPF,
            CreatedAt = new DateTime(),
            UpdatedAt = new DateTime()
        };
    }

    public static PersonCreateDtoV1 CreatePersonCreateDtoV1()
    {
        return new PersonCreateDtoV1
        {
            Name = Guid.NewGuid().ToString(),
            BirthDay = new DateOnly(),
            CPF = "",
            Email = "",
            Gender = "",
            Nationality = "",
            PlaceOfBirth = ""
        };
    }

    public static PersonCreateDtoV2 CreatePersonCreateDtoV2()
    {
        return new PersonCreateDtoV2
        {
            Name = Guid.NewGuid().ToString(),
            BirthDay = new DateOnly(),
            CPF = "",
            Email = "",
            Gender = "",
            Nationality = "",
            PlaceOfBirth = ""
        };
    }

    public static PersonUpdateDtoV1 CreatePersonUpdateDTOv1()
    {
        return new PersonUpdateDtoV1
        {
            Name = Guid.NewGuid().ToString(),
            BirthDay = new DateOnly(),
            CPF = "",
            Email = "",
            Gender = "",
            Nationality = "",
            PlaceOfBirth = ""
        };
    }

    public static PersonUpdateDtoV2 CreatePersonUpdateDTOv2()
    {
        return new PersonUpdateDtoV2
        {
            Name = Guid.NewGuid().ToString(),
            BirthDay = new DateOnly(),
            CPF = "",
            Email = "",
            Gender = "",
            Nationality = "",
            PlaceOfBirth = ""
        };
    }
}