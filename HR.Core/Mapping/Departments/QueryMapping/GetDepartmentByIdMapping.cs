using AutoMapper;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>();


            //  .ForMember(des=>des.ManagerName,opt=>opt.MapFrom(src => src.Manager.Name))
            //  .ForMember(des=>des.Instructors,opt=>opt.MapFrom(src => src.Instructors))
            //  .ForMember(des=>des.Students,opt=>opt.MapFrom(src => src.Students))
            //  .ForMember(des=>des.Subjects,opt=>opt.MapFrom(src => src.DepartmentSubjects));

            //CreateMap<Instructor, InstructorResponse>()
            //     .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            //  .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name))
            //  .ForMember(des => des.Position, opt => opt.MapFrom(src => src.Position));

            //CreateMap<Student, StudentResponse>()
            //  .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            //  .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name))
            //  .ForMember(des => des.Phone, opt => opt.MapFrom(src => src.Phone));

            //CreateMap<DepartmentSubject, SubjectResponse>()
            //  .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Subject.Id))
            //  .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Subject.Name))
            //  .ForMember(des => des.Hours, opt => opt.MapFrom(src => src.Subject.Hours));
;

        }
    
    }
}
