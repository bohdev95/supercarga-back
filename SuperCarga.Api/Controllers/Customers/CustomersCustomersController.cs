using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Customers.Customers.Commands.Register;
using SuperCarga.Application.Domain.Customers.Customers.Commands.UploadIdDocument;
using SuperCarga.Application.Domain.Customers.Customers.Queries.Details;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Api.Controllers.Customers
{
    [ApiController]
    [Authorize(Roles = Roles.Customer)]
    [Route("customers/customers")]
    [Tags("Customers")]
    public class CustomersCustomersController : BaseController
    {
        public CustomersCustomersController(IMediator mediator, IAuthService authService, ILogger<CustomersCustomersController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Get customers details
        /// </summary>
        /// <remarks>
        /// Returns customers details
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCustomersDetailsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetCustomersDetailsQueryResponse>> GetCustomersDetails([FromQuery] GetCustomersDetailsRequest request)
        {
            return await ExecuteUserRequest<GetCustomersDetailsQuery, GetCustomersDetailsRequest>(nameof(GetCustomersDetails), request);
        }

        /// <summary>
        /// Register customer
        /// </summary>
        /// <remarks>
        /// Register customer
        /// </remarks>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterCustomerCommand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegisterCustomerCommandResponse>> RegisterCustomer(RegisterCustomerCommand request)
        {
            return await Execute<RegisterCustomerCommand>(nameof(RegisterCustomer), request);
        }

        /// <summary>
        /// Upload id document
        /// </summary>
        /// <remarks>
        /// Using to upload id document by customer
        /// </remarks>
        [HttpPost("uload-id-document")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UploadIdDocumentCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UploadIdDocumentCommandResponse>> UploadIdDocument(UploadIdDocumentRequest request)
        {
            return await ExecuteUserRequest<UploadIdDocumentCommand, UploadIdDocumentRequest>(nameof(UploadIdDocument), request);
        }
    }
}
