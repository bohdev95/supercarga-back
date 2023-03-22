using MediatR;
using SuperCarga.Application.Domain.Costs.Abstraction;

namespace SuperCarga.Application.Domain.Costs.Queries.GetActual
{
    public class GetActualCostsQueryHandler : IRequestHandler<GetActualCostsQuery, GetActualCostsQueryResponse>
    {
        private readonly ICostsService costsService;

        public GetActualCostsQueryHandler(ICostsService costsService)
        {
            this.costsService = costsService;
        }

        public async Task<GetActualCostsQueryResponse> Handle(GetActualCostsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetActualCostsQueryResponse();

            response.Costs = await costsService.GetActualCosts();

            return response;
        }
    }
}
