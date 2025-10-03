using Microsoft.AspNetCore.Mvc;
using StefaniniDotNetReactChallenge.Application.DTOs;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Application.Mappers;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/person")]
[Tags("Person")]
public class PersonControllerV2 : PersonBasicController
{
    private readonly IPersonService _service;

    public PersonControllerV2(IPersonService service)
    : base(service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonCreateDtoV2 dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById),
            new { id = created.Id },
            PersonMapper.ToDto(created));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PersonUpdateDtoV2 person)
    {
        var updated = await _service.UpdateAsync(person);
        return Ok(PersonMapper.ToDto(updated));
    }
}