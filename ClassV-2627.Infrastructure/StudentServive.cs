using GlassV_2627.Data;
using GlassV_2627.DTOs.StudentDTOs;
using GlassV_2627.Filters;
using GlassV_2627.Models;
using GlassV_2627.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlassV_2627.Services;
public class StudentServive : IStudentService
{
    private readonly ApplicationDbContext _context;
    
    public StudentServive(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentResponseDTO>> GetAll(StudentFilterModel filters)
    {
        IQueryable<Student> baseQuery = _context.Students;

        if (filters.Grade.HasValue)
        {
            baseQuery = baseQuery.Where(x => x.Grade == filters.Grade.Value);
        }

        if (filters.Age.HasValue)
        {
            baseQuery = baseQuery.Where(x => x.Age == filters.Age.Value);
        }

        if (!string.IsNullOrEmpty(filters.FirstName))
        {
            baseQuery = baseQuery.Where(x => x.FirstName.Contains(filters.FirstName));
        }

        if (!string.IsNullOrEmpty(filters.LastName))
        {
            baseQuery = baseQuery.Where(x => x.LastName.Contains(filters.LastName));
        }

        baseQuery = baseQuery.Skip((filters.Page - 1) * filters.PageSize).Take(filters.PageSize);

        IQueryable<StudentResponseDTO> query = baseQuery.Select(x => new StudentResponseDTO()
        {
            Id = x.Id,
            Names = x.FirstName + " " + x.LastName,
            Age = x.Age,
        });

        return await query.ToListAsync();
    }

    public async Task<StudentResponseDTO?> GetOne(int id)
    {
        StudentResponseDTO? student = await _context.Students.Where(x => x.Id == id).Select(x => new StudentResponseDTO()
        {
            Id = x.Id,
            Age = x.Age,
            Names = x.FirstName + " " + x.LastName
        }).FirstOrDefaultAsync();

        return student;
    }

    public async Task<int?> Create(StudentEditDTO student)
    {
        Student entity = new Student();
        
        entity.FirstName = student.FirstName;
        entity.LastName = student.LastName;
        entity.Age = student.Age;
        entity.Username = student.Username;
        entity.Grade = 0;
        entity.Password = "TempPassword";

        try
        {
            _context.Students.Add(entity);
           await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return entity.Id;
    }

    public async Task<bool> Update(StudentEditDTO student)
    {
        Student? entity = await _context.Students.Where(x => x.Id == student.Id).FirstOrDefaultAsync();

        if (entity == null)
        {
            return false;
        }
        
        entity.FirstName = student.FirstName;
        entity.LastName = student.LastName;
        entity.Age = student.Age;
        entity.Username = student.Username;
        
        bool result = await _context.SaveChangesAsync() == 1;
        
        return result;
    }

    public async Task<bool> Delete(int id)
    {
        Student? entity = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity == null)
        {
            return false;
        }
        
        _context.Students.Remove(entity);
        
        bool result = await _context.SaveChangesAsync() == 1;

        return result;
    }

    public async Task<bool> UpdateGrade(StudentUpdateGradeDTO updateStudent)
    {
        Student entity = await _context.Students.Where(x => x.Id == updateStudent.StudentId).FirstAsync();

        entity.Grade = updateStudent.NewGrade;
        
        bool result = await _context.SaveChangesAsync() == 1;
        
        return result;
    }
    
}