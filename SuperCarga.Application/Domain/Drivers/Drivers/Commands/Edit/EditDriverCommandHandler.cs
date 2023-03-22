using MediatR;
using SuperCarga.Application.Domain.Drivers.Drivers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Drivers.Drivers.Commands.Edit
{
    public class EditDriverCommandHandler : IRequestHandler<EditDriverCommand, EditDriverCommandResponse>
    {
        private readonly EditDriverCommandValidator validator;
        private readonly IDriversDriversService driversService;

        public EditDriverCommandHandler(EditDriverCommandValidator validator, IDriversDriversService driversService)
        {
            this.validator = validator;
            this.driversService = driversService;
        }

        public async Task<EditDriverCommandResponse> Handle(EditDriverCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new EditDriverCommandResponse();

            await driversService.EditDriver(request);

            return response;
        }
    }
}
