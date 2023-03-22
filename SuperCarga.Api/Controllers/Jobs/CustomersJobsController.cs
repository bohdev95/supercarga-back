using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Common.Dto;
using SuperCarga.Application.Domain.Jobs.Customers.Commands.Add;
using SuperCarga.Application.Domain.Jobs.Customers.Commands.Close;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.Details;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Active;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Archived;
using SuperCarga.Application.Domain.Jobs.Customers.Queries.List.Common.Dto;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Api.Controllers.Jobs
{
    [ApiController]
    [Authorize(Roles = Roles.Customer)]
    [Route("customers/jobs")]
    [Tags("Jobs")]
    public class CustomersJobsController : BaseController
    {
        public CustomersJobsController(IMediator mediator, IAuthService authService, ILogger<CustomersJobsController> logger) 
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Add job
        /// </summary>
        /// <remarks>
        /// Add new job by customer. 
        /// Before add job calculation is needed.
        /// </remarks>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddJobCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<AddJobCommandResponse>> AddJob(AddJobRequest request)
        {
            return await ExecuteUserRequest<AddJobCommand, AddJobRequest>(nameof(AddJob), request);
        }

        /// <summary>
        /// Close job
        /// </summary>
        /// <remarks>
        /// Close by customer. 
        /// </remarks>
        [HttpPost("close")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CloseJobCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CloseJobCommandResponse>> CloseJob(CloseJobRequest request)
        {
            return await ExecuteUserRequest<CloseJobCommand, CloseJobRequest>(nameof(AddJob), request);
        }

        /// <summary>
        /// Get customers active jobs
        /// </summary>
        /// <remarks>
        /// Return customers active jobs to customer views.
        /// </remarks>
        [HttpGet("list/active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<CustomerJobListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<CustomerJobListItemDto>>> ListActiveJobs([FromQuery] CustomerListActiveJobsRequest request)
        {
            return await ExecuteUserRequest<CustomerListActiveJobsQuery, CustomerListActiveJobsRequest>(nameof(ListActiveJobs), request);
        }

        /// <summary>
        /// Get customers archived jobs
        /// </summary>
        /// <remarks>
        /// Return customers archived jobs to customer views.
        /// </remarks>
        [HttpGet("list/archived")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<CustomerJobListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ListResponseDto<CustomerJobListItemDto>>> ListArchivedJobs([FromQuery] CustomerListArchivedJobsRequest request)
        {
            return await ExecuteUserRequest<CustomerListArchivedJobsQuery, CustomerListArchivedJobsRequest>(nameof(ListArchivedJobs), request);
        }

        /// <summary>
        /// Get jobs details
        /// </summary>
        /// <remarks>
        /// Returns jobs details to customers views
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCustomerJobsDetailsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetCustomerJobsDetailsQueryResponse>> GetJobsDetails([FromQuery] GetCustomerJobsDetailsRequest request)
        {
            return await ExecuteUserRequest<GetCustomerJobsDetailsQuery, GetCustomerJobsDetailsRequest>(nameof(GetJobsDetails), request);
        }

    }
}
