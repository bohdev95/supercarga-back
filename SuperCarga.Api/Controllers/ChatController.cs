using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Chats.Abstraction;
using SuperCarga.Application.Domain.Chats.Commands;
using SuperCarga.Application.Domain.Costs.Commands.Set;
using SuperCarga.Application.Domain.Users.Abstraction;
using System.Threading.Channels;

namespace SuperCarga.Api.Controllers
{

    [Route("chat")]
    public class ChatController : BaseController
    {
        private IChatService _chatService;
        public ChatController(IMediator mediator, IAuthService authService, ILogger<CostsController> logger, IChatService chatService)
        : base(mediator, authService, logger)
        {
            this._chatService = chatService;
        }

        [HttpPost("message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateChatResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateChatResponse>> CreateMessage(CreateChatCommand command) { 
            
            var response = await Execute<CreateChatCommand>(nameof(CreateMessage), command);


            return Ok(response);
        }

    }
}
