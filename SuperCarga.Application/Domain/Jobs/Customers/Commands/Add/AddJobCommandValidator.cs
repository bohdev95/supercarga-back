using FluentValidation;
using SuperCarga.Application.Domain.Common.Validation;
using SuperCarga.Application.Domain.Location.Validation;
using SuperCarga.Application.Domain.VehiculeTypes.Validation;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Jobs.Customers.Commands.Add
{
    public class AddJobCommandValidator : AbstractValidator<AddJobCommand>
    {
        public AddJobCommandValidator(
            VehiculeTypeExistsValidator vehiculeTypeExistsValidator, 
            AddressDtoValidator adressValidator, 
            CargoDtoValidator cargoValidator,
            ImageDtoValidator imageDtoValidator)
        {
            RuleFor(x => x.Data.Tittle).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Tittle"));
            RuleFor(x => x.Data.Description).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Description"));
            RuleFor(x => x.Data).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Description"));
            RuleFor(x => x.Data.PickupDate).Must(x => x.Date >= DateTime.Now.Date).WithMessage(ValidationMessage.NotFutureOrToday("PickupDate"));
            RuleFor(x => x.Data.DeliveryDate).Must(x => x.Date >= DateTime.Now.Date).WithMessage(ValidationMessage.NotFutureOrToday("DeliveryDate"));
            RuleFor(x => x.Data).Must(x => x.DeliveryDate.Date >= x.PickupDate.Date).WithMessage(ValidationMessage.GreaterThan("DeliveryDate", "PickupDate"));

            RuleFor(c => c.Data.Cargo).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Cargo"));
            RuleFor(c => c.Data.Cargo).SetValidator(cargoValidator);

            RuleFor(c => c.Data.VehiculeTypeId).NotEmpty().WithMessage(ValidationMessage.NotEmpty("VehiculeTypeId"));
            RuleFor(c => c.Data.VehiculeTypeId).SetValidator(vehiculeTypeExistsValidator);

            RuleFor(c => c.Data.Origin).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Origin"));
            RuleFor(c => c.Data.Origin).SetValidator(adressValidator);

            RuleFor(c => c.Data.Destination).NotEmpty().WithMessage(ValidationMessage.NotEmpty("Destination"));
            RuleFor(c => c.Data.Destination).SetValidator(adressValidator);

            RuleFor(c => c.Data.Distance).GreaterThan(0).WithMessage(ValidationMessage.GreaterThan("Distance", 0));
            RuleFor(c => c.Data.PricePerKm).GreaterThan(0).WithMessage(ValidationMessage.GreaterThan("PricePerKm", 0));

            RuleFor(c => c.Data.Cargo.Image).SetValidator(imageDtoValidator).When(image => image != null);
        }
    }
}
