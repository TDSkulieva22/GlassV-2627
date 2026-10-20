using GlassV_2627.Data;
using GlassV_2627.Services;
using GlassV_2627.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace GlassV_2627;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        builder.Services.AddControllers();

        builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        builder.Services.AddScoped<IStudentService, StudentServive>();
        builder.Services.AddScoped<ITeacherService, TeacherService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        
        app.UseRouting();
       
        app.MapScalarApiReference();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}