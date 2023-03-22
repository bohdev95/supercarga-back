using SuperCarga.Application.Domain.Customers.Common.Models;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Application.Domain.Customers.Driver.Dto
{
    public static class DriverCustomerDtoExtensions
    {
        public static DriverCustomerDto GetDriverCustomerDto(this Customer customer) => new DriverCustomerDto
        {
            Id = customer.Id,
            Name = customer.User.DisplayName(),
            Email = customer.User.Email,
            ImagePath = customer.User.ImagePath,
            Spend = 100000 //TODO
        };
    }
}
