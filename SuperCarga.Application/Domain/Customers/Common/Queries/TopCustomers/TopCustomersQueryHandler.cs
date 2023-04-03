using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Customers.Common.Abstraction;
using SuperCarga.Application.Domain.Customers.Common.Dto;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Application.Domain.Drivers.Common.Dto;

namespace SuperCarga.Application.Domain.Customers.Common.Queries.TopCustomers
{
    public class TopCustomersQueryHandler : IRequestHandler<TopCustomersQuery, ListResponseDto<TopCustomersDto>>
    {
        private readonly ICustomersService customersService;

        public TopCustomersQueryHandler(ICustomersService customersService)
        {
            this.customersService = customersService;   
        }

        public async Task<ListResponseDto<TopCustomersDto>> Handle(TopCustomersQuery request, CancellationToken cancellationToken)
        {
            var response = await customersService.GetTopCustomers(request);

            return response;
        }
    }
}
