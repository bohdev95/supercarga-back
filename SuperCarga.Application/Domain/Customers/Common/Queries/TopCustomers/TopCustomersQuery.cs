using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Customers.Common.Dto;

namespace SuperCarga.Application.Domain.Customers.Common.Queries.TopCustomers
{

    public class TopCustomersQuery : ListRequestDto, IRequest<ListResponseDto<TopCustomersDto>>
    {
        
    }
}
