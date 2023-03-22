using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Commands.AcceptTerms;
using SuperCarga.Application.Domain.Users.Commands.ChangeImage;
using SuperCarga.Application.Domain.Users.Commands.Login;
using SuperCarga.Application.Domain.Users.Commands.Refresh;

namespace SuperCarga.Api.Controllers
{
    [ApiController]
    [Route("users")]
    [Tags("Users")]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator, IAuthService authService, ILogger<UsersController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Login user 
        /// </summary>
        /// <remarks>
        /// Using login user
        /// </remarks>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginCommandResponse>> Login(LoginCommand request)
        {
            return await Execute<LoginCommand>(nameof(Login), request);
        }

        /// <summary>
        /// Refresh acces token 
        /// </summary>
        /// <remarks>
        /// Using to refresh acces token
        ///  - AccesToken lifetime: 1h
        ///  - RefreshToken lifetime: 7d
        /// </remarks>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RefreshCommandResponse>> Refresh(RefreshCommand request)
        {
            return await Execute<RefreshCommand>(nameof(Refresh), request);
        }

        /// <summary>
        /// Change user image
        /// </summary>
        /// <remarks>
        /// Change user image
        /// </remarks>
        [HttpPost("change-image")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangeImageCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ChangeImageCommandResponse>> ChangeImage(ChangeImageRequest request)
        {
            return await ExecuteUserRequest<ChangeImageCommand, ChangeImageRequest>(nameof(ChangeImage), request);
        }

        /// <summary>
        /// Accept terms and conditions
        /// </summary>
        /// <remarks>
        /// Using to accept terms and conditions by user
        /// </remarks>
        [HttpPost("accept-terms")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AcceptTermsCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AcceptTermsCommandResponse>> AcceptTerms(AcceptTermsRequest request)
        {
            return await ExecuteUserRequest<AcceptTermsCommand, AcceptTermsRequest>(nameof(AcceptTerms), request);
        }

    }
}
