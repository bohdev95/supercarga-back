using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Customers.Driver.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Drivers.Queries.Details.Dto
{
    public class DriverContractDetailsDto : ContractDetailsDto
    {
        public DriverCustomerDto Customer { get; set; }
    }
}
