namespace Events_Web_Application.src.Application.Events.DTOs
{
    public class ChangesDTO
    {
        public string ChangesInEvent { get; set; }

        public ChangesDTO(string changesInEvent)
        {
            ChangesInEvent = changesInEvent;
        }

        public ChangesDTO()
        {
        }
    }
}
