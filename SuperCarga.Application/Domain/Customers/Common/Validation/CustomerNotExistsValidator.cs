using FluentValidation;
using SuperCarga.Application.Domain.Customers.Common.Abstraction;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Customers.Common.Validation
{
    public class CustomerNotExistsValidator : AbstractValidator<Guid>
    {
        public CustomerNotExistsValidator(ICustomersService customersService)
        {
            RuleFor(x => x).Custom((customerId, ctx) =>
            {
                if (!customersService.CustomerExists(customerId))
                    ctx.AddFailure("customerId", ValidationMessage.NotExist("Customer"));
            });
        }
    }
}
