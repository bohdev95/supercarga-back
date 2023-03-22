using MediatR;
using SuperCarga.Application.Domain.Drivers.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.Register
{
    public class RegisterDriverCommandHandler : IRequestHandler<RegisterDriverCommand, RegisterDriverCommandResponse>
    {
        private readonly RegisterDriverCommandValidator validator;
        private readonly IDriversDriversService DriversService;

        public RegisterDriverCommandHandler(RegisterDriverCommandValidator validator, IDriversDriversService DriversService)
        {
            this.validator = validator;
            this.DriversService = DriversService;
        }

        public async Task<RegisterDriverCommandResponse> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new RegisterDriverCommandResponse();

            response.Id = await DriversService.CreateDriver(request, request.VehiculeTypeId);

            return response;
        }
    }
}
