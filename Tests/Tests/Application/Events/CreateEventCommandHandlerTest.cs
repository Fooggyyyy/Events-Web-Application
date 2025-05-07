using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.Commands.Handler;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;
using Events_Web_Application.src.Infastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Application.Events
{
    public class CreateEventCommandHandlerTest : TestCommandBase
    {
        private readonly IMapper _mapper;

        public CreateEventCommandHandlerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateEventCommand, Event>();
                cfg.CreateMap<Event, CreateEventCommand>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldCreateEventAndReturnId()
        {
            var repository = new EventRepository(context);
            var handler = new CreateEventCommandHandler(repository, _mapper);

            var command = new CreateEventCommand(
                id: 11,
                name: "Test Event",
                description: "Test Description",
                date: DateTime.Now.AddDays(1),
                place: "Test Place",
                category: Category.Concert,
                maxUser: 100,
                photoPath: new List<string> { "photo1.jpg", "photo2.jpg" },
                participations: new List<Participation>()
            );

            // Act
            var resultId = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(11, resultId); 

            var createdEvent = await context.Events.FindAsync(11);
            Assert.NotNull(createdEvent);
            Assert.Equal("Test Event", createdEvent.Name);
            Assert.Equal(Category.Concert, createdEvent.Category);
            Assert.Equal(2, createdEvent.PhotoPath.Count);
        }
    }
}
