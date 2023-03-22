using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Finished
{
    public class CustomerListFinishedContractsRequest : ListRequestDto
    {
    }

    public class CustomerListFinishedContractsQuery : UserRequest<CustomerListFinishedContractsRequest, ListResponseDto<FinishedContractListITemDto>>
    {
    }
}
