using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sprout.Exam.Business.Domain;

namespace Sprout.Exam.WebApp.Mapping
{
    public class ErrorMappingProfile : Profile
    {
        public ErrorMappingProfile()
        {
            CreateMap<CommandResult, CommandErrorResult>();

            CreateMap<ModelStateDictionary, CommandErrorResult>()
                .ForMember(c => c.Error, from => from.MapFrom(m => new CommandResult()));
        }
    }
}
