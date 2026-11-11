using GlassV_2627.DTOs.StudentDTOs;
using GlassV_2627.DTOs.TeacherDTOs;
using GlassV_2627.Filters;
using GlassV_2627.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlassV_2627.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _service;
    
    public TeachersController(ITeacherService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> GetAllTeachers([FromBody] TeacherFilterModel filters)
    {
        var teachers = await _service.GetAllTeachers(filters);
        return Ok(teachers);
    }

    [HttpGet]
    public async Task<IActionResult> GetOneTeacher([FromQuery] int id)
    {
        var teacher = await _service.GetOneTeacher(id);

        if (teacher == null)
        {
            return NotFound();
        }
        
        return Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TeacherEditDTO teacher)
    {
        var entity = await _service.Create(teacher);

        if (entity == null)
        {
            return BadRequest();
        }
        
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TeacherEditDTO teacher)
    {
        var entity = await _service.Update(teacher);
        
        if (!entity)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
       var entity = await _service.Delete(id);

       if (!entity)
       {
           return NotFound();
       }
       
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateSubject([FromBody] TeacherUpdateSubjectDTO teacher)
    {
        var entity = await _service.UpdateSubject(teacher);
        
        if (!entity)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}