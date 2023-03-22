using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Jobs.Common.Models;

namespace SuperCarga.Application.Domain.Costs.Dto
{
    public static class CostsSummaryDtoExtensions
    {
        public static CostsSummaryDto GetCostsSummary(this Contract contract) => new CostsSummaryDto
        {
            PricePerKm = contract.PricePerKm,
            PricePerDistance = contract.PricePerDistance,
            Additions = contract.Additions.Select(x => new AdditionCostDto
            {
                Name = x.Name,
                Price = x.Price,
            }).ToList(),
            TotalPrice = contract.TotalPrice,
            ServiceFee = contract.ServiceFee,
            Price = contract.Price
        };

        public static CostsSummaryDto GetCostsSummary(this Job job) => new CostsSummaryDto
        {
            PricePerKm = job.PricePerKm,
            PricePerDistance = job.PricePerDistance,
            Additions = job.Additions.Select(x => new AdditionCostDto
            {
                Name = x.Name,
                Price = x.Price,
            }).ToList(),
            TotalPrice = job.TotalPrice,
            ServiceFee = job.ServiceFee,
            Price = job.Price
        };
    }
}
