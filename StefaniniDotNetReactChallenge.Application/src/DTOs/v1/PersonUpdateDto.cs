using System.ComponentModel.DataAnnotations;

namespace StefaniniDotNetReactChallenge.Application.DTOs;

public class PersonUpdateDtoV1 : PersonBaseMutationDto
{
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public override string? Email { get; set; }
}