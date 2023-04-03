using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Customers.Common.Dto;
using SuperCarga.Application.Domain.Customers.Common.Queries.TopCustomers;
using SuperCarga.Application.Domain.Users.Abstraction;

namespace SuperCarga.Api.Controllers.Customers
{
    [ApiController]
    [Route("customers")]
    [Tags("Customers")]
    public class CustomersController : BaseController
    {
        public CustomersController(IMediator mediator, IAuthService authService, ILogger<CustomersController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Get top customers
        /// </summary>
        /// <remarks>
        /// Returns top customers
        /// </remarks>
        [HttpGet("list/top")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<TopCustomersDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<TopCustomersDto>>> GetTopCustomers([FromQuery] TopCustomersQuery request)
        {
            return await Execute(nameof(GetTopCustomers), request);
        }
    }
}
