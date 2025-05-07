using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;
using Events_Web_Application.src.Domain.interfaces;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Handler
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, int>
    {
        private IEventRepository _eventRepository;
        private IMapper _mapper;
        public static string Changes = "";

        public UpdateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(request.Id);
            

            if (existingEvent.Name != request.Name)
                Changes += $"Имя сменилось с  {existingEvent.Name} на {request.Name}.";
            if (existingEvent.Description != request.Description)
                Changes += $"Описание сменилось с  {existingEvent.Description} на {request.Description}.";
            if (existingEvent.Date != request.Date)
                Changes += $"Дата сменилась с  {existingEvent.Date} на {request.Date}.";
            if (existingEvent.Place != request.Place)
                Changes += $"Место сменилось с  {existingEvent.Place} на {request.Place}.";
            if (existingEvent.Category != request.Category)
                Changes += $"Категория сменилась с  {existingEvent.Category} на {request.Category}.";
            if (existingEvent.MaxUser != request.MaxUser)
                Changes += $"Максимальное количество сменилось с  {existingEvent.MaxUser} на {request.MaxUser}.";


            existingEvent.Name = request.Name;
            existingEvent.Description = request.Description;
            existingEvent.Date = request.Date;
            existingEvent.Place = request.Place;
            existingEvent.Category = request.Category;
            existingEvent.MaxUser = request.MaxUser;
            existingEvent.PhotoPath = request.PhotoPath;

            await _eventRepository.UpdateAsync(existingEvent);
            return existingEvent.Id;
        }
    }
}
