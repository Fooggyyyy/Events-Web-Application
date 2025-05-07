using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Handler
{
    public class AddPhotoEventCommandHandler : IRequestHandler<AddPhotoEventCommand, Unit>
    {
        private IEventRepository _eventRepository;
        private IMapper _mapper;

        public AddPhotoEventCommandHandler(IEventRepository eventRepository, IMapper _mapper)
        {
            _eventRepository = eventRepository;
            this._mapper = _mapper;
        }
            
        public async Task<Unit> Handle(AddPhotoEventCommand request, CancellationToken cancellationToken)
        {
            await _eventRepository.AddPhotoAsync(request.id, request.PhotoPath);
            return Unit.Value;
        }
    }
}
