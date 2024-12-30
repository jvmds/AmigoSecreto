using AmigoSecreto.Dtos;
using FluentValidation;

namespace AmigoSecreto.Validations;

public class UserGroupDrawInputDtoValidator : AbstractValidator<UserGroupDrawInputDto>
{
    public UserGroupDrawInputDtoValidator()
    {
        RuleFor(x => x.GroupId)
            .GreaterThan(0);
    }
}