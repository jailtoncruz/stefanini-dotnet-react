using Microsoft.AspNetCore.Mvc;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Domain.Entities;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[ApiVersion("2.0")]
[Tags("Person")]
[Route("api/v{version:apiVersion}/person")]
public class PersonControllerV2 : PersonControllerV1
{
    private readonly IPersonService _service;

    public PersonControllerV2(IPersonService service)
    : base(service)
    {
        _service = service;
    }

    [HttpPost]
    public override async Task<IActionResult> Create(Person person)
    {
        var created = await _service.CreateAsync(person);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public override async Task<IActionResult> Update(int id, Person person)
    {
        if (id != person.Id) return BadRequest();

        var updated = await _service.UpdateAsync(person);
        return Ok(updated);
    }


}