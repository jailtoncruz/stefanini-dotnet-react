using Microsoft.AspNetCore.Mvc;
using StefaniniDotNetReactChallenge.Application.DTOs;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Application.Mappers;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/person")]
[Tags("Person")]
public class PersonControllerV1 : PersonBasicController
{
    private readonly IPersonService _service;

    public PersonControllerV1(IPersonService service)
    : base(service)
    {
        _service = service;
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] PersonCreateDtoV1 dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id },
        PersonMapper.ToDto(created));
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(int id, [FromBody] PersonUpdateDtoV1 dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return Ok(PersonMapper.ToDto(updated));
    }
}