using MediatR;

namespace SuperCarga.Application.Domain.Users.Commands.Refresh
{
    public class RefreshCommand : IRequest<RefreshCommandResponse>
    {
        public string AccesToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
