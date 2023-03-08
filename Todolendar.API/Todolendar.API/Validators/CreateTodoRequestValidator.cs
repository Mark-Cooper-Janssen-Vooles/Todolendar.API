using FluentValidation;
using FluentValidation.AspNetCore;
using Todolendar.API.Models.DTO.Todo;
using Todolendar.API.Models.DTO.ScheduledTodo;
using Todolendar.API.Validators;

namespace Todolendar.API.Validators
{
    public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
    {
        public CreateTodoRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MinimumLength(4).MaximumLength(25);
        }
    }
}