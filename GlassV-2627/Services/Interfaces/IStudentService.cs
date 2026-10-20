using GlassV_2627.DTOs.StudentDTOs;

namespace GlassV_2627.Services.Interfaces;

public interface IStudentService
{
    public List<StudentResponseDTO> GetAll();
    public StudentResponseDTO? GetOne(int id);
    public int? Create(StudentEditDTO student);
    public bool Update(StudentEditDTO student);
    public bool Delete(int id);
    public bool UpdateGrade(StudentUpdateGradeDTO student);
}