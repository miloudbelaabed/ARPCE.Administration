using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPCE.Administration.Application.Common.Interfaces;
using ARPCE.Administration.Application.Common.Mappings.ViewModels;
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
    public async Task<Guid> CreateStudentAsync(StudentVm student)
    {

        /* var entity = _mapper.Map<Student>(student);*/
        var entity = new Student
        {
            Name = student.Name,
        };
        var output = await _studentRepository.CreateAsync(entity);
        return output.Id;


        
    }

    public async Task<List<StudentVm>> GetAllStudentsAsync()
    {
        var result = await _studentRepository.GetAllAsync();
        result.ForEach(i => Console.Write("{0}\t", i.Name));

        var output = _mapper.Map<List<StudentVm>>(result);

        return output;
    }
}
