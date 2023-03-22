using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Costs.Commands.Set;
using SuperCarga.Application.Domain.Costs.Dto;
using SuperCarga.Application.Domain.Costs.Model;
using SuperCarga.Persistence.Database;

namespace SuperCarga.Domain.Domain.Costs
{
    public class CostsService : ICostsService
    {
        private readonly SuperCargaContext ctx;

        public CostsService(SuperCargaContext ctx)
        {
            this.ctx = ctx;
        }

        private IQueryable<Cost> GetActualCostsQuery()
        {
            var now = DateTime.Now.Date;

            var query = ctx.Costs
                .Where(x => x.FromDate.Date <= now)
                .Where(x => x.ToDate == null || x.ToDate.Value.Date > now)
                .AsQueryable();

            return query;
        }

        private IQueryable<CostDto> GetActualCostsDtoQuery()
        {
            var query = GetActualCostsQuery().Select(x => new CostDto
            {
                Type = x.Type,
                Value = x.Value
            })
            .AsQueryable();

            return query;
        }

        public async Task<List<CostDto>> GetActualCosts() => await GetActualCostsDtoQuery().ToListAsync();

        public async Task SetCost(SetCostCommand request)
        {
            var now = DateTime.Now.Date;

            var actualCost = await GetActualCostsQuery()
                .Where(x => x.Type == request.Type)
                .FirstOrDefaultAsync();

            if(actualCost != null)
            {
                actualCost.ToDate = request.FromDate.Date;
            }

            var cost = new Cost
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Type = request.Type,
                Value = request.Value,
                FromDate = request.FromDate,
                ToDate = null
            };

            await ctx.AddAsync(cost);
            await ctx.SaveChangesAsync();
        }

        public async Task<CostsSummaryDto> CalculateCostsSummary(CaluclateCostsSummaryDto data)
        {
            var summary = new CostsSummaryDto();

            var costs = await GetActualCosts();

            summary.PricePerKm = data.PricePerKm;
            summary.PricePerDistance = Math.Round(data.PricePerKm * data.Distance, 2);
            summary.Additions = new List<AdditionCostDto>();

            if (data.RequireLoadingCrew)
            {
                summary.Additions.Add(new AdditionCostDto
                {
                    Name = "Loading",
                    Price = costs.Where(x => x.Type == CostType.Loading).First().Value
                });
            }

            if (data.RequireUnloadingCrew)
            {
                summary.Additions.Add(new AdditionCostDto
                {
                    Name = "Unloading",
                    Price = costs.Where(x => x.Type == CostType.Unloading).First().Value
                });
            }

            summary.Price = Math.Round(summary.PricePerDistance + summary.Additions.Sum(x => x.Price), 2);
            summary.ServiceFee = Math.Round(summary.Price * costs.Where(x => x.Type == CostType.ServiceFee).First().Value / 100, 2);
            summary.TotalPrice = Math.Round(summary.Price + summary.ServiceFee, 2);

            return summary;
        }
    }
}
