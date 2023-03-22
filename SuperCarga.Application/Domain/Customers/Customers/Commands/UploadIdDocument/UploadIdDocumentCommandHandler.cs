using MediatR;
using SuperCarga.Application.Domain.Customers.Customers.Abstraction;
using SuperCarga.Application.Exceptions;

namespace SuperCarga.Application.Domain.Customers.Customers.Commands.UploadIdDocument
{
    public class UploadIdDocumentCommandHandler : IRequestHandler<UploadIdDocumentCommand, UploadIdDocumentCommandResponse>
    {
        private readonly UploadIdDocumentCommandValidator validator;
        private readonly ICustomersCustomersService customersService;

        public UploadIdDocumentCommandHandler(UploadIdDocumentCommandValidator validator, ICustomersCustomersService customersService)
        {
            this.validator = validator;
            this.customersService = customersService;
        }

        public async Task<UploadIdDocumentCommandResponse> Handle(UploadIdDocumentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var response = new UploadIdDocumentCommandResponse();

            response.IdDocumentPath = await customersService.UploadIdDocument(request);

            return response;
        }
    }
}
