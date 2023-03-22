using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Domain.Domain.Common;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Contracts
{
    public class ContractsService : IContractsService
    {
        protected readonly SuperCargaContext ctx;

        public ContractsService(SuperCargaContext ctx)
        {
            this.ctx = ctx;
        }

        public bool ContractExists(Guid id) => ctx.Contracts.Where(x => x.Id == id).Any();

        public string GetState(Guid id) => ctx.Contracts.Where(x => x.Id == id).Select(x => x.State).FirstOrDefault();

        protected async Task UpdateContractStatus(Func<Contract, Task> update, Contract contract)
        {
            await update(contract);

            var contractHistory = new ContractHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ContractId = contract.Id,
                State = contract.State
            };

            await ctx.ContractHistories.AddAsync(contractHistory);
            await ctx.SaveChangesAsync();
        }

        protected async Task<ListResponseDto<ActiveContractListITemDto>> ListActiveContracts(ListRequestDto listRequest, Expression<Func<Contract, bool>> predicate)
        {
            var contracts = await ctx.Contracts
                .Include(x => x.Job)
                .Include(x => x.History)
                .Where(predicate)
                .Where(x => ContractState.Active.Contains(x.State))
                .Select(x => new ActiveContractListITemDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Tittle = x.Job.Tittle,
                    Origin = x.Job.GetOrigin(),
                    Destination = x.Job.GetDestination(),
                    PaymentState = "On delivery confirmation", //TODO
                    State = x.State,
                    StateChanged = x.History.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault(),
                    IsInDispute = false //TODO
                })
                .OrderByDescending(x => x.Created)
                .Paginate(listRequest);

            return contracts;
        }

        protected async Task<ListResponseDto<FinishedContractListITemDto>> ListFinishedContracts(ListRequestDto listRequest, Expression<Func<Contract, bool>> predicate)
        {
            var contracts = await ctx.Contracts
                .Include(x => x.Job)
                .Include(x => x.History)
                .Where(predicate)
                .Where(x => ContractState.Finished.Contains(x.State))
                .Select(x => new FinishedContractListITemDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Rating = x.Rating.Value,
                    Price = x.Price,
                    Tittle = x.Job.Tittle,
                    Origin = x.Job.GetOrigin(),
                    Destination = x.Job.GetDestination(),
                    State = x.State,
                    StateChanged = x.History.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault(),
                })
                .OrderByDescending(x => x.Created)
                .Paginate(listRequest);

            return contracts;
        }


    }
}
