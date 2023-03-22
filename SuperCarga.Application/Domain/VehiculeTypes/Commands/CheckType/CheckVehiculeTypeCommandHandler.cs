using MediatR;
using SuperCarga.Application.Domain.VehiculeTypes.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.VehiculeTypes.Commands.CheckType
{
    public class CheckVehiculeTypeCommandHandler : IRequestHandler<CheckVehiculeTypeCommand, CheckVehiculeTypeCommandResponse>
    {
        private readonly CheckVehiculeTypeCommandValidator validator;
        private readonly IVehiculeTypesService vehiculeTypesService;

        public CheckVehiculeTypeCommandHandler(CheckVehiculeTypeCommandValidator validator, IVehiculeTypesService vehiculeTypesService)
        {
            this.validator = validator;
            this.vehiculeTypesService = vehiculeTypesService;
        }

        public async Task<CheckVehiculeTypeCommandResponse> Handle(CheckVehiculeTypeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new CheckVehiculeTypeCommandResponse();

            response.VehiculeType = await vehiculeTypesService.CheckVehiculeType(request);

            return response;
        }
    }
}
