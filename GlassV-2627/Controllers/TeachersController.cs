using GlassV_2627.Data;
using GlassV_2627.DTOs.StudentDTOs;
using GlassV_2627.DTOs.TeacherDTOs;
using GlassV_2627.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlassV_2627.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TeachersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public TeachersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllTeachers()
    {
        List<TeacherResponseDTO> teachers = _context.Teachers.Select(x => new TeacherResponseDTO()
        {
            Name = x.Name,
            Subject = x.Subject
        }).ToList();
        
        return Ok(teachers);
    }

    [HttpGet]
    public IActionResult GetOneTeacher([FromQuery] int id)
    {
        TeacherResponseDTO teacher = _context.Teachers.Where(x => x.Id == id).Select(x => new TeacherResponseDTO()
        {
            Name = x.Name,
            Subject = x.Subject
        }).First();
        
        return Ok(teacher);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TeacherEditDTO teacher)
    {
        Teacher entity = new Teacher();
        
        entity.Name = teacher.Name;
        entity.Subject = teacher.Subject;
        
        _context.Teachers.Add(entity);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] TeacherEditDTO teacher)
    {
        Teacher entity = _context.Teachers.Where(x => x.Id == teacher.Id).First();
        
        entity.Name = teacher.Name;
        entity.Subject = teacher.Subject;

        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] int id)
    {
        Teacher teacher = _context.Teachers.Where(x => x.Id == id).First();

        _context.Teachers.Remove(teacher);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPatch]
    public IActionResult Patch([FromBody] TeacherUpdateSubjectDTO teacher)
    {
        Teacher entity = _context.Teachers.Where(x => x.Id == teacher.TeacherId).First();

        entity.Subject = teacher.NewSubject;
        _context.SaveChanges();
        return Ok();
    }
}