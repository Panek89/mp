using FluentValidation;
using MP.MachinesApi.Models;

namespace Machines.Api.Validators
{
    public class MachineValidator : AbstractValidator<Machine>
    {
        public MachineValidator()
        {
            RuleFor(machine => machine.Manufacturer)
                .NotEmpty()
                .MinimumLength(2);
            
            RuleFor(machine => machine.Model)
                .NotEmpty()
                .MinimumLength(2)
                .NotEqual(machine => machine.Manufacturer);
        }
    }
}