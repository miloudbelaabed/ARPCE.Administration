using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Domain.Entities;

namespace ARPCE.Administration.Application.Common.Interfaces;
public interface IStudentService
{
    Task<Guid> CreateStudentAsync(StudentVm student);
    Task<List<StudentVm>> GetAllStudentsAsync();
}
