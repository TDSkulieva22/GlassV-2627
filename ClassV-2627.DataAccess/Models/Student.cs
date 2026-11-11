using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlassV_2627.Models;

[Index(nameof(Username), IsUnique = true)]
[Index(nameof(FirstName),nameof(LastName))]
public class Student
{
    [Key]
    public int Id { get; set; }
    
    [Column(TypeName = "nvarchar(100)")]
    [Required]
    public string FirstName { get; set; }
    
    [Column(TypeName = "nvarchar(100)")]
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public int Age { get; set; }
    public int Grade { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    
    [ForeignKey(nameof(Teacher))]
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    
}