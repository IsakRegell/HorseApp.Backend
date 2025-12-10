using FluentValidation;
using HorseApp.Application.DTOs.Posts;

namespace HorseApp.Application.Validators.Posts
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Titel är ett krav")
                .MaximumLength(20).WithMessage("Titel får inte ha mer än 20 tecken");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Beskrivning är ett krav")
                .MaximumLength(400).WithMessage("Beskrivning får inte ha mer än 400 tecken");

            RuleFor(x => x.PostType)
                .IsInEnum().WithMessage("Ogiltig posttyp");
        }
    }
}
