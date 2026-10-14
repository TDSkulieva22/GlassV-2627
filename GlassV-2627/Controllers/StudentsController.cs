using GlassV_2627.Data;
using GlassV_2627.DTOs.StudentDTOs;
using GlassV_2627.Models;
using Microsoft.AspNetCore.Mvc;

namespace GlassV_2627.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<StudentResponseDTO> student = _context.Students.Select(x => new StudentResponseDTO()
        {
            Id = x.Id,
            Age = x.Age,
            Names = x.FirstName + " " + x.LastName,

        }).ToList<StudentResponseDTO>();
        
        return Ok(student);
    }

    [HttpGet]
    public IActionResult GetOne([FromQuery] int id)
    {
        StudentResponseDTO student = _context.Students.Where(x => x.Id == id).Select(x => new StudentResponseDTO()
        {
            Id = x.Id,
            Age = x.Age,
            Names = x.FirstName + " " + x.LastName
        }).First();
        
        return Ok(student);
    }

    [HttpPost]
    public IActionResult Create([FromBody] StudentEditDTO student)
    {
        Student entity = new Student();
        entity.FirstName = student.FirstName;
        entity.LastName = student.LastName;
        entity.Age = student.Age;
        entity.Username = student.Username;
        entity.Grade = 0;
        entity.Password = "TempPassword";
        
        _context.Students.Add(entity);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] StudentEditDTO student)
    {
        Student entity = _context.Students.Where(x => x.Id == student.Id).First();
        
        entity.FirstName = student.FirstName;
        entity.LastName = student.LastName;
        entity.Age = student.Age;
        entity.Username = student.Username;
        
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] int id)
    {
        Student entity = _context.Students.Where(x => x.Id == id).First();
        
        _context.Students.Remove(entity);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPatch]
    public IActionResult UpdateGrade([FromBody] StudentUpdateGradeDTO updateStudent)
    {
        Student entity = _context.Students.Where(x => x.Id == updateStudent.StudentId).First();

        entity.Grade = updateStudent.NewGrade;
        _context.SaveChanges();
        return Ok();
    }
}