namespace StefaniniDotNetReactChallenge.Application.Interfaces;

public interface IPersonBaseDto
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public DateOnly BirthDay { get; set; }
    public string CPF { get; set; }
    public string? Gender { get; set; }
    public string? Nationality { get; set; }
    public string? PlaceOfBirth { get; set; }
}