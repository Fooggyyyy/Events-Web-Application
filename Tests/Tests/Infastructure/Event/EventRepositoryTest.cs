﻿using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;
using Events_Web_Application.src.Infastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Infastructure.Event
{
    public class EventRepositoryTest : TestCommandBase
    {
        [Fact]
        public async Task GetAllAsync_ReturnsAllEvents()
        {
            var repo = new EventRepository(context);

            var result = await repo.GetAllAsync(CancellationToken.None);

            Assert.Equal(10, result.Count);
        }

        [Fact]
        public async Task GetByDateAsync_ReturnsCorrectEvents()
        {
            var repo = new EventRepository(context);
            var targetDate = context.Events.First().Date;

            var result = await repo.GetByDateAsync(targetDate, CancellationToken.None);

            Assert.All(result, e => Assert.Equal(targetDate.Date, e.Date.Date));
        }

        [Fact]
        public async Task GetByPlaceAsync_ReturnsCorrectEvents()
        {
            var repo = new EventRepository(context);

            var result = await repo.GetByPlaceAsync("Tech Hub", CancellationToken.None);

            Assert.Single(result);
            Assert.Equal("Tech Hub", result.First().Place);
        }

        [Fact]
        public async Task GetByCategoryAsync_ReturnsCorrectEvents()
        {
            var repo = new EventRepository(context);

            var result = await repo.GetByCategoryAsync(Category.MasterClasses, CancellationToken.None);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectEvent()
        {
            var repo = new EventRepository(context);

            var result = await repo.GetByIdAsync(1, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByNameAsync_ReturnsCorrectEvent()
        {
            var repo = new EventRepository(context);

            var result = await repo.GetByNameAsync("Hackathon", CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Hackathon", result.Name);
        }

        [Fact]
        public async Task AddAsync_AddsEventSuccessfully()
        {
            var repo = new EventRepository(context);

            var newEvent = new Events_Web_Application.src.Domain.Entities.Event(11, "New Event", "Description", DateTime.Now, "New Place", Category.Parties, 100, new List<string>(), new List<Participation>());
            await repo.AddAsync(newEvent, CancellationToken.None);

            var result = await context.Events.FindAsync(11);
            Assert.NotNull(result);
            Assert.Equal("New Event", result.Name);
        }
    }
}
