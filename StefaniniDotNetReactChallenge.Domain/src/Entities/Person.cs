namespace StefaniniDotNetReactChallenge.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Gender { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public required DateOnly BirthDay { get; set; }
    public string? Nationality { get; set; } = string.Empty;
    public string? PlaceOfBirth { get; set; } = string.Empty;
    public required string CPF { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
}
