using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.Delivered;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.PickedUp;
using SuperCarga.Application.Domain.Contracts.Drivers.Commands.Start;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Active;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Drivers.Queries.List.Finished;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Api.Controllers.Contracts
{
    [ApiController]
    [Authorize(Roles = Roles.Driver)]
    [Route("drivers/contracts")]
    [Tags("Contracts")]
    public class DriversContractsController : BaseController
    {
        public DriversContractsController(IMediator mediator, IAuthService authService, ILogger<DriversContractsController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Sign contract as delivered
        /// </summary>
        /// <remarks>
        /// Sign contract as delivered by driver
        /// </remarks>
        [HttpPost("delivered")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverDeliveredCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DriverDeliveredCommandResponse>> Delivered(DriverDeliveredRequest request)
        {
            return await ExecuteUserRequest<DriverDeliveredCommand, DriverDeliveredRequest>(nameof(Delivered), request);
        }

        /// <summary>
        /// Sign contract as pizked up
        /// </summary>
        /// <remarks>
        /// Sign contract as picked up (InProgress contract state) by driver
        /// </remarks>
        [HttpPost("picked-up")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverPickedUpCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DriverPickedUpCommandResponse>> PickedUp(DriverPickedUpRequest request)
        {
            return await ExecuteUserRequest<DriverPickedUpCommand, DriverPickedUpRequest>(nameof(PickedUp), request);
        }

        /// <summary>
        /// Start contract
        /// </summary>
        /// <remarks>
        /// Start a new contract by driver by driver
        /// </remarks>
        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StartContractCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<StartContractCommandResponse>> StartContract(StartContractRequest request)
        {
            return await ExecuteUserRequest<StartContractCommand, StartContractRequest>(nameof(StartContract), request);
        }

        /// <summary>
        /// Get active contracts
        /// </summary>
        /// <remarks>
        /// Returns drivers contracts with state STARTED, IN_PROGRESS, DELIVERED or DELIVERED_CONFIRMED.
        /// </remarks>
        [HttpGet("list/active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<ActiveContractListITemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<ActiveContractListITemDto>>> ListActiveContracts([FromQuery] DriverListActiveContractsRequest request)
        {
            return await ExecuteUserRequest<DriverListActiveContractsQuery, DriverListActiveContractsRequest>(nameof(ListActiveContracts), request);
        }

        /// <summary>
        /// Get finished contracts
        /// </summary>
        /// <remarks>
        /// Returns drivers contracts with state CANCELED or CLOSED.
        /// </remarks>
        [HttpGet("list/finished")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<FinishedContractListITemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<FinishedContractListITemDto>>> ListFinishedContracts([FromQuery] DriverListFinishedContractsRequest request)
        {
            return await ExecuteUserRequest<DriverListFinishedContractsQuery, DriverListFinishedContractsRequest>(nameof(ListFinishedContracts), request);
        }

        /// <summary>
        /// Get contract details
        /// </summary>
        /// <remarks>
        /// Returns contract details to drivers views
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDriverContractDetailsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetDriverContractDetailsQueryResponse>> GetContractDetails([FromQuery] GetDriverContractDetailsRequest request)
        {
            return await ExecuteUserRequest<GetDriverContractDetailsQuery, GetDriverContractDetailsRequest>(nameof(GetContractDetails), request);
        }

    }
}
