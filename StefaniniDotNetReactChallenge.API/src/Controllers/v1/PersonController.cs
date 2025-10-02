using Microsoft.AspNetCore.Mvc;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Domain.Entities;

namespace StefaniniDotNetReactChallenge.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/person")]
[Tags("Person")]
public class PersonControllerV1 : BaseApiController
{
    private readonly IPersonService _service;

    public PersonControllerV1(IPersonService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var people = await _service.GetAllAsync();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _service.GetByIdAsync(id);
        return person is not null ? Ok(person) : NotFound();
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create(Person person)
    {
        var created = await _service.CreateAsync(person);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(int id, Person person)
    {
        if (id != person.Id) return BadRequest();

        var updated = await _service.UpdateAsync(person);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteByIdAsync(id);
        return NoContent();
    }
}