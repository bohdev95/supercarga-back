using FluentValidation;
using SuperCarga.Application.Domain.Users.Abstraction;
using SuperCarga.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCarga.Application.Domain.Users.Validation
{
    public class UserAlreadyExistsValidator : AbstractValidator<string>
    {
        public UserAlreadyExistsValidator(IUsersService usersService)
        {
            RuleFor(x => x).Custom((userEmail, ctx) =>
            {
                if (usersService.UserExists(userEmail))
                    ctx.AddFailure("Email", ValidationMessage.AlreadyExist("User"));
            });
        }
    }
}
