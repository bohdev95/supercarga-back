using SuperCarga.Application.Domain.Contracts.Customers.Queries.Details.Dto;

namespace SuperCarga.Application.Domain.Contracts.Customers.Queries.Details
{
    public class GetCustomerContractDetailsQueryResponse
    {
        public CustomerContractDetailsDto Contract { get; set; }
    }
}
