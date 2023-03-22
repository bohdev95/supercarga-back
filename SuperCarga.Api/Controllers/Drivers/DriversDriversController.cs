using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Drivers.Drivers.Commands.Edit;
using SuperCarga.Application.Domain.Drivers.Drivers.Commands.Register;
using SuperCarga.Application.Domain.Drivers.Drivers.Commands.UploadDrivingLicense;
using SuperCarga.Application.Domain.Drivers.Drivers.Queries.Details;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.Users.Models;

namespace SuperCarga.Api.Controllers.Drivers
{
    [ApiController]
    [Authorize(Roles = Roles.Driver)]
    [Route("drivers/drivers")]
    [Tags("Drivers")]
    public class DriversDriversController : BaseController
    {
        public DriversDriversController(IMediator mediator, IAuthService authService, ILogger<DriversDriversController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Get drivers details
        /// </summary>
        /// <remarks>
        /// Returns drivers details
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDriverDetailsQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetDriverDetailsQueryResponse>> GetDriverDetails([FromQuery] GetDriverDetailsRequest request)
        {
            return await ExecuteUserRequest<GetDriverDetailsQuery, GetDriverDetailsRequest>(nameof(GetDriverDetails), request);
        }

        /// <summary>
        /// Edit driver data
        /// </summary>
        /// <remarks>
        /// Using to edit driver data like vehicule type
        /// </remarks>
        [HttpPost("edit")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EditDriverCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<EditDriverCommandResponse>> EditDriver(EditDriverRequest request)
        {
            return await ExecuteUserRequest<EditDriverCommand, EditDriverRequest>(nameof(EditDriver), request);
        }

        /// <summary>
        /// Upload driving license
        /// </summary>
        /// <remarks>
        /// Using to upload driving license by driver
        /// </remarks>
        [HttpPost("uload-driving-license")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UploadDrivingLicenseCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UploadDrivingLicenseCommandResponse>> UploadDrivingLicense(UploadDrivingLicenseRequest request)
        {
            return await ExecuteUserRequest<UploadDrivingLicenseCommand, UploadDrivingLicenseRequest>(nameof(UploadDrivingLicense), request);
        }

        /// <summary>
        /// Register driver
        /// </summary>
        /// <remarks>
        /// Register driver
        /// </remarks>
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterDriverCommand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegisterDriverCommandResponse>> RegisterDriver(RegisterDriverCommand request)
        {
            return await Execute<RegisterDriverCommand>(nameof(RegisterDriver), request);
        }
    }
}
