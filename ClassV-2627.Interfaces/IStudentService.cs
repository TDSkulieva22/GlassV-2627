using GlassV_2627.DTOs.StudentDTOs;
using GlassV_2627.Filters;

namespace GlassV_2627.Services.Interfaces;

public interface IStudentService
{
    public Task<List<StudentResponseDTO>> GetAll(StudentFilterModel filters);
    public Task<StudentResponseDTO?> GetOne(int id);
    public Task<int?> Create(StudentEditDTO student);
    public Task<bool> Update(StudentEditDTO student);
    public Task<bool> Delete(int id);
    public Task<bool> UpdateGrade(StudentUpdateGradeDTO student);
}