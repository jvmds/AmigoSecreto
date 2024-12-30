using AmigoSecreto.Dtos;
using FluentValidation;

namespace AmigoSecreto.Validations;

public class GroupInputDtoValidator : AbstractValidator<GroupInputDto>
{
    public GroupInputDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}