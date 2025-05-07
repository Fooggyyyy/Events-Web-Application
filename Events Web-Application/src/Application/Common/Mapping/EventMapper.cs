using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Entities;

namespace Events_Web_Application.src.Application.Common.Mapping
{
    public class EventMapper : Profile
    {
        public EventMapper() 
        {
            CreateMap<CreateEventCommand, Event>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.Participations, opt => opt.Ignore());

            CreateMap<AddPhotoEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Participations, opt => opt.Ignore());

            CreateMap<Event, EventDTO>();
        }
    }
}
