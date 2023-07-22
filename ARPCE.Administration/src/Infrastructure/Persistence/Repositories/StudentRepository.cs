using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Domain.Entities;
using ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ARPCE.Administration.Infrastructure.Persistence.Repositories;
public class StudentRepository : IStudentRepository
{
    private readonly ARPCEDbContext _context;

    public StudentRepository(ARPCEDbContext context)
    {
        _context = context; 
    }

    public async Task<Student> CreateAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public Task<List<Student>> GetAllAsync()
    {
        return _context.Students.ToListAsync();
    }
}
