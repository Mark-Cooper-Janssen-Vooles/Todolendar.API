using FluentValidation;
using Todolendar.API.Models.DTO.PlanReminder;
using Todolendar.API.Models.DTO.Auth;

namespace Todolendar.API.Validators
{
    public class UpdatePlanReminderRequestValidator : AbstractValidator<UpdatedPlanReminderRequest>
    {
        public UpdatePlanReminderRequestValidator()
        {
            RuleFor(x => x.PlanReminderOn).NotNull();
        }
    }
}
