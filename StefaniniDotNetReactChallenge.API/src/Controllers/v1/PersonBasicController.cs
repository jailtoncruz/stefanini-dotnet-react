using Microsoft.AspNetCore.Mvc;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Application.Mappers;

namespace StefaniniDotNetReactChallenge.API.Controllers;

public abstract class PersonBasicController : BaseApiController
{
    private readonly IPersonService _service;

    public PersonBasicController(IPersonService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var people = await _service.GetAllAsync();
        return Ok(people.Select(PersonMapper.ToDto));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _service.GetByIdAsync(id);
        return person is not null ? Ok(PersonMapper.ToDto(person)) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteByIdAsync(id);
        return NoContent();
    }
}
