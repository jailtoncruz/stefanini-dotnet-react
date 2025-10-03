using System.ComponentModel.DataAnnotations;

namespace StefaniniDotNetReactChallenge.Application.DTOs;

public class PersonUpdateDtoV2 : PersonBaseMutationDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(100, ErrorMessage = "Email can't exceed 100 characters")]
    public override string? Email { get; set; }
}