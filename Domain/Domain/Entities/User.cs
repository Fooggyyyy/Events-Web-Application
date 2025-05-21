namespace Events_Web_Application.src.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthdayDate { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<Participation> Participations;

        public User()
        {

        }

        public User(int id, string name, string surname, DateOnly birthdayDate, string email, ICollection<Participation> participations)
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
