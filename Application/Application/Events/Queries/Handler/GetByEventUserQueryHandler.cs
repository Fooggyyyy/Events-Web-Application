using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetByEventUserQueryHandler : IRequestHandler<GetByEventUserQuery, ICollection<UserDTO>>
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;

        public GetByEventUserQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ICollection<UserDTO>> Handle(GetByEventUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetByEventAsync(request.EventId);

                var pagedUsers = users
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

            return _mapper.Map<ICollection<UserDTO>>(pagedUsers);
        }
    }
}
