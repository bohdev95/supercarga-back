using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Finances.Queries.GetBalance;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("finances")]
    [Tags("Finances")]
    public class FinancesController : BaseController
    {
        public FinancesController(IMediator mediator, IAuthService authService, ILogger<UsersController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Get user balance
        /// </summary>
        /// <remarks>
        /// Get user balance
        /// </remarks>
        [HttpGet("balance")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBalanceQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetBalanceQueryResponse>> GetBalance([FromQuery] GetBalanceRequest request)
        {
            return await ExecuteUserRequest<GetBalanceQuery, GetBalanceRequest>(nameof(GetBalance), request);
        }

    }
}
