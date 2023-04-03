using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Contracts.Common.Models;

namespace SuperCarga.Api.Controllers.Contracts
{
    [ApiController]
    [Authorize]
    [Route("contracts")]
    [Tags("Contracts")]
    public class ContractsController : ControllerBase
    {
        /// <summary>
        /// Get contracts active states
        /// </summary>
        /// <remarks>
        /// Get contracts active states
        /// </remarks>
        [HttpGet("states/active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<string>>> GetActiveStates()
        {
            return ContractState.Active;
        }

        /// <summary>
        /// Get contracts finished states
        /// </summary>
        /// <remarks>
        /// Get contracts finished states
        /// </remarks>
        [HttpGet("states/finished")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<string>>> GetFinishedStates()
        {
            return ContractState.Finished;
        }
    }
}
