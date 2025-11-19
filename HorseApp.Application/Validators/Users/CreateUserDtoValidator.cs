using FluentValidation;
using HorseApp.Application.DTOs.Users;

namespace HorseApp.Application.Validators.Users
{
    // Validator för att skapa en användare--körs innan en användare skapas
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            //Username
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Användarnamn är obligatoriskt.")
                .MinimumLength(3).WithMessage("Användarnamn måste innehålla minst 3 tecken.")
                .MaximumLength(30).WithMessage("Användarnamn får inte inehålla mer än 30 tecken.");

            //DisplayName
            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Visningsnamn name is obligatoriskt.")
                .MaximumLength(25).WithMessage("Visningsnamn får inte innehålla mer än 25 tecken.");

            //Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-post är obligatoriskt.")
                .EmailAddress().WithMessage("Ogiltig e-postadress.")
                .MaximumLength(100).WithMessage("E-post får inte innehålla mer än 100 tecken.");

            //Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lösenord är obligatoriskt")
                .MinimumLength(8).WithMessage("Lösenord måste innehålla minst 8 tecken.")
                .MaximumLength(100).WithMessage("Lösenord får inte innehålla mer än 100 tecken.");

            //Age
            RuleFor(x => x.Age)
                .InclusiveBetween(10, 100)
                .WithMessage("Ålder måste vara mellan 10 och 100 år.");

        }
    }
}
