using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Handler;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Handler;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Infastructure.Persistence;
using Events_Web_Application.src.Infastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Application.Users
{
    public class GetByEventUserQueryHandlerTest : TestCommandBase
    {
        private readonly IMapper _mapper;

        public GetByEventUserQueryHandlerTest()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetByEventUserQueryHandlerTest_Should()
        {
            // Arrange
            var repository = new UserRepository(context);
            var handler = new GetByEventUserQueryHandler(_mapper, repository);
            var query = new GetByEventUserQuery(1, 1, 10);
            // Act

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);   

        }
    }
}
