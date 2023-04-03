using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Customers.Abstraction;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Common.Dto;

namespace SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Archived
{
    public class CustomerListArchivedJobsQueryHandler : IRequestHandler<CustomerListArchivedJobsQuery, ListResponseDto<CustomerJobListItemDto>>
    {
        private readonly ICustomerJobsService customerJobsService;

        public CustomerListArchivedJobsQueryHandler(ICustomerJobsService customerJobsService)
        {
            this.customerJobsService = customerJobsService;
        }

        public async Task<ListResponseDto<CustomerJobListItemDto>> Handle(CustomerListArchivedJobsQuery request, CancellationToken cancellationToken)
        {
            var response = await customerJobsService.ListArchivedJobs(request);

            return response;
        }
    }
}
