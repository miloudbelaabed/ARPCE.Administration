using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Domain.Entities;

namespace ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
public interface IStudentRepository
{
    Task<Guid> CreateAsync(Student student);
    Task<List<Student>> GetAllAsync();
    Task<(bool updated, string message)> AddStudentToCourse(Guid studentId, List<Guid> coursesId);
    Task<Result> DeleteAsync(Guid Id);


    }
