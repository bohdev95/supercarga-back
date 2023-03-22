using FluentValidation;
using SuperCarga.Application.Domain.VehiculeTypes.Abstraction;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.VehiculeTypes.Validation
{
    public class VehiculeTypeExistsValidator : AbstractValidator<Guid>
    {
        public VehiculeTypeExistsValidator(IVehiculeTypesService vehiculeTypesService)
        {
            RuleFor(x => x).Custom((vehiculeTypeId, ctx) =>
            {
                if (!vehiculeTypesService.VehiculeTypeExists(vehiculeTypeId))
                    ctx.AddFailure("vehiculeTypeId", ValidationMessage.NotExist("VehiculeType"));
            });
        }
    }
}
