using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Costs.Dto;
using SuperCarga.Application.Domain.Customers.Driver.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using SuperCarga.Application.Domain.Proposals.Drivers.Abstraction;
using SuperCarga.Application.Domain.Proposals.Drivers.Commands.Add;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Active;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Archived;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Common.Dto;
using SuperCarga.Application.Exceptions;
using SuperCarga.Application.Validation;
using SuperCarga.Domain.Domain.Common;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Domain.Domain.Proposals
{
    public class DriverProposalsService : IDriverProposalsService
    {
        private readonly SuperCargaContext ctx;
        private readonly ICostsService costsService;

        public DriverProposalsService(SuperCargaContext ctx, ICostsService costsService)
        {
            this.ctx = ctx;
            this.costsService = costsService;
        }

        public async Task<Guid> AddProposal(AddProposalCommand request)
        {
            var alreadyAdded = await ctx.Proposals
                .Where(x => x.JobId == request.Data.JobId)
                .Where(x => x.DriverId == request.User.DriverId.Value)
                .AnyAsync();

            if(alreadyAdded)
            {
                throw new AlreadyExistsException(ValidationMessage.AlreadyExist("Proposal"));
            }

            var proposal = new Proposal
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                JobId = request.Data.JobId,
                DriverId = request.User.DriverId.Value,
                PricePerKm = request.Data.PricePerKm,
                State = ProposalState.Pending
            };

            await ctx.AddAsync(proposal);
            await ctx.SaveChangesAsync();

            return proposal.Id;
        }

        public async Task<DriverProposalDetailsDto> GetProposalsDetails(GetDriverProposalDetailsQuery request)
        {
            var proposalWasCreatedByDriver = await ctx.Proposals
                .Where(x => x.Id == request.Data.ProposalId)
                .Where(x => x.DriverId == request.User.DriverId.Value)
                .AnyAsync();

            if(!proposalWasCreatedByDriver)
            {
                throw new ForbiddenException();
            }

            var dto = await ctx.Proposals
                .Include(x => x.Job)
                .ThenInclude(x => x.VehiculeType)
                .Include(x => x.Job)
                .ThenInclude(x => x.Customer)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == request.Data.ProposalId)
                .Select(x => new DriverProposalDetailsDto
                {
                    Id = x.Id,
                    ProposalCreated = x.Created,
                    JobCreated = x.Created,
                    VehiculeTypeName = x.Job.VehiculeType.Name,
                    RequireLoadingCrew = x.Job.RequireLoadingCrew,
                    RequireUnloadingCrew = x.Job.RequireUnloadingCrew,
                    Description = x.Job.Description,
                    Tittle = x.Job.Tittle,
                    Distance = x.Job.Distance,
                    JobsPricePerKm = x.Job.PricePerKm,
                    DriversPricePerKm = x.PricePerKm,
                    State = x.State,
                    PickupDate = x.Job.PickupDate,
                    DeliveryDate = x.Job.DeliveryDate,
                    Cargo = x.Job.GetDimensionsCargo(),
                    Origin = x.Job.GetOrigin(),
                    Destination = x.Job.GetDestination(),
                    Customer = x.Job.Customer.GetDriverCustomerDto()
                })
                .FirstOrDefaultAsync();

            var contract = await ctx.Contracts
                .Include(x => x.Additions)
                .Where(x => x.ProposalId == request.Data.ProposalId)
                .FirstOrDefaultAsync();

            if(contract != null)
            {
                dto.CostsSummary = contract.GetCostsSummary();
            }
            else
            {
                dto.CostsSummary = await costsService.CalculateCostsSummary(new CaluclateCostsSummaryDto
                {
                    PricePerKm = dto.DriversPricePerKm,
                    Distance = dto.Distance,
                    RequireLoadingCrew = dto.RequireLoadingCrew,
                    RequireUnloadingCrew = dto.RequireUnloadingCrew
                });
            }

            return dto;
        }

        public async Task<ListResponseDto<DriverProposalListItemDto>> ListActiveProposals(DriverListActiveProposalsQuery request) => await ListProposals(request, ProposalState.Active);

        public async Task<ListResponseDto<DriverProposalListItemDto>> ListArchivedProposals(DriverListArchivedProposalsQuery request) => await ListProposals(request, ProposalState.Archived);

        private async Task<ListResponseDto<DriverProposalListItemDto>> ListProposals<T>(UserRequest<T, ListResponseDto<DriverProposalListItemDto>> request, List<string> states) where T : DriverListProposalsRequest
        {
            var query = ctx.Proposals
                .Include(x => x.Job)
                .ThenInclude(x => x.Customer)
                .ThenInclude(x => x.User)
                .Where(x => x.DriverId == request.User.DriverId)
                .Where(x => states.Contains(x.State))
                .Select(x => new DriverProposalListItemDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    JobId = x.JobId,
                    PricePerKm = x.PricePerKm,
                    JobTittle =  x.Job.Tittle,
                    State = x.State,
                    PickupDate = x.Job.PickupDate,
                    Destination = x.Job.GetDestination(),
                    Origin = x.Job.GetOrigin(),
                    Customer = x.Job.Customer.GetDriverCustomerDto()
                })
                .AsQueryable();

            if (request.Data.JobId != null)
            {
                query = query.Where(x => x.JobId == request.Data.JobId).AsQueryable();
            }

            if (request.Data.CreatedFrom != null)
            {
                query = query.Where(x => x.Created >= request.Data.CreatedFrom.Value).AsQueryable();
            }

            if (request.Data.CreatedTo != null)
            {
                query = query.Where(x => x.Created <= request.Data.CreatedTo.Value).AsQueryable();
            }

            if (request.Data.PickupFrom != null)
            {
                query = query.Where(x => x.PickupDate >= request.Data.PickupFrom.Value).AsQueryable();
            }

            if (request.Data.PickupTo != null)
            {
                query = query.Where(x => x.PickupDate <= request.Data.PickupTo.Value).AsQueryable();
            }

            if (!string.IsNullOrWhiteSpace(request.Data.State))
            {
                query = query.Where(x => x.State == request.Data.State).AsQueryable();
            }

            if (request.Data.PricePerKmFrom != null)
            {
                query = query.Where(x => x.PricePerKm >= request.Data.PricePerKmFrom.Value).AsQueryable();
            }

            if (request.Data.PricePerKmTo != null)
            {
                query = query.Where(x => x.PricePerKm <= request.Data.PricePerKmTo.Value).AsQueryable();
            }

            var proposals = await query
                .OrderByDescending(x => x.Created)
                .Paginate(request.Data);

            return proposals;
        }
    }
}
