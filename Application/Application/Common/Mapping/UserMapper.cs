using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Domain.Entities;

namespace Events_Web_Application.src.Application.Common.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
