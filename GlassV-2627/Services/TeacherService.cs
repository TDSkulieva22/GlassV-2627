using GlassV_2627.Data;
using GlassV_2627.DTOs.TeacherDTOs;
using GlassV_2627.Filters;
using GlassV_2627.Models;
using GlassV_2627.Services.Interfaces;

namespace GlassV_2627.Services;

public class TeacherService : ITeacherService
{
    private readonly ApplicationDbContext _context;

    public TeacherService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public List<TeacherResponseDTO> GetAllTeachers(TeacherFilterModel filters)
    {
        IQueryable<Teacher> teachers = _context.Teachers;

        if (!string.IsNullOrEmpty(filters.Name))
        {
            teachers = teachers.Where(x => x.Name.Contains(filters.Name));
        }

        if (!string.IsNullOrEmpty(filters.Subject))
        {
            teachers = teachers.Where(x => x.Subject.Contains(filters.Subject));
        }
          
        teachers = teachers.Skip((filters.Page - 1) * filters.PageSize).Take(filters.PageSize);

        IQueryable<TeacherResponseDTO> query = teachers.Select(x => new TeacherResponseDTO()
        {
            Name = x.Name,
            Subject = x.Subject,
        });
        
        return query.ToList();
    }

    public TeacherResponseDTO? GetOneTeacher(int id)
    {
        TeacherResponseDTO? teacher = _context.Teachers.Where(x => x.Id == id).Select(x => new TeacherResponseDTO()
        {
            Name = x.Name,
            Subject = x.Subject
        }).FirstOrDefault();
        
        return teacher;
    }

    public int? Create(TeacherEditDTO teacher)
    {
        Teacher entity = new Teacher();
        
        entity.Name = teacher.Name;
        entity.Subject = teacher.Subject;

        try
        {
            _context.Teachers.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return entity.Id;
    }

    public bool Update(TeacherEditDTO teacher)
    {
        Teacher? entity = _context.Teachers.Where(x => x.Id == teacher.Id).FirstOrDefault();

        if (entity == null)
        {
            return false;
        }
        
        entity.Name = teacher.Name;
        entity.Subject = teacher.Subject;

        bool result = _context.SaveChanges() == 1;
        return result;
    }

    public bool Delete(int id)
    {
        Teacher? teacher = _context.Teachers.Where(x => x.Id == id).FirstOrDefault();

        if (teacher == null)
        {
            return false;
        }
        
        _context.Teachers.Remove(teacher);
        
        bool result = _context.SaveChanges() == 1;
        return result;
    }

    public bool UpdateSubject(TeacherUpdateSubjectDTO updateTeacherSubject)
    {
        Teacher entity = _context.Teachers.Where(x => x.Id == updateTeacherSubject.TeacherId).First();
        
        entity.Subject = updateTeacherSubject.NewSubject;
        
        bool result = _context.SaveChanges() == 1;
        return result;
    }
}