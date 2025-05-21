using AutoMapper;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.interfaces;
using Events_Web_Application.src.Infastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Events_Web_Application.src.Application.Events.Queries.Handler
{
    public class GetChangesHandler : IRequestHandler<GetChangesQuery, ICollection<ChangesDTO>>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private AppDbContext _appDbContext;

        public GetChangesHandler(IUserRepository _userRepository, IMapper mapper, IHttpContextAccessor _httpContextAccessor, AppDbContext _appDbContext)
        {
            this._userRepository = _userRepository;
            _mapper = mapper;
            this._httpContextAccessor = _httpContextAccessor;
            this._appDbContext = _appDbContext;
        }

        public async Task<ICollection<ChangesDTO>> Handle(GetChangesQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            List<int> EventsWhereCurrentUserRegister = _appDbContext.Participations
                .Where(x =>  x.UserId == currentUserId)
                .Select(x => x.EventId).ToList();
            var Changes = await _userRepository.GetChanges();
            var ChangesReturn = Changes.Where(x => EventsWhereCurrentUserRegister.Contains(x.EventId));

            return _mapper.Map<ICollection<ChangesDTO>>(ChangesReturn);
        }
    }
}
