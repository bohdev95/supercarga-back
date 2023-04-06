using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperCarga.Application.Domain.Chats.Abstraction;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Domain.Domain.Users.Auth;
using System.Net.WebSockets;
using System.Text;

namespace SuperCarga.Api.Controllers.Chat
{
    //[Authorize]
    public class ChatWebSocketController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IChatService _chatService;
        public ChatWebSocketController(IAuthService authService, IChatService chatService)
        {
            _authService = authService;
            _chatService = chatService;
        }

        [HttpGet("ws/chats")]
        public async Task GetUserChats()
        {

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var websocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var user = await _authService.GetUser(accessToken);

                DateTime? lastUpdatedDatetime = null;

                while (user != null)
                {

                    var results = await _chatService.GetUserChatsAsync(user.Id, lastUpdatedDatetime);

                    if (results.Count > 0)
                    {

                        lastUpdatedDatetime = results[results.Count - 1].LastUpdatedDateTime;

                        var bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(results));
                        var arraySegment = new ArraySegment<byte>(bytes);

                        await websocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                    }

                    Thread.Sleep(1000);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }


        [HttpGet("ws/chats/to")]
        public async Task GetUserChatsTo(Guid ToUserId)
        {

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var websocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var user = await _authService.GetUser(accessToken);

                DateTime? lastUpdatedDatetime = null;

                while (user != null)
                {

                    var results = await _chatService.GetUserToUserChatsAsync(user.Id, ToUserId, lastUpdatedDatetime);

                    if (results.Count > 0)
                    {

                        lastUpdatedDatetime = results[results.Count - 1].UpdatedDateTime;

                        var bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(results));
                        var arraySegment = new ArraySegment<byte>(bytes);

                        await websocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                    }

                    Thread.Sleep(1000);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

    }
}
