using AutoMapper;
using ToDoApi.Data.Models;
using ToDoApi.Dto;

namespace ToDoApi.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Data.Models.Task, TaskDto>().ReverseMap();
            CreateMap<Assignee, AssigneeDto>()
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));
        }
    }
}
