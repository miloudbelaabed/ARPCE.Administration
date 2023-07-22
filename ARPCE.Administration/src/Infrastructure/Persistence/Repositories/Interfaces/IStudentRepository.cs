using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Domain.Entities;

namespace ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
public interface IStudentRepository
{
    Task<Student> CreateAsync(Student student);
    Task<List<Student>> GetAllAsync();
}
