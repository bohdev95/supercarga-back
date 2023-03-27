using Microsoft.EntityFrameworkCore;
using SuperCarga.Application.Domain.Common.Abstraction;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Contracts.Drivers.Abstraction;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.Delivered;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.PickedUp;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.Start;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.Details.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Active;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Finished;
using SuperCarga.Application.Domain.Customers.Driver.Dto;
using SuperCarga.Application.Domain.Location.Dto;
using SuperCarga.Application.Domain.Proposals.Common.Models;
using SuperCarga.Application.Exceptions;
using SuperCarga.Application.Validation;
using SuperCarga.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperCarga.Application.Domain.Costs.Abstraction;
using SuperCarga.Application.Domain.Costs.Dto;

namespace SuperCarga.Domain.Domain.Contracts
{
    public class DriverContractsService : ContractsService, IDriverContractsService
    {
        private readonly IImagesService imagesService;

        public DriverContractsService(SuperCargaContext ctx, IImagesService imagesService) : base(ctx)
        {
            this.imagesService = imagesService;
        }

        public async Task StartContract(StartContractCommand request)
        {
            var contract = await ctx.Contracts
                .Include(x => x.Proposal)
                .Where(x => x.ProposalId == request.Data.ProposalId)
                .FirstOrDefaultAsync();

            if (contract == null)
            {
                throw new NotFoundException(ValidationMessage.NotExist("Contract"));
            }

            if (contract.DriverId != request.User.DriverId || contract.Proposal.DriverId != request.User.DriverId)
            {
                throw new ForbiddenException();
            }

            if (contract.State != ContractState.Created)
            {
                throw new BadRequestException(ValidationMessage.InvalidState("Contract"));
            }

            await UpdateContractStatus(contract, ContractState.Started, false);

            contract.Proposal.State = ProposalState.Hired;

            await ctx.SaveChangesAsync();
        }

        public async Task Delivered(DriverDeliveredCommand request)
        {
            var contract = await ctx.Contracts
                .Include(x => x.Proposal)
                .Where(x => x.Id == request.Data.ContractId)
                .FirstOrDefaultAsync();

            if (contract.DriverId != request.User.DriverId)
            {
                throw new ForbiddenException();
            }

            await UpdateContractStatus(contract, ContractState.Delivered, false);

            contract.DeliveryCargoImagePath = await imagesService.SaveDeliveryCargoImage(request.Data.CargoImage, request.Data.ContractId);
            contract.DeliveryProofImagePath = await imagesService.SaveDeliveryProofImage(request.Data.ProofImage, request.Data.ContractId);

            await ctx.SaveChangesAsync();
        }

        public async Task PickedUp(DriverPickedUpCommand request)
        {
            var contract = await ctx.Contracts
                .Include(x => x.Proposal)
                .Where(x => x.Id == request.Data.ContractId)
                .FirstOrDefaultAsync();

            if (contract.DriverId != request.User.DriverId)
            {
                throw new ForbiddenException();
            }

            await UpdateContractStatus(contract, ContractState.InProgress, false);

            contract.PickUpCargoImagePath = await imagesService.SavePickUpCargoImage(request.Data.CargoImage, request.Data.ContractId);
            contract.PickUpProofImagePath = await imagesService.SavePickUpProofImage(request.Data.ProofImage, request.Data.ContractId);

            await ctx.SaveChangesAsync();
        }

        public async Task<DriverContractDetailsDto> GetContractDetails(GetDriverContractDetailsQuery request)
        {
            var dto = await ctx.Contracts
                .Include(x => x.Job)
                .ThenInclude(x => x.VehiculeType)
                .Include(x => x.Customer)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == request.Data.ContractId)
                .Where(x => x.DriverId == request.User.DriverId.Value)
                .Select(x => new DriverContractDetailsDto
                {
                    Id = x.Id,
                    Created = x.Created,
                    Rating = x.Rating,
                    VehiculeTypeName = x.Job.VehiculeType.Name,
                    Description = x.Job.Description,
                    Tittle = x.Job.Tittle,
                    Distance = x.Job.Distance,
                    PickupDate = x.Job.PickupDate,
                    DeliveryDate = x.Job.DeliveryDate,
                    Cargo = x.Job.GetDimensionsCargo(),
                    Origin = x.Job.GetOrigin(),
                    Destination = x.Job.GetDestination(),
                    PaymentState = x.PaymentState,
                    State = x.State,
                    StateChanged = x.History.OrderByDescending(x => x.Created).Select(x => x.Created).FirstOrDefault(),
                    IsInDispute = false, //TODO
                    PickUpCargoImagePath = x.PickUpCargoImagePath,
                    DeliveryCargoImagePath = x.DeliveryCargoImagePath,
                    PickUpProofImagePath = x.PickUpProofImagePath,
                    DeliveryProofImagePath = x.DeliveryProofImagePath,
                    Customer = x.Customer.GetDriverCustomerDto(),
                    CostsSummary = x.GetCostsSummary()
                })
                .FirstOrDefaultAsync();

            return dto;
        }

        public async Task<ListResponseDto<ActiveContractListITemDto>> ListActiveContracts(DriverListActiveContractsQuery request) =>
            await ListActiveContracts(request.Data, x => x.DriverId == request.User.DriverId.Value);

        public async Task<ListResponseDto<FinishedContractListITemDto>> ListFinishedContracts(DriverListFinishedContractsQuery request) =>
            await ListFinishedContracts(request.Data, x => x.DriverId == request.User.DriverId.Value);


    }
}
