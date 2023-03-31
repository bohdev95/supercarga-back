using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.AddToFavorites;
using SuperCarga.Application.Domain.Proposals.Customers.Commands.RemoveFromFavorites;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.Details;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.All;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Proposals.Customers.Queries.List.Favorites;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Api.Controllers.Proposals
{
    [ApiController]
    [Authorize(Roles = Roles.Customer)]
    [Route("customers/proposals")]
    [Tags("Proposals")]
    public class CustomersProposalsController : BaseController
    {
        public CustomersProposalsController(IMediator mediator, IAuthService authService, ILogger<CustomersProposalsController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Get proposal details
        /// </summary>
        /// <remarks>
        /// Returns proposal details to customers views
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCustomerProposalDetailsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetCustomerProposalDetailsQueryResponse>> GetProposalDetails([FromQuery] GetCustomerProposalDetailsRequest request)
        {
            return await ExecuteUserRequest<GetCustomerProposalDetailsQuery, GetCustomerProposalDetailsRequest>(nameof(GetProposalDetails), request);
        }

        /// <summary>
        /// Get all proposals
        /// </summary>
        /// <remarks>
        /// Returns all proposals to customers views.
        /// </remarks>
        [HttpGet("list/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<CustomerProposalListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<CustomerProposalListItemDto>>> ListAllProposals([FromQuery] CustomerListAllProposalsRequest request)
        {
            return await ExecuteUserRequest<CustomerListAllProposalsQuery, CustomerListAllProposalsRequest>(nameof(ListAllProposals), request);
        }

        /// <summary>
        /// Get favorites proposals
        /// </summary>
        /// <remarks>
        /// Returns favorites proposals to customers views.
        /// </remarks>
        [HttpGet("list/favorites")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<CustomerProposalListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<CustomerProposalListItemDto>>> ListFavoritesProposals([FromQuery] CustomerListFavoritesProposalsRequest request)
        {
            return await ExecuteUserRequest<CustomerListFavoritesProposalsQuery, CustomerListFavoritesProposalsRequest>(nameof(ListFavoritesProposals), request);
        }

        /// <summary>
        /// Add proposal to favorite
        /// </summary>
        /// <remarks>
        /// Add proposal to favorite list by customer.
        /// </remarks>
        [HttpPost("favorites/add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddProposalToFavoritesCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AddProposalToFavoritesCommandResponse>> AddToFavorites(AddProposalToFavoritesRequest request)
        {
            return await ExecuteUserRequest<AddProposalToFavoritesCommand, AddProposalToFavoritesRequest>(nameof(AddToFavorites), request);
        }

        /// <summary>
        /// Remove proposal from favorite
        /// </summary>
        /// <remarks>
        /// Remove proposal from favorite list by customer.
        /// </remarks>
        [HttpPost("favorites/remove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RemoveProposalFromFavoritesCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<RemoveProposalFromFavoritesCommandResponse>> RemoveFromFavorites(RemoveProposalFromFavoritesRequest request)
        {
            return await ExecuteUserRequest<RemoveProposalFromFavoritesCommand, RemoveProposalFromFavoritesRequest>(nameof(RemoveFromFavorites), request);
        }
    }
}
