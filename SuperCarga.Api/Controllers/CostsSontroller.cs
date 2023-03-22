using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Costs.Commands.Set;
using SuperCarga.Application.Domain.Costs.Queries.GetActual;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("costs")]
    [Tags("Costs")]
    public class CostsController : BaseController
    {
        public CostsController(IMediator mediator, IAuthService authService, ILogger<CostsController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Set costs
        /// </summary>
        /// <remarks>
        /// Set cost. Cost Types:
        /// - SERVICE_FEE
        /// - LOADING
        /// - UNLOADING
        /// </remarks>
        [Authorize(Roles = "ADMIN")]
        [HttpPost("set")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetCostCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<SetCostCommandResponse>> SetCost(SetCostCommand request)
        {
            return await Execute<SetCostCommand>(nameof(SetCost), request);
        }

        /// <summary>
        /// Get actual costs
        /// </summary>
        /// <remarks>
        /// Get actual costs
        /// </remarks>
        [HttpGet("get-actual")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActualCostsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetActualCostsQueryResponse>> GetActualCosts([FromQuery] GetActualCostsQuery request)
        {
            return await Execute<GetActualCostsQuery>(nameof(GetActualCosts), request);
        }
    }
}
