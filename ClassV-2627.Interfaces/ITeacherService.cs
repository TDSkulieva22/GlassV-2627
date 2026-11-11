using GlassV_2627.DTOs.TeacherDTOs;
using GlassV_2627.Filters;

namespace GlassV_2627.Services.Interfaces;

public interface ITeacherService
{
    public Task<List<TeacherResponseDTO>> GetAllTeachers(TeacherFilterModel filters);
    public Task<TeacherResponseDTO?> GetOneTeacher(int id);
    public Task<int?> Create(TeacherEditDTO teacher);
    public Task<bool> Update(TeacherEditDTO teacher);
    public Task<bool> Delete(int id);
    public Task<bool> UpdateSubject(TeacherUpdateSubjectDTO updateTeacherSubject);
}