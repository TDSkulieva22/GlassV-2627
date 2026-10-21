namespace GlassV_2627.Filters;

public class StudentFilterModel : BaseFilterModel
{
    public int? Age { get; set; }
    public string? FirstName { get; set; } //Moje i bez ?
    public string? LastName { get; set; }  //Moje i bez ?
    public int? Grade { get; set; }
}