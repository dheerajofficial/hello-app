using Microsoft.AspNetCore.Mvc;
using PersonApp.Models;

namespace PersonApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    bool isFileRead = false;

    public PersonController(ILogger<PersonController> logger)
    {
        _logger = logger;
        if (isFileRead == false)
        {
            PersonService.ReadFromJsonFile();
            isFileRead = true;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        await Task.FromResult(0);

        var persons = PersonService.Get();

        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        await Task.FromResult(0);

        var persons = PersonService.Get().Where(p => p.Id == id).ToList<Person>();

        return Ok(persons);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Person person)
    {
        await Task.FromResult(0);
        person.Id = Guid.NewGuid();
        PersonService.Create(person);
        var isSaved = PersonService.SaveToJsonFile();
        return CreatedAtAction(nameof(CreateAsync), new { id = person.Id }, person);
    }


}
