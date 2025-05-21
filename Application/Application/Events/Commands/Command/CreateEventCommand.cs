using Events_Web_Application.src.Domain.Entities;
using Events_Web_Application.src.Domain.Enums;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Command
{
    public class CreateEventCommand : IRequest<int>
    {
        public int Id;
        public string Name;
        public string Description;
        public DateTime Date;
        public string Place;
        public Category Category;
        public int MaxUser;
        public ICollection<string> PhotoPath;
        public ICollection<Participation> Participations;

        public CreateEventCommand(int id, string name, string description, DateTime date, string place, Category category, int maxUser, ICollection<string> photoPath, ICollection<Participation> participations)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Place = place;
            Category = category;
            MaxUser = maxUser;
            PhotoPath = photoPath;
            Participations = participations;
        }

        public CreateEventCommand() { }
    }
}
