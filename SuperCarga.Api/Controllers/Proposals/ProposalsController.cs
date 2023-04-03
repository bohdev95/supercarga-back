using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Contracts.Common.Models;
using SuperCarga.Application.Domain.Proposals.Common.Models;

namespace SuperCarga.Api.Controllers.Proposals
{
    [ApiController]
    [Authorize]
    [Route("proposals")]
    [Tags("Proposals")]
    public class ProposalsController : ControllerBase
    {
        /// <summary>
        /// Get proposals active states
        /// </summary>
        /// <remarks>
        /// Get proposals active states
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
            return ProposalState.Active;
        }

        /// <summary>
        /// Get proposals archived states
        /// </summary>
        /// <remarks>
        /// Get proposals archived states
        /// </remarks>
        [HttpGet("states/archived")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<string>>> GetArchivedStates()
        {
            return ProposalState.Archived;
        }
    }
}
