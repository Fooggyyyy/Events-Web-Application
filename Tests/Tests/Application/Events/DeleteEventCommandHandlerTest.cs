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
    public class DeleteEventCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task DeleteEventCommandHandlerTest_Should_DeleteEvent()
        {
            // Arrange
            var handler = new DeleteEventCommandHandler(new EventRepository(context));

            await context.SaveChangesAsync();

            var command = new DeleteEventCommand(11);
            // Act
            var DeleteEvent = await context.Events.FindAsync(11);

            // Assert
            Assert.Null(DeleteEvent);
        }
    }
}
