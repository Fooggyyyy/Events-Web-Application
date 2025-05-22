using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserDTO>
    {
        private IMapper mapper;
        private IUserRepository userRepository;

        public GetByIdUserQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var User = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
            return mapper.Map<UserDTO>(User);
        }
    }

    
}
