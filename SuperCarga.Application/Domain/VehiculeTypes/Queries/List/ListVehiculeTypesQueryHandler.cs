using MediatR;
using SuperCarga.Application.Domain.VehiculeTypes.Abstraction;

namespace SuperCarga.Application.Domain.VehiculeTypes.Queries.List
{
    public class ListVehiculeTypesQueryHandler : IRequestHandler<ListVehiculeTypesQuery, ListVehiculeTypesQueryResponse>
    {
        private readonly IVehiculeTypesService vehiculeTypesService;

        public ListVehiculeTypesQueryHandler(IVehiculeTypesService vehiculeTypesService)
        {
            this.vehiculeTypesService = vehiculeTypesService;
        }

        public async Task<ListVehiculeTypesQueryResponse> Handle(ListVehiculeTypesQuery request, CancellationToken cancellationToken)
        {
            var response = new ListVehiculeTypesQueryResponse();

            response.VehiculeTypes = await vehiculeTypesService.ListVehiculeTypes();

            return response;
        }
    }
}
