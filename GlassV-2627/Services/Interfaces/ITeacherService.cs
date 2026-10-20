using GlassV_2627.DTOs.TeacherDTOs;

namespace GlassV_2627.Services.Interfaces;

public interface ITeacherService
{
    public List<TeacherResponseDTO> GetAllTeachers();
    public TeacherResponseDTO? GetOneTeacher(int id);
    public int? Create(TeacherEditDTO teacher);
    public bool Update(TeacherEditDTO teacher);
    public bool Delete(int id);
    public bool UpdateSubject(TeacherUpdateSubjectDTO updateTeacherSubject);
}