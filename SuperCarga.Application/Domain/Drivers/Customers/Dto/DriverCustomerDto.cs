using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Drivers.Common.Models;
using SuperCarga.Application.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Customers.Dto
{
    public class CustomerDriverDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public decimal? Rating { get; set; }

        public int RatedContracts { get; set; }
    }

    public static class DriverCustomerDtoExtensions
    {
        public static CustomerDriverDto GetCustomerDriverDto(this Driver driver) => new CustomerDriverDto
        {
            Id = driver.Id,
            Name = driver.User.DisplayName(),
            Email = driver.User.Email,
            ImagePath = driver.User.ImagePath,
            RatedContracts = driver.RatedContracts,
            Rating = driver.Rating
        };
    }
}
