using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Location.Commands.CheckDistance;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("location")]
    [Tags("Location")]
    public class LocationController : BaseController
    {
        public LocationController(IMediator mediator, IAuthService authService, ILogger<LocationController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Check distance 
        /// </summary>
        /// <remarks>
        /// Check distance
        /// </remarks>
        [HttpPost("check-distance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckDistanceCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckDistanceCommandResponse>> CheckDistance(CheckDistanceCommand request)
        {
            return await Execute(nameof(CheckDistance), request);
        }
    }
}
