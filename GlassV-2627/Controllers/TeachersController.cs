using GlassV_2627.Data;
using GlassV_2627.DTOs.StudentDTOs;
using GlassV_2627.DTOs.TeacherDTOs;
using GlassV_2627.Filters;
using GlassV_2627.Models;
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
    public IActionResult GetAllTeachers([FromBody] TeacherFilterModel filters)
    {
        var teachers = _service.GetAllTeachers(filters);
        return Ok(teachers);
    }

    [HttpGet]
    public IActionResult GetOneTeacher([FromQuery] int id)
    {
        var teacher = _service.GetOneTeacher(id);

        if (teacher == null)
        {
            return NotFound();
        }
        
        return Ok(teacher);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TeacherEditDTO teacher)
    {
        var entity = _service.Create(teacher);

        if (entity == null)
        {
            return BadRequest();
        }
        
        return Created();
    }

    [HttpPut]
    public IActionResult Update([FromBody] TeacherEditDTO teacher)
    {
        var entity = _service.Update(teacher);
        
        if (!entity)
        {
            return BadRequest();
        }
        
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] int id)
    {
       var entity = _service.Delete(id);

       if (!entity)
       {
           return NotFound();
       }
       
        return Ok();
    }

    [HttpPatch]
    public IActionResult UpdateSubject([FromBody] TeacherUpdateSubjectDTO teacher)
    {
        var entity = _service.UpdateSubject(teacher);
        
        if (!entity)
        {
            return BadRequest();
        }
        
        return Ok();
    }
}