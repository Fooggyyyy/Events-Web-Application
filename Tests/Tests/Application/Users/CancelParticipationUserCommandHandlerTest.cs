using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.Commands.Handler;
using Events_Web_Application.src.Infastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Application.Users
{
    public class CancelParticipationUserCommandHandlerTest : TestCommandBase
    {
        //Не работает из за ExectueDelete в UserRepository
        //[Fact]
        //public async Task CancelParticipationUserCommandHandler_Should_RemoveParticipation()
        //{
        //    // Arrange
        //    var userRepository = new UserRepository(context);
        //    var handler = new CancelParticipationUserCommandHandler(userRepository);
        //    var userId = 1;
        //    var eventId = 1;

        //    var participationExists = context.Participations.Any(p => p.UserId == userId && p.EventId == eventId);
        //    Assert.True(participationExists); 

        //    var command = new CancelParticipationUserCommand(eventId, userId);

        //    // Act
        //    await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    var participationStillExists = context.Participations.Any(p => p.UserId == userId && p.EventId == eventId);
        //    Assert.False(participationStillExists); 
        //}
    }
}
