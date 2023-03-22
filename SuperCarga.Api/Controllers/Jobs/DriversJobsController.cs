using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Commands.AddToFavorites;
using SuperCarga.Application.Domain.Jobs.Drivers.Commands.RemoveFromFavorites;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.All;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Drivers.Queries.List.Favorites;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Api.Controllers.Jobs
{
    [ApiController]
    [Authorize(Roles = Roles.Driver)]
    [Route("drivers/jobs")]
    [Tags("Jobs")]
    public class DriversJobsController : BaseController
    {
        public DriversJobsController(IMediator mediator, IAuthService authService, ILogger<DriversJobsController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Get all jobs
        /// </summary>
        /// <remarks>
        /// Returns all jobs to drivers views.
        /// Returns only jobs that match drivers vehicule type.
        /// </remarks>
        [HttpGet("list/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<DriverJobListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<DriverJobListItemDto>>> ListAllJobs([FromQuery] DriverListAllJobsRequest request)
        {
            return await ExecuteUserRequest<DriverListAllJobsQuery, DriverListAllJobsRequest>(nameof(ListAllJobs), request);
        }

        /// <summary>
        /// Get favorites jobs
        /// </summary>
        /// <remarks>
        /// Returns favorites jobs to drivers views.
        /// Returns only jobs that match drivers vehicule type.
        /// </remarks>
        [HttpGet("list/favorites")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<DriverJobListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<DriverJobListItemDto>>> ListFavoritesJobs([FromQuery] DriverListFavoritesJobsRequest request)
        {
            return await ExecuteUserRequest<DriverListFavoritesJobsQuery, DriverListFavoritesJobsRequest>(nameof(ListFavoritesJobs), request);
        }

        /// <summary>
        /// Get jobs details
        /// </summary>
        /// <remarks>
        /// Returns jobs details to drivers views
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDriverJobsDetailsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetDriverJobsDetailsQueryResponse>> GetJobsDetails([FromQuery] GetDriverJobsDetailsRequest request)
        {
            return await ExecuteUserRequest<GetDriverJobsDetailsQuery, GetDriverJobsDetailsRequest>(nameof(GetJobsDetails), request);
        }

        /// <summary>
        /// Add job to favorite
        /// </summary>
        /// <remarks>
        /// Add job to favorite list by driver.
        /// </remarks>
        [HttpPost("favorites/add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddJobToFavoritesCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AddJobToFavoritesCommandResponse>> AddToFavorites(AddJobToFavoritesRequest request)
        {
            return await ExecuteUserRequest<AddJobToFavoritesCommand, AddJobToFavoritesRequest>(nameof(AddToFavorites), request);
        }

        /// <summary>
        /// Remove job from favorite
        /// </summary>
        /// <remarks>
        /// Remove job from favorite list by driver.
        /// </remarks>
        [HttpPost("favorites/remove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RemoveJobFromFavoritesCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<RemoveJobFromFavoritesCommandResponse>> RemoveFromFavorites(RemoveJobFromFavoritesRequest request)
        {
            return await ExecuteUserRequest<RemoveJobFromFavoritesCommand, RemoveJobFromFavoritesRequest>(nameof(RemoveFromFavorites), request);
        }

    }
}
