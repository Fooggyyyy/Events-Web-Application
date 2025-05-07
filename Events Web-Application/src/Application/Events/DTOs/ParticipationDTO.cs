namespace Events_Web_Application.src.Application.Events.DTOs
{
    public class ParticipationDTO
    {
        public DateTime RegistrationDate { get; set; }

        public ParticipationDTO()
        {
        }

        public ParticipationDTO(DateTime registrationDate)
        {
            RegistrationDate = registrationDate;
        }
    }
}
