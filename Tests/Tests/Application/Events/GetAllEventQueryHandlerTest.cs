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

namespace Tests.Tests.Application.Events
{
    public class GetAllEventQueryHandlerTest : TestCommandBase
    {
        private readonly IMapper _mapper;

        public GetAllEventQueryHandlerTest()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDTO>();
            });
            _mapper = config.CreateMapper();
        }


        [Fact]
        public async Task GetAllEventQueryHandlerTest_Should()
        {
            // Arrange
            var repository = new EventRepository(context);
            var handler = new GetAllEventQueryHandler(repository, _mapper);
            var query = new GetAllEventQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(context.Events.Count(), result.Count);

        }
    }
}
