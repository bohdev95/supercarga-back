using SuperCarga.Application.Domain.Costs.Commands.Set;
using SuperCarga.Application.Domain.Costs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Costs.Abstraction
{
    public interface ICostsService
    {
        Task<List<CostDto>> GetActualCosts();

        Task SetCost(SetCostCommand request);

        Task<CostsSummaryDto> CalculateCostsSummary(CaluclateCostsSummaryDto data);
    }
}
