using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;
using Events_Web_Application.src.Infastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests
{
    public class AppDBContextFactory
    {
        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("TestDataBase").Options;
            var context = new AppDbContext(options);
            context.Database.EnsureCreated();

 
            var users = new List<User>
            {
                    new User(1, "John", "Doe", new DateOnly(1990, 5, 15), "john.doe@example.com", new List<Participation>()),
                    new User(2, "Jane", "Smith", new DateOnly(1985, 8, 22), "jane.smith@example.com", new List<Participation>()),
                    new User(3, "Michael", "Johnson", new DateOnly(1992, 3, 10), "michael.j@example.com", new List<Participation>()),
                    new User(4, "Emily", "Williams", new DateOnly(1988, 11, 5), "emily.w@example.com", new List<Participation>()),
                    new User(5, "David", "Brown", new DateOnly(1995, 7, 30), "david.b@example.com", new List<Participation>()),
                    new User(6, "Sarah", "Jones", new DateOnly(1991, 9, 18), "sarah.j@example.com", new List<Participation>()),
                    new User(7, "Robert", "Garcia", new DateOnly(1987, 4, 25), "robert.g@example.com", new List<Participation>()),
                    new User(8, "Lisa", "Miller", new DateOnly(1993, 12, 8), "lisa.m@example.com", new List<Participation>()),
                    new User(9, "Thomas", "Davis", new DateOnly(1989, 6, 14), "thomas.d@example.com", new List<Participation>()),
                    new User(10, "Jennifer", "Wilson", new DateOnly(1994, 2, 28), "jennifer.w@example.com", new List<Participation>())
            };

            var events = new List<Event>
            {
                new Event(1, "Tech Conference", "Annual technology conference", DateTime.Now.AddDays(30), "Convention Center", Category.MasterClasses, 200,
                    new List<string> {"conf1.jpg", "conf2.jpg"}, new List<Participation>()),
                new Event(2, "Code Workshop", "Hands-on coding workshop", DateTime.Now.AddDays(15), "Tech Hub", Category.Conferences, 50,
                    new List<string> {"workshop1.jpg"}, new List<Participation>()),
                new Event(3, "Hackathon", "48-hour coding competition", DateTime.Now.AddDays(60), "Innovation Center", Category.Hackathons, 100,
                    new List<string> {"hack1.jpg", "hack2.jpg", "hack3.jpg"}, new List<Participation>()),
                new Event(4, "AI Summit", "Artificial intelligence conference", DateTime.Now.AddDays(60), "Grand Hotel", Category.Parties, 150,
                    new List<string> {"ai1.jpg"}, new List<Participation>()),
                new Event(5, "Web Dev Bootcamp", "Web development training", DateTime.Now.AddDays(20), "Digital Campus", Category.MasterClasses, 30,
                    new List<string> {"web1.jpg", "web2.jpg"}, new List<Participation>()),
                new Event(6, "Startup Pitch", "Startup pitching event", DateTime.Now.AddDays(10), "Business Center", Category.Tournaments, 80,
                    new List<string> {"pitch1.jpg"}, new List<Participation>()),
                new Event(7, "Game Jam", "Game development marathon", DateTime.Now.AddDays(25), "Creative Space", Category.Conferences, 75,
                    new List<string> {"game1.jpg", "game2.jpg"}, new List<Participation>()),
                new Event(8, "Data Science Meetup", "Data science networking", DateTime.Now.AddDays(5), "Science Park", Category.Conferences, 40,
                    new List<string> {"data1.jpg"}, new List<Participation>()),
                new Event(9, "UX Design Workshop", "User experience design", DateTime.Now.AddDays(35), "Design Studio", Category.Conferences, 25,
                    new List<string> {"ux1.jpg", "ux2.jpg", "ux3.jpg"}, new List<Participation>()),
                new Event(10, "Blockchain Forum", "Blockchain technology discussion", DateTime.Now.AddDays(50), "Finance Tower", Category.Hackathons, 120,
                    new List<string> {"block1.jpg"}, new List<Participation>())
            };

            var participations = new List<Participation>
            {
                new Participation(1, 1, DateTime.Today.AddDays(-9)),
                new Participation(1, 2, DateTime.Today.AddDays(-8)),
                new Participation(2, 3, DateTime.Today.AddDays(-7)),
                new Participation(3, 4, DateTime.Today.AddDays(-6)),
                new Participation(4, 5, DateTime.Today.AddDays(-5)),
                new Participation(5, 6, DateTime.Today.AddDays(-4)),
                new Participation(6, 7, DateTime.Today.AddDays(-3)),
                new Participation(7, 8, DateTime.Today.AddDays(-2)),
                new Participation(8, 9, DateTime.Today.AddDays(-1)),
                new Participation(9, 10, DateTime.Today)
            };

            context.Users.AddRange(users);
            context.Events.AddRange(events);
            context.Participations.AddRange(participations);

            context.SaveChanges();  
            return context;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
