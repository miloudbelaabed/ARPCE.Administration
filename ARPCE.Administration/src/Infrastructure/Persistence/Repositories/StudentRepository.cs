using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Domain.Entities;
using ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Hosting.Internal;

namespace ARPCE.Administration.Infrastructure.Persistence.Repositories;
public class StudentRepository : IStudentRepository
{
    private readonly ARPCEDbContext _context;

    public StudentRepository(ARPCEDbContext context)
    {
        _context = context; 
    }

    public async Task<(bool updated, string message)> AddStudentToCourse(Guid studentId, List<Guid> coursesId)
    {
        try
        {
            await _context.StudentCourse.Where(s => s.StudentId == studentId).ExecuteDeleteAsync();

            foreach (var courseId in coursesId)
            {

                var studentCourse = new StudentCourse
                {
                    CourseId = courseId,
                    StudentId = studentId
                };
                _context.StudentCourse.Add(studentCourse);
                await _context.SaveChangesAsync();
            }

          
          

            return (true, string.Empty);        
        } 
        catch (Exception ex)
        {
            if(ex.InnerException != null)
            {
                return (false, ex.InnerException.Message);
            }else
            {
                return (false, ex.Message);
            }
        }



       
    }

    public async Task<Guid> CreateAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return student.Id;
    }

    public Task<List<Student>> GetAllAsync()
    {
        return _context.Students.Include(r=>r.Courses).ToListAsync();
    }
    public async Task<Result> DeleteAsync(Guid Id)
    {
        var student = _context.Students.Find(Id);
        try
        {
            if(student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                
            }
            
        }catch (Exception ex)
        {
            if(ex.InnerException != null)
            {
                Result.Failure(null);
            }
        }
        return Result.Success();

    }

}
