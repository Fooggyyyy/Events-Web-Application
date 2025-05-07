using Abp.Domain.Repositories;
using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Command;
using Events_Web_Application.src.Application.Events.Commands.Handler;
using Events_Web_Application.src.Application.Events.Queries.Handler;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Infastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Application.Users
{
    public class RegisterOnEventUserCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task RegisterOnEventUserCommandHandlerTest_Should_RegisterOnEvent()
        {
            // Arrange
            var repository = new UserRepository(context);
            var handler = new RegisterOnEventUserCommandHandler(repository);
            var command = new RegisterOnEventUserCommand(1, 3);

            // Act
            var result =  handler.Handle(command, CancellationToken.None);

            Assert.Equal(3, context.Participations.Where(x => x.EventId == 1).Count());
        }
    }
}
