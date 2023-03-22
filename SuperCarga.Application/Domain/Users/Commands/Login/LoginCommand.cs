using MediatR;

namespace SuperCarga.Application.Domain.Users.Commands.Login
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
