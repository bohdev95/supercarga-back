using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.FreeEstimation.Commands.Calculate;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers
{
    [ApiController]
    [Route("free-estimation")]
    [Tags("Free Estimation")]
    public class FreeEstimationController : BaseController
    {
        public FreeEstimationController(IMediator mediator, IAuthService authService, ILogger<FreeEstimationController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Calculate transport cost 
        /// </summary>
        /// <remarks>
        /// Calculate transport cost
        /// </remarks>
        [HttpPost("Calculate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FreeEstimationCalculateCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FreeEstimationCalculateCommandResponse>> Calculate(FreeEstimationCalculateCommand request)
        {
            return await Execute(nameof(Calculate), request);
        }
    }
}
