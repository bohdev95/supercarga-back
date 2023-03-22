using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.Add;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.ConfirmDelivery;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.Finalize;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.Details;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Active;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Finished;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Contracts.Customers.Abstraction
{
    public interface ICustomerContractsService
    {
        Task<ListResponseDto<ActiveContractListITemDto>> ListActiveContracts(CustomerListActiveContractsQuery request);

        Task<ListResponseDto<FinishedContractListITemDto>> ListFinishedContracts(CustomerListFinishedContractsQuery request);

        Task<CustomerContractDetailsDto> GetContractDetails(GetCustomerContractDetailsQuery request);

        Task ConfirmDelivery(CustomerConfirmDeliveryCommand request);

        Task Finalize(CustomerFinalizeCommand request);

        Task<Guid> AddContract(AddContractCommand request);
    }
}
