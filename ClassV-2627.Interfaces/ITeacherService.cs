using GlassV_2627.DTOs.TeacherDTOs;
using GlassV_2627.Filters;

namespace GlassV_2627.Services.Interfaces;

public interface ITeacherService
{
    public List<TeacherResponseDTO> GetAllTeachers(TeacherFilterModel filters);
    public TeacherResponseDTO? GetOneTeacher(int id);
    public int? Create(TeacherEditDTO teacher);
    public bool Update(TeacherEditDTO teacher);
    public bool Delete(int id);
    public bool UpdateSubject(TeacherUpdateSubjectDTO updateTeacherSubject);
}