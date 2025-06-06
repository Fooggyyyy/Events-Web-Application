﻿using AutoMapper;
using Events_Web_Application.src.Application.Events.Commands.Handler;
using Events_Web_Application.src.Application.Events.DTOs;
using Events_Web_Application.src.Application.Events.Queries.Handler;
using Events_Web_Application.src.Application.Events.Queries.Query;
using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;
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
    public class GetByNameEventQueryHandlerTest : TestCommandBase
    {
        private readonly IMapper _mapper;

        public GetByNameEventQueryHandlerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDTO>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetByNameEventEventQueryHandlerTest_Should()
        {
            // Arrange
            var repository = new EventRepository(context);
            var handler = new GetByNameEventQueryHandler(_mapper, repository);
            var query = new GetByNameEventQuery("Tech Conference");
            // Act

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal("Tech Conference", result.Name);

        }
    }
}
