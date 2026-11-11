using GlassV_2627.DTOs.StudentDTOs;
using GlassV_2627.Filters;
using GlassV_2627.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlassV_2627.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;
    
    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] StudentFilterModel filters)
    {
        var result = await _service.GetAll(filters);
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetOne([FromQuery] int id)
    {
        var result = await _service.GetOne(id);

        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentEditDTO newStudent)
    {
        var result = await _service.Create(newStudent);

        if (result == null)
        {
            return BadRequest();
        }
        
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] StudentEditDTO student)
    {
        var result = await _service.Update(student);

        if (!result)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _service.Delete(id);

        if (!result)
        {
            return NotFound();
        }
        
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateGrade([FromBody] StudentUpdateGradeDTO updateStudent)
    {
        var result = await _service.UpdateGrade(updateStudent);

        if (!result)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}