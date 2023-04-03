using MediatR;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Application.Domain.Drivers.Common.Dto;

namespace SuperCarga.Application.Domain.Drivers.Common.Queries.TopRatedDrivers
{
    public class TopRatedDriversQueryHandler : IRequestHandler<TopRatedDriversQuery, ListResponseDto<TopRatedDriverDto>>
    {
        private readonly IDriversService driversService;

        public TopRatedDriversQueryHandler(IDriversService driversService)
        {
            this.driversService = driversService;   
        }

        public async Task<ListResponseDto<TopRatedDriverDto>> Handle(TopRatedDriversQuery request, CancellationToken cancellationToken)
        {
            var response = await driversService.GetTopRatedDrivers(request);

            return response;
        }
    }
}
