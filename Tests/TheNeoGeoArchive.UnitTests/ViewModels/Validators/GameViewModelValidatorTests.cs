using FluentValidation.TestHelper;
using TheNeoGeoArchive.WebApp.ViewModels.Validators;
using Xunit;

namespace TheNeoGeoArchive.ViewModels.Validators
{
    public class GameViewModelValidatorTests
    {
        private GameViewModelValidator validator;

        public GameViewModelValidatorTests()
        {
            validator = new GameViewModelValidator();
        }

        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            validator.ShouldHaveValidationErrorFor(game => game.Name, null as string);
        }

        [Fact]
        public void Should_have_error_when_Genre_is_null()
        {
            validator.ShouldHaveValidationErrorFor(game => game.Genre, null as string);
        }

        [Fact]
        public void Should_have_error_when_Modes_is_null()
        {
            validator.ShouldHaveValidationErrorFor(game => game.Modes, null as string);
        }
    }
}
