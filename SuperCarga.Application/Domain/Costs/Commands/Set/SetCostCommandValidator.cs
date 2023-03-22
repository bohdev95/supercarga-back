using FluentValidation;
using SuperCarga.Application.Domain.Costs.Model;
using SuperCarga.Application.Validation;

namespace SuperCarga.Application.Domain.Costs.Commands.Set
{
    public class SetCostCommandValidator : AbstractValidator<SetCostCommand>
    {
        public SetCostCommandValidator()
        {
            RuleFor(x => x.Type).Must(x => CostType.Contains(x)).WithMessage(ValidationMessage.NotExist("Type"));
            RuleFor(x => x.Value).GreaterThan(0).WithMessage(ValidationMessage.GreaterThan("Value", 0));
            RuleFor(x => x.FromDate).Must(x => x.Date >= DateTime.Now.Date).WithMessage(ValidationMessage.NotFutureOrToday("FromDate"));
        }
    }
}
