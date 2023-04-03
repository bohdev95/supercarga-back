using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Customers.Abstraction;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Active
{
    public class CustomerListActiveJobsQueryHandler : IRequestHandler<CustomerListActiveJobsQuery, ListResponseDto<CustomerJobListItemDto>>
    {
        private readonly ICustomerJobsService customerJobsService;

        public CustomerListActiveJobsQueryHandler(ICustomerJobsService customerJobsService)
        {
            this.customerJobsService = customerJobsService;
        }

        public async Task<ListResponseDto<CustomerJobListItemDto>> Handle(CustomerListActiveJobsQuery request, CancellationToken cancellationToken)
        {
            var response = await customerJobsService.ListActiveJobs(request);

            return response;
        }
    }
}
