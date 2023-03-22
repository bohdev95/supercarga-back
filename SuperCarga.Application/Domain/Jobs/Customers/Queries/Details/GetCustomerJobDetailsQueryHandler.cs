using MediatR;
using SuperCarga.Application.Domain.Jobs.Customers.Abstraction;

namespace SuperCarga.Application.Domain.Jobs.Customers.Queries.Details
{
    public class GetCustomerJobsDetailsQueryHandler : IRequestHandler<GetCustomerJobsDetailsQuery, GetCustomerJobsDetailsQueryResponse>
    {
        private readonly ICustomerJobsService customerJobsService;

        public GetCustomerJobsDetailsQueryHandler(ICustomerJobsService customerJobsService)
        {
            this.customerJobsService = customerJobsService;
        }

        public async Task<GetCustomerJobsDetailsQueryResponse> Handle(GetCustomerJobsDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetCustomerJobsDetailsQueryResponse();

            response.Job = await customerJobsService.GetJobsDetails(request);

            return response;
        }
    }
}
