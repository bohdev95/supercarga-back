using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Drivers.Common.Dto;
using SuperCarga.Application.Domain.Drivers.Common.Queries.TopRatedDrivers;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers.Drivers
{
    [ApiController]
    [Route("drivers")]
    [Tags("Drivers")]
    public class DriversController : BaseController
    {
        public DriversController(IMediator mediator, IAuthService authService, ILogger<DriversController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Get top rated drivers
        /// </summary>
        /// <remarks>
        /// Returns top rated drivers
        /// </remarks>
        [HttpGet("list/top-rated")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<TopRatedDriverDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<TopRatedDriverDto>>> GetTopRatedDrivers([FromQuery] TopRatedDriversQuery request)
        {
            return await Execute(nameof(GetTopRatedDrivers), request);
        }
    }
}
