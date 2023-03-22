using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Drivers.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Customers.Queries.Details.Dto
{
    public class CustomerContractDetailsDto : ContractDetailsDto
    {
        public CustomerDriverDto Driver { get; set; }
    }
}
