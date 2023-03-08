using FluentValidation;
using Todolendar.API.Models.DTO.Todo;

namespace Todolendar.API.Validators
{
    public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
    {
        public UpdateTodoRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(4).MaximumLength(25);
        }
    }
}
