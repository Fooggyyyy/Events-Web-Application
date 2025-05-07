namespace Events_Web_Application.src.Domain.Entities
{
    public class Changes
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public string ChangesInEvent { get; set; }

        public Changes(int eventId, Event @event, string changesInEvent)
        {
            EventId = eventId;
            Event = @event;

            ChangesInEvent = changesInEvent;
        }

        public Changes(int eventId, string changesInEvent)
        {
            EventId = eventId;
            ChangesInEvent = changesInEvent;
        }

        public Changes()
        {
        }
    }
}
