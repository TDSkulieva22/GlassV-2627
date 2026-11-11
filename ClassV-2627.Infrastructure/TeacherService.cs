using GlassV_2627.Data;
using GlassV_2627.DTOs.TeacherDTOs;
using GlassV_2627.Filters;
using GlassV_2627.Models;
using GlassV_2627.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GlassV_2627.Services;

public class TeacherService : ITeacherService
{
    private readonly ApplicationDbContext _context;

    public TeacherService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<TeacherResponseDTO>> GetAllTeachers(TeacherFilterModel filters)
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
        
        return await query.ToListAsync();
    }

    public async Task<TeacherResponseDTO?> GetOneTeacher(int id)
    {
        TeacherResponseDTO? teacher = await _context.Teachers.Where(x => x.Id == id).Select(x => new TeacherResponseDTO()
        {
            Name = x.Name,
            Subject = x.Subject
        }).FirstOrDefaultAsync();
        
        return teacher;
    }

    public async Task<int?> Create(TeacherEditDTO teacher)
    {
        Teacher entity = new Teacher();
        
        entity.Name = teacher.Name;
        entity.Subject = teacher.Subject;

        try
        {
            _context.Teachers.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return entity.Id;
    }

    public async Task<bool> Update(TeacherEditDTO teacher)
    {
        Teacher? entity = await _context.Teachers.Where(x => x.Id == teacher.Id).FirstOrDefaultAsync();

        if (entity == null)
        {
            return false;
        }
        
        entity.Name = teacher.Name;
        entity.Subject = teacher.Subject;

        bool result = await _context.SaveChangesAsync() == 1;
        return result;
    }

    public async Task<bool> Delete(int id)
    {
        Teacher? teacher = await _context.Teachers.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (teacher == null)
        {
            return false;
        }
        
        _context.Teachers.Remove(teacher);
        
        bool result = await _context.SaveChangesAsync() == 1;
        return result;
    }

    public async Task<bool> UpdateSubject(TeacherUpdateSubjectDTO updateTeacherSubject)
    {
        Teacher entity = await _context.Teachers.Where(x => x.Id == updateTeacherSubject.TeacherId).FirstAsync();
        
        entity.Subject = updateTeacherSubject.NewSubject;
        
        bool result = await _context.SaveChangesAsync() == 1;
        return result;
    }
}