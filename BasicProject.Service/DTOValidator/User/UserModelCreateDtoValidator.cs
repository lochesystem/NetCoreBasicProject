using BasicProject.Service.DTOs.User;
using BasicProject.Service.Resources;
using FluentValidation;

namespace BasicProject.Service.DTOValidator.User
{
    public class UserModelCreateDtoValidator : AbstractValidator<UserModelCreateDto>
    {
        public UserModelCreateDtoValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage(UserResource.User_Validate_Name);
            RuleFor(r => r.Email).NotEmpty().WithMessage(UserResource.User_Validate_Email);
            RuleFor(r => r.Password).NotEmpty().WithMessage(UserResource.User_Validate_Password);
        }
    }
}
