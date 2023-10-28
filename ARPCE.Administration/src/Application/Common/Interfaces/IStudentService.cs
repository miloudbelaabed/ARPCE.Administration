using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Application.Features.StudentFeatures.Commands.CreateStudent;
using ARPCE.Administration.Domain.Entities;

namespace ARPCE.Administration.Application.Common.Interfaces;
public interface IStudentService
{
    Task<Guid> CreateStudentAsync(CreateStudentCommand request);
    Task<List<StudentVm>> GetAllStudentsAsync();
    Task<Result> DeleteAsync(Guid Id);

}
