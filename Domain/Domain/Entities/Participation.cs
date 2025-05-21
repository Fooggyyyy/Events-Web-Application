namespace Events_Web_Application.src.Domain.Entities
{
    public class Participation
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime RegistrationDate { get; set; }
        public Participation()
        {

        }

        public Participation(int eventId, Event @event, int userId, User user, DateTime registrationDate)
        {
            EventId = eventId;
            Event = @event;
            UserId = userId;
            User = user;
            RegistrationDate = registrationDate;
        }

        public Participation(int eventId, int userId, DateTime registrationDate)
        {
            EventId = eventId;
            UserId = userId;
            RegistrationDate = registrationDate;
        }
    }
}
