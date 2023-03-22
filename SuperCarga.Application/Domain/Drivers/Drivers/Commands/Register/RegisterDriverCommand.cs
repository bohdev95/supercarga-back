using MediatR;
using SuperCarga.Application.Domain.Users.Commands.Register;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.Register
{
    public class RegisterDriverCommand : RegisterUserCommand, IRequest<RegisterDriverCommandResponse>
    {
        public Guid VehiculeTypeId { get; set; }
    }
}
