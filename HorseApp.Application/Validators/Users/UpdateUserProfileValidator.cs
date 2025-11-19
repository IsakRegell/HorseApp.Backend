using FluentValidation;
using HorseApp.Application.DTOs.Users;

namespace HorseApp.Application.Validators.Users
{
    public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileDto>
    {
        // Validator för att uppdatera en användarprofil--körs innan en användarprofil uppdateras
        public UpdateUserProfileValidator()
        {
            //DisplayName
            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Visningsnamn är obligatoriskt.")
                .MaximumLength(25).WithMessage("Visningsnamn får inte innehålla mer än 25 tecken.");

            //Age
            RuleFor(x => x.Age)
                .InclusiveBetween(10, 100)
                .WithMessage("Ålder måste vara mellan 10 och 100 år.");

            //Bio
            RuleFor(x => x.ProfilePictureUrl)
                .MaximumLength(200)
                .WithMessage("Profilbildens URL får inte innehålla mer än 200 tecken.");

            //ProfilePictureUrl
            RuleFor(x => x.Bio)
                .MaximumLength(350)
                .WithMessage("Bio får inte innehålla mer än 350 tecken.");

            //LocationId

        }
    }
}
