using FluentValidation;

namespace TheNeoGeoArchive.WebApp.ViewModels.Validators
{
    public class GameViewModelValidator : AbstractValidator<GameViewModel>
    {
        public GameViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Genre)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Modes)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(1970)
                .LessThan(2999);
        }
    }
}
