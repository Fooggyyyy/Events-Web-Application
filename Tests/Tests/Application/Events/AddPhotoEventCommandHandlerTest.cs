using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.Commands.Handler;
using Events_Web_Application.src.Infastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Application.Events
{
    public class AddPhotoEventCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task Handle_ShouldAddPhotoToEvent_InMemoryDb()
        {
            //Arrange
            var handler = new AddPhotoEventCommandHandler(new EventRepository(context), null);

            await context.SaveChangesAsync();

            var command = new AddPhotoEventCommand(1, "path/to/photo.jpg");

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var updatedEvent = await context.Events.FindAsync(1);
            Assert.Contains("path/to/photo.jpg", updatedEvent.PhotoPath);
        }
    }
}
