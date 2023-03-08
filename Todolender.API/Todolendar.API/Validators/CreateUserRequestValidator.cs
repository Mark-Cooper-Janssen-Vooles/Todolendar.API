using FluentValidation;
using Todolendar.API.Models.DTO.Auth;

namespace Todolendar.API.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator() 
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PasswordHash).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Mobile).NotEmpty();
            // RuleFor(x => x.CurrentGoal).NotEmpty(); => this can be empty
        }
    }
}
