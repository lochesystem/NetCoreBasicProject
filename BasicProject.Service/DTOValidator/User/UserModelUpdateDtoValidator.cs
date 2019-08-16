using BasicProject.Service.DTOs.User;
using FluentValidation;

namespace BasicProject.Service.DTOValidator.User
{
    public class UserModelUpdateDtoValidator : AbstractValidator<UserModelUpdateDto>
    {
        public UserModelUpdateDtoValidator()
        {
        }
    }
}
