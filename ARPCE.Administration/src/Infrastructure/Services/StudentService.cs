using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
using ARPCE.Administration.Application.Common.Models;
using ARPCE.Administration.Application.Features.StudentFeatures.Commands.CreateStudent;
using ARPCE.Administration.Domain.Entities;
using ARPCE.Administration.Infrastructure.Persistence.Repositories.Interfaces;
using AutoMapper;

namespace ARPCE.Administration.Infrastructure.Services;
public class StudentService : IStudentService
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;
    public StudentService(IMapper mapper, IStudentRepository studentRepository) {
        _mapper = mapper;
        _studentRepository = studentRepository;
    }



    public async Task<Guid> CreateStudentAsync(CreateStudentCommand request)
    {
         var entity = _mapper.Map<Student>(request);
        var studentId = await _studentRepository.CreateAsync(entity);
        if(request.CoursesIds!=null && request.CoursesIds.Count>0)
        {
            await _studentRepository.AddStudentToCourse(studentId, request.CoursesIds);
        }
        
        return studentId;




    }

    public async Task<List<StudentVm>> GetAllStudentsAsync()
    {
        var entities = await _studentRepository.GetAllAsync();

        var output = _mapper.Map<List<StudentVm>>(entities);

        return output;
    }
    public async Task<Result> DeleteAsync(Guid Id)
    {
        return await _studentRepository.DeleteAsync(Id);
    }
}
