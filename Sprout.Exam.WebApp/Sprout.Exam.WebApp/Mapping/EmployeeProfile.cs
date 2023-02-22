using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Models;

namespace Sprout.Exam.WebApp.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeEntity, EmployeeDto>()
                .ForMember(dto => dto.FullName, entity => entity.MapFrom(obj => obj.FullName))
                .ForMember(dto => dto.Birthdate, entity => entity.MapFrom(obj => obj.Birthdate.ToString("yyyy-MM-dd")))
                .ForMember(dto => dto.Tin, entity => entity.MapFrom(obj => obj.TIN))
                .ForMember(dto => dto.TypeId, entity => entity.MapFrom(obj => obj.EmployeeTypeId))
                .ReverseMap();
        }
    }
}
