using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    [Route("admin")]
    [Tags("Admin")]
    public class AdminController : BaseController
    {
        private readonly IMediator mediator;

        public AdminController(IMediator mediator, IAuthService authService, ILogger<AdminController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Some admin action 
        /// </summary>
        /// <remarks>
        /// Some admin action
        /// </remarks>
        [HttpPost("Action")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Login([FromBody] string parameter)
        {
            return Ok("success");
        }

    }
}
