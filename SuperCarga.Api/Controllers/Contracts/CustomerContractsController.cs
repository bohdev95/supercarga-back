using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Common.Dto;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.Add;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.ConfirmDelivery;
using SuperCarga.Application.Domain.Contracts.Customers.Commands.Finalize;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.Details;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Active;
using SuperCarga.Application.Domain.Contracts.Customers.Queries.List.Finished;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Api.Controllers.Contracts
{
    [ApiController]
    [Authorize(Roles = Roles.Customer)]
    [Route("customers/contracts")]
    [Tags("Contracts")]
    public class CustomerContractsController : BaseController
    {
        public CustomerContractsController(IMediator mediator, IAuthService authService, ILogger<CustomerContractsController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Accept proposal and add contract
        /// </summary>
        /// <remarks>
        /// Accept proposal and add contract
        /// </remarks>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddContractCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AddContractCommandResponse>> AddContract(AddContractRequest request)
        {
            return await ExecuteUserRequest<AddContractCommand, AddContractRequest>(nameof(AddContract), request);
        }

        /// <summary>
        /// Confirm delivery
        /// </summary>
        /// <remarks>
        /// Confirm delivery by customer
        /// </remarks>
        [HttpPost("confirm-delivery")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerConfirmDeliveryCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CustomerConfirmDeliveryCommandResponse>> ConfirmDelivery(CustomerConfirmDeliveryRequest request)
        {
            return await ExecuteUserRequest<CustomerConfirmDeliveryCommand, CustomerConfirmDeliveryRequest>(nameof(ConfirmDelivery), request);
        }

        /// <summary>
        /// Finalize
        /// </summary>
        /// <remarks>
        /// Finalize contract by customer
        /// </remarks>
        [HttpPost("finalize")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerConfirmDeliveryCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CustomerFinalizeCommandResponse>> Finalize(CustomerFinalizeRequest request)
        {
            return await ExecuteUserRequest<CustomerFinalizeCommand, CustomerFinalizeRequest>(nameof(ConfirmDelivery), request);
        }

        /// <summary>
        /// Get active contracts
        /// </summary>
        /// <remarks>
        /// Returns customers contracts with state STARTED, IN_PROGRESS, DELIVERED or DELIVERED_CONFIRMED.
        /// </remarks>
        [HttpGet("list/active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<ActiveContractListITemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<ActiveContractListITemDto>>> ListActiveContracts([FromQuery] CustomerListActiveContractsRequest request)
        {
            return await ExecuteUserRequest<CustomerListActiveContractsQuery, CustomerListActiveContractsRequest>(nameof(ListActiveContracts), request);
        }

        /// <summary>
        /// Get finished contracts
        /// </summary>
        /// <remarks>
        /// Returns customers contracts with state CANCELED or CLOSED.
        /// </remarks>
        [HttpGet("list/finished")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<FinishedContractListITemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<FinishedContractListITemDto>>> ListFinishedContracts([FromQuery] CustomerListFinishedContractsRequest request)
        {
            return await ExecuteUserRequest<CustomerListFinishedContractsQuery, CustomerListFinishedContractsRequest>(nameof(ListFinishedContracts), request);
        }

        /// <summary>
        /// Get contract details
        /// </summary>
        /// <remarks>
        /// Returns contract details to customers views
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCustomerContractDetailsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetCustomerContractDetailsQueryResponse>> GetContractDetails([FromQuery] GetCustomerContractDetailsRequest request)
        {
            return await ExecuteUserRequest<GetCustomerContractDetailsQuery, GetCustomerContractDetailsRequest>(nameof(GetContractDetails), request);
        }

    }
}
