using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Domain.VehiculeTypes.Commands.CheckType;
using SuperCarga.Application.Domain.VehiculeTypes.Queries.List;

namespace SuperCarga.Api.Controllers
{
    [ApiController]
    [Route("vehicule-types")]
    [Tags("VehiculeTypes")]
    public class VehiculeTypesController : BaseController
    {
        public VehiculeTypesController(IMediator mediator, IAuthService authService, ILogger<UsersController> logger)
            : base(mediator, authService, logger)
        {
        }

        /// <summary>
        /// Get vehicule types list
        /// </summary>
        /// <remarks>
        /// Returns vehicule types list
        /// </remarks>
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListVehiculeTypesQueryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListVehiculeTypesQueryResponse>> ListVehiculeTypes([FromQuery] ListVehiculeTypesQuery request)
        {
            return await Execute<ListVehiculeTypesQuery>(nameof(ListVehiculeTypes), request);
        }

        /// <summary>
        /// Get vehicule types 
        /// </summary>
        /// <remarks>
        /// Get vehicule type by parameters
        /// </remarks>
        [HttpPost("check")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CheckVehiculeTypeCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckVehiculeTypeCommandResponse>> CheckVehiculeType(CheckVehiculeTypeCommand request)
        {
            return await Execute<CheckVehiculeTypeCommand>(nameof(CheckVehiculeType), request);
        }


    }
}
