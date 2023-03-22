using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Api.Logging;
using SuperCarga.Application.Domain.Common.Model;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAuthService authService;
        private readonly ILogger logger;

        public BaseController(IMediator mediator, IAuthService authService, ILogger logger)
        {
            this.mediator = mediator;
            this.authService = authService;
            this.logger = logger;
        }

        protected async Task<ActionResult> Execute<TRequest>(string actionName, TRequest request)
        {
            logger.LogData($"{actionName} request", request);

            var response = await mediator.Send(request);

            logger.LogData($"{actionName} response", response);

            return Ok(response);
        }

        protected async Task<ActionResult> ExecuteUserRequest<TUserRequest, TData>(string actionName, TData data) where TUserRequest : IUserRequest<TData>
        {
            logger.LogData($"{actionName} request", data);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var user = await authService.GetUser(accessToken);

            var userRequest = (TUserRequest)Activator.CreateInstance(typeof(TUserRequest));
            userRequest.User = user;
            userRequest.Data = data;

            logger.LogData($"{actionName} user", new
            {
                Email = userRequest.User.Email,
                CustomerId = userRequest.User.CustomerId,
                DriverId = userRequest.User.DriverId
            });

            var response = await mediator.Send(userRequest);

            logger.LogData($"{actionName} response", response);

            return Ok(response);
        }
    }
}
