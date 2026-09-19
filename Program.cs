using Microsoft.EntityFrameworkCore;
using UniversityManagement.API.Context;
using UniversityManagement.API.Repository;
using Microsoft.OpenApi;
using UniversityManagement.API.Models;
using UniversityManagement.API.Controllers;
using UniversityManagement.API.Repository.Interfaces;
using UniversityManagement.API.UnitOfWork;
using UniversityManagement.API.Services;
using UniversityManagement.API.IServices;

namespace UniversityManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<UniversityDbContext>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("UniversityCon")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
               opt=>opt.SwaggerDoc("v1",new OpenApiInfo
               {
                   Version = "v1",
                   Title = "LMS API",
                   Description = "API for course managment system",
                   Contact = new OpenApiContact
                   {
                       Name = "Moaz Mohamed",
                       Email = "mm6007@fayoum.edu.eg"
                       
                   }
               })
                
                
             );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.MapSwagger().RequireAuthorization(op=>op.RequireRole("Admin"));
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}