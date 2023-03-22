using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Drivers.Commands.Add;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Active;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Archived;
using SuperCarga.Application.Domain.Proposals.Drivers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Api.Controllers.Proposals
{
    [ApiController]
    [Authorize(Roles = Roles.Driver)]
    [Route("drivers/proposals")]
    [Tags("Proposals")]
    public class DriversProposalsController : BaseController
    {
        public DriversProposalsController(IMediator mediator, IAuthService authService, ILogger<DriversProposalsController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Add proposal
        /// </summary>
        /// <remarks>
        /// Add proposal to job by driver
        /// </remarks>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddProposalCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AddProposalCommandResponse>> AddProposal(AddProposalRequest request)
        {
            return await ExecuteUserRequest<AddProposalCommand, AddProposalRequest>(nameof(AddProposal), request);
        }

        /// <summary>
        /// Get proposal details
        /// </summary>
        /// <remarks>
        /// Returns proposal details to drivers views
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDriverProposalDetailsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetDriverProposalDetailsQueryResponse>> GetProposalDetails([FromQuery] GetDriverProposalDetailsRequest request)
        {
            return await ExecuteUserRequest<GetDriverProposalDetailsQuery, GetDriverProposalDetailsRequest>(nameof(GetProposalDetails), request);
        }

        /// <summary>
        /// List active proposals
        /// </summary>
        /// <remarks>
        /// Get drivers proposal with state PENDING or ACCEPTED
        /// </remarks>
        [HttpGet("list/active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<DriverProposalListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<DriverProposalListItemDto>>> ListActiveProposals([FromQuery] DriverListActiveProposalsRequest request)
        {
            return await ExecuteUserRequest<DriverListActiveProposalsQuery, DriverListActiveProposalsRequest>(nameof(ListActiveProposals), request);
        }

        /// <summary>
        /// List archived proposals
        /// </summary>
        /// <remarks>
        /// Get drivers proposal with state CLOSED, HIRED or CANCELED
        /// </remarks>
        [HttpGet("list/archived")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<DriverProposalListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<DriverProposalListItemDto>>> ListArchivedProposals([FromQuery] DriverListArchivedProposalsRequest request)
        {
            return await ExecuteUserRequest<DriverListArchivedProposalsQuery, DriverListArchivedProposalsRequest>(nameof(ListArchivedProposals), request);
        }

    }
}
