using FluentValidation;
using SuperCarga.Application.Domain.Drivers.Common.Abstraction;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Drivers.Common.Validation
{
    public class DriverNotExistsValidator : AbstractValidator<Guid>
    {
        public DriverNotExistsValidator(IDriversService driversService)
        {
            RuleFor(x => x).Custom((driverId, ctx) =>
            {
                if (!driversService.DriverExists(driverId))
                    ctx.AddFailure("driverId", ValidationMessage.NotExist("Driver"));
            });
        }
    }
}
