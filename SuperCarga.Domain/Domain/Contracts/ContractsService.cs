using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Abstraction;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Domain.Domain.Common;
using SuperCarga.Persistence.Database;
using System.Linq.Expressions;

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

        protected async Task UpdateContractStatus(Contract contract, string state, bool save)
        {
            contract.State = state;

            var contractHistory = new ContractHistory
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                ContractId = contract.Id,
                State = contract.State
            };

            await ctx.ContractHistories.AddAsync(contractHistory);

            if(save)
            {
                await ctx.SaveChangesAsync();
            }
        }

        protected async Task<ListResponseDto<ActiveContractListITemDto>> ListActiveContracts(ListContractRequest listRequest, Expression<Func<Contract, bool>> predicate)
        {
            var query = ctx.Contracts
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
                    PaymentState = x.PaymentState,
                    State = x.State,
                    StateChanged = x.History.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault(),
                    IsInDispute = false //TODO
                })
                .AsQueryable();

            //TODO duplicated
            if (listRequest.CreatedFrom != null)
            {
                query = query.Where(x => x.Created.Date >= listRequest.CreatedFrom.Value.Date).AsQueryable();
            }

            if (listRequest.CreatedTo != null)
            {
                query = query.Where(x => x.Created.Date <= listRequest.CreatedTo.Value.Date).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(listRequest.State))
            {
                query = query.Where(x => x.State == listRequest.State).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(listRequest.Search))
            {
                var search = listRequest.Search.ToLower().Trim();

                query = query.Where(x => x.Tittle.Contains(search)).AsQueryable();
            }

            var contracts = await query
                .OrderByDescending(x => x.Created)
                .Paginate(listRequest);

            return contracts;
        }

        protected async Task<ListResponseDto<FinishedContractListITemDto>> ListFinishedContracts(ListContractRequest listRequest, Expression<Func<Contract, bool>> predicate)
        {
            var query = ctx.Contracts
                .Include(x => x.Job)
                .Include(x => x.History)
                .Where(predicate)
                .Where(x => ContractState.Finished.Contains(x.State))
                .Select(x => new FinishedContractListITemDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Rating = x.Rating.Value,
                    Tittle = x.Job.Tittle,
                    Origin = x.Job.GetOrigin(),
                    Destination = x.Job.GetDestination(),
                    State = x.State,
                    StateChanged = x.History.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault(),
                    ContractValue = x.TotalPrice
                })
                .AsQueryable();

            //TODO duplicated
            if (listRequest.CreatedFrom != null)
            {
                query = query.Where(x => x.Created.Date >= listRequest.CreatedFrom.Value.Date).AsQueryable();
            }

            if (listRequest.CreatedTo != null)
            {
                query = query.Where(x => x.Created.Date <= listRequest.CreatedTo.Value.Date).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(listRequest.State))
            {
                query = query.Where(x => x.State == listRequest.State).AsQueryable();
            }

            if(!string.IsNullOrWhiteSpace(listRequest.Search))
            {
                var search = listRequest.Search.ToLower().Trim();

                query = query.Where(x => x.Tittle.Contains(search)).AsQueryable();
            }

            var contracts = await query
                .OrderByDescending(x => x.Created)
                .Paginate(listRequest);

            return contracts;
        }


    }
}
