using FluentValidation;
using Todolender.API.Models.DTO.ScheduledTodo;

namespace Todolender.API.Validators
{
    public class UpdateScheduledTodoRequestValidator : AbstractValidator<UpdateScheduledTodoRequest>
    {
        public UpdateScheduledTodoRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Colour).NotEmpty();
            RuleFor(x => x.RecurCount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.RecurFrequencyType).NotEmpty();
            RuleFor(x => x.RecurEndDate).NotNull();
            RuleFor(x => x.NotifyBeforeTime).GreaterThanOrEqualTo(5);
        }
    }
}