using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Drivers.Customers.Dto;
using SuperCarga.Application.Domain.Finances.Abstraction;
using SuperCarga.Application.Domain.Finances.Model;
using SuperCarga.Application.Domain.Finances.Queries.GetBalance;
using SuperCarga.Application.Domain.Finances.Queries.GetBalance.Dto;
using SuperCarga.Application.Exceptions;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Finances
{
    public class FinancesService : IFinancesService
    {
        private readonly SuperCargaContext ctx;

        public FinancesService(SuperCargaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<BalanceDto> GetBalance(GetBalanceQuery query)
        {
            var dto = await ctx.Finances
                .Include(x => x.Holds)
                .ThenInclude(x => x.RelatedContract)
                .ThenInclude(x => x.Job)
                .Include(x => x.Holds)
                .ThenInclude(x => x.RelatedContract)
                .ThenInclude(x => x.Driver)
                .ThenInclude(x => x.User)
                .Where(x => x.UserId == query.User.Id)
                .Select(x => new BalanceDto
                {
                    Value = x.Balance,
                    Holds = x.Holds.Select(y => new BalanceHoldDto
                    {
                        Value = y.Value,
                        JobTittle = y.RelatedContract.Job.Tittle,
                        ContractState = y.RelatedContract.State,
                        Driver = y.RelatedContract.Driver.GetCustomerDriverDto()
                    })
                    .ToList()
                })
                .FirstOrDefaultAsync();

            return dto;
        }

        

        

    }
}
