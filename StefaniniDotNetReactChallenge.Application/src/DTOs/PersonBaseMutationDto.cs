using System.ComponentModel.DataAnnotations;
using StefaniniDotNetReactChallenge.Application.Validations;
using StefaniniDotNetReactChallenge.Application.Interfaces;

namespace StefaniniDotNetReactChallenge.Application.DTOs;

public abstract class PersonBaseMutationDto : IPersonBaseDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name can't exceed 100 characters")]
    public virtual string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Birthday is required")]
    public virtual DateOnly BirthDay { get; set; }

    [Required(ErrorMessage = "CPF is required")]
    [Cpf(ErrorMessage = "Invalid CPF number")]
    public virtual string CPF { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email can't exceed 100 characters")]
    public virtual string? Email { get; set; }

    public string? Gender { get; set; }
    public string? Nationality { get; set; }
    public string? PlaceOfBirth { get; set; }
}