using Events_Web_Application.src.Domain.Entities;

namespace Events_Web_Application.src.Application.Events.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthdayDate { get; set; }
        public string Email { get; set; }
        public ICollection<Participation> Participations { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(int id, string name, string surname, DateOnly birthdayDate, string email, ICollection<Participation> participations)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BirthdayDate = birthdayDate;
            Email = email;
            Participations = participations;
        }
    }
}
