using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Events.Commands.Command
{
    public class RegisterUserCommand : IRequest<AuthTokens>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthdayDate { get; set; }

        public RegisterUserCommand(string email, string name, string surname, DateOnly birthdayDate)
        {
            Email = email;
            Name = name;
            Surname = surname;
            BirthdayDate = birthdayDate;
        }
    }
}
