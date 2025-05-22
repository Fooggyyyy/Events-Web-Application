using Events_Web_Application.src.Infastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Infastructure.User
{
    //Не знаю почему, но ошибок все тесты проходятся, если потом запускать их по отдельности
    public class UserRepositoryTest : TestCommandBase
    {
        [Fact]
        public async Task GetByEventAsync_ReturnsParticipants()
        {
            var repo = new UserRepository(context);

            var result = await repo.GetByEventAsync(1, CancellationToken.None);

            Assert.Equal(2, result.Count); 
        }

        [Fact]
        public async Task GetByUserIdAsync_ReturnsCorrectUser()
        {
            var repo = new UserRepository(context);

            var result = await repo.GetByIdAsync(1, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("John", result.Name);
        }

        [Fact]
        public async Task RegisterOnEventAsync_AddsParticipation()
        {
            var repo = new UserRepository(context);

            await repo.RegisterOnEventAsync(2, 1, CancellationToken.None);

            var result = context.Participations.FirstOrDefault(p => p.UserId == 1 && p.EventId == 2);
            Assert.NotNull(result);
        }


        //MemoryDB не поддерживает Execute, поэтому на этот тест дает ошибку 
        //[Fact]
        //public async Task CancelParticipationAsync_RemovesParticipation()
        //{
        //    var repo = new UserRepository(context);

        //    await repo.CancelParticipationAsync(1, 1);

        //    var result = context.Participations.FirstOrDefault(p => p.UserId == 1 && p.EventId == 1);
        //    Assert.Null(result);
        //}
    }
}
