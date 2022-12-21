using FluentValidation;
using FluentValidation.AspNetCore;
using Todolender.API.Models.DTO.ScheduledTodo;
using Todolender.API.Models.DTO.Todo;
using Todolender.API.Validators;

namespace Todolender.API.Validators
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