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
    public class UpdateEventCommandHandlerTest : TestCommandBase
    {

        [Fact]
        public async Task Handle_ShouldUpdateExistingEvent()
        {
            var initialEvent = new Event
            {
                Id = 11,
                Name = "Old Name",
                Description = "Old Description",
                Date = DateTime.Now.AddDays(-1),
                Place = "Old Place",
                Category = Category.Conferences,
                MaxUser = 50,
                PhotoPath = new List<string> { "old_photo.jpg" }
            };

            await context.Events.AddAsync(initialEvent);
            await context.SaveChangesAsync();

            var repository = new EventRepository(context);
            var handler = new UpdateEventCommandHandler(repository, null);

            var newDate = DateTime.Now.AddDays(1);
            var command = new UpdateEventCommand(
                id: 11,
                name: "New Name",
                description: "New Description",
                date: newDate,
                place: "New Place",
                category: Category.Concert,
                maxUser: 100,
                photoPath: new List<string> { "new_photo1.jpg", "new_photo2.jpg" },
                participations: new List<Participation>()
            );

            // Act
            var resultId = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(11, resultId);
            var updatedEvent = await context.Events.FindAsync(11);
            Assert.NotNull(updatedEvent);
            Assert.Equal("New Name", updatedEvent.Name);
            Assert.Equal("New Description", updatedEvent.Description);
            Assert.Equal(newDate, updatedEvent.Date);
            Assert.Equal("New Place", updatedEvent.Place);
            Assert.Equal(Category.Concert, updatedEvent.Category);
            Assert.Equal(100, updatedEvent.MaxUser);
            Assert.Equal(2, updatedEvent.PhotoPath.Count);
            Assert.Contains("new_photo1.jpg", updatedEvent.PhotoPath);
            Assert.Contains("new_photo2.jpg", updatedEvent.PhotoPath);
        }
    }
}
