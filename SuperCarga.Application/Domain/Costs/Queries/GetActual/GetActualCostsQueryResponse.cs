using SuperCarga.Application.Domain.Costs.Dto;

namespace SuperCarga.Application.Domain.Costs.Queries.GetActual
{
    public class GetActualCostsQueryResponse
    {
        public List<CostDto> Costs { get; set; }
    }
}
