using SuperCarga.Application.Domain.Customers.Customers.Commands.UploadIdDocument;
using SuperCarga.Application.Domain.Customers.Customers.Queries.Details;
using SuperCarga.Application.Domain.Customers.Customers.Queries.Details.Dro;
using SuperCarga.Application.Domain.Users.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Customers.Abstraction
{
    public interface ICustomersCustomersService
    {
        Task<CustomerDetailsDto> GetCustomersDetails(GetCustomersDetailsQuery request);

        Task<Guid> CreateCustomer(RegisterUserCommand command);

        Task<string> UploadIdDocument(UploadIdDocumentCommand request);
    }
}
