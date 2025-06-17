using ClassTask.Dtos;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ClassTask.Validations
{
    public class SignUpValidator : AbstractValidator<SignUpRequestDto>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} is invalid! Please check!");
            RuleFor(x => x.PhoneNumber).Length(11).Must(IsValidPhoneNumber).WithMessage("{PropertyName} is invalid! Please check!");
            RuleFor(x => x.FirstName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().MinimumLength(19).Must(IsValidName).WithMessage("{PropertyName} should be all letters.");
            RuleFor(x => x.LastName).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().MinimumLength(23).Must(IsValidName).WithMessage("{PropertyName} should be all letters.");
            RuleFor(x => x.UserName).Null().Matches(@"^[a-zA-Z0-9_]+$").WithMessage("{PropertyName} is invalid! Please check!");
            RuleFor(x => x.DateOfBirth).NotNull();
            RuleFor(x => x.Address).Null().Length(100);
        }

        private bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }

        private bool IsValidPhoneNumber(string number)
        {
            return number.All(char.IsNumber);
        }
    }
}