using MediatR;
using SuperCarga.Application.Domain.Customers.Customers.Abstraction;

namespace SuperCarga.Application.Domain.Customers.Customers.Queries.Details
{
    public class GetCustomersDetailsQueryHandler : IRequestHandler<GetCustomersDetailsQuery, GetCustomersDetailsQueryResponse>
    {
        private readonly ICustomersCustomersService customersService;

        public GetCustomersDetailsQueryHandler(ICustomersCustomersService customersService)
        {
            this.customersService = customersService;   
        }

        public async Task<GetCustomersDetailsQueryResponse> Handle(GetCustomersDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetCustomersDetailsQueryResponse();

            response.Customer = await customersService.GetCustomersDetails(request);

            return response;
        }
    }
}
