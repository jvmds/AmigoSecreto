using AmigoSecreto.Dtos;
using FluentValidation;

namespace AmigoSecreto.Validations;

public class UserGroupInputDtoValidator : AbstractValidator<UserGroupInputDto>
{
    public UserGroupInputDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);
        RuleFor(x => x.GroupId)
            .GreaterThan(0);
    }
}