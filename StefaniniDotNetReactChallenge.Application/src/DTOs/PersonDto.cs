using StefaniniDotNetReactChallenge.Application.Interfaces;

namespace StefaniniDotNetReactChallenge.Application.DTOs;

public class PersonDto : IPersonBaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateOnly BirthDay { get; set; }
    public string CPF { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string? Nationality { get; set; }
    public string? PlaceOfBirth { get; set; }
}