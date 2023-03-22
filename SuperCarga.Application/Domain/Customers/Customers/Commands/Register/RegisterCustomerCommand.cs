using MediatR;
using SuperCarga.Application.Domain.Users.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Customers.Commands.Register
{
    public class RegisterCustomerCommand : RegisterUserCommand, IRequest<RegisterCustomerCommandResponse>
    {
    }
}
