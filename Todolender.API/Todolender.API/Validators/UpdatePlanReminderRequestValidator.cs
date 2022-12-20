using FluentValidation;
using Todolender.API.Models.DTO.Auth;
using Todolender.API.Models.DTO.PlanReminder;

namespace Todolender.API.Validators
{
    public class UpdatePlanReminderRequestValidator : AbstractValidator<UpdatedPlanReminderRequest>
    {
        public UpdatePlanReminderRequestValidator()
        {
            RuleFor(x => x.PlanReminderOn).NotNull();
        }
    }
}
