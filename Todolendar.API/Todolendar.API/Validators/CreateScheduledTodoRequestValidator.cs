using FluentValidation;
using Todolendar.API.Models.DTO.ScheduledTodo;

namespace Todolendar.API.Validators
{
    public class CreateScheduledTodoRequestValidator : AbstractValidator<CreateScheduledTodoRequest>
    {
        public CreateScheduledTodoRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Colour).NotEmpty();
            RuleFor(x => x.RecurCount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
            RuleFor(x => x.RecurFrequencyType).NotEmpty();
            RuleFor(x => x.RecurEndDate).NotNull();
            RuleFor(x => x.NotifyBeforeTime).GreaterThanOrEqualTo(5);
        }
    }
}