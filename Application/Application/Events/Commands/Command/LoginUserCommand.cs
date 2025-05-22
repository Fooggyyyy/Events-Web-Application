using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Events.Commands.Command
{
    public class LoginUserCommand : IRequest<AuthTokens>
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public LoginUserCommand(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
