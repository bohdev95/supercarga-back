using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Chats.Abstraction;
using SuperCarga.Application.Domain.Chats.Commands.CreateMessage;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers.Chat
{

    [Route("chat")]
    public class ChatController : BaseController
    {
        private IChatService _chatService;
        public ChatController(IMediator mediator, IAuthService authService, ILogger<CostsController> logger, IChatService chatService)
        : base(mediator, authService, logger)
        {
            _chatService = chatService;
        }

        [HttpPost("message")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateMessageCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateMessageCommandResponse>> CreateMessage(CreateMessageCommand command)
        {

            var response = await Execute(nameof(CreateMessage), command);

            return Ok(response);
        }

    }
}
