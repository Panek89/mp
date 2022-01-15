using FluentValidation;
using MP.MachinesApi.Models;

namespace Machines.Api.Validators
{
    public class ParameterValidator : AbstractValidator<Parameter>
    {
        public ParameterValidator()
        {
            RuleFor(parameter => parameter.MinValue)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(parameter => parameter.MaxValue);
            
            RuleFor(parameter => parameter.MaxValue)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(parameter => parameter.Key)
                .NotEmpty()
                .MinimumLength(2);
        }
    }
}