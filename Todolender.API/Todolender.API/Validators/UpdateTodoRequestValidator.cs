using FluentValidation;
using Todolender.API.Models.DTO.Todo;

namespace Todolender.API.Validators
{
    public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
    {
        public UpdateTodoRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MinimumLength(4).MaximumLength(25);
        }
    }
}
