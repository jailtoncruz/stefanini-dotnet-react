using System.ComponentModel.DataAnnotations;

namespace StefaniniDotNetReactChallenge.Application.DTOs;

public class PersonCreateDtoV1 : PersonBaseMutationDto
{
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public override string? Email { get; set; }
}