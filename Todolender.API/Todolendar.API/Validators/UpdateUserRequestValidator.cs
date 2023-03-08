using FluentValidation;
using Todolendar.API.Models.DTO.Auth;

namespace Todolendar.API.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator() 
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PasswordHash).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Mobile).NotEmpty();
        }
    }
}
