using Events_Web_Application.src.Domain.Enums;
using MediatR;

namespace Events_Web_Application.src.Application.Events.Commands.Command
{
    public class AddPhotoEventCommand : IRequest<Unit>
    {
        public int id;
        public string PhotoPath;

        public AddPhotoEventCommand(int id, string photoPath)
        {
            this.id = id;
            PhotoPath = photoPath;
        }
    }
}
