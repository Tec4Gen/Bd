using Epam.Library.BLL.Validations;
using Epam.Library.Entities;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Epam.Library.Tests.BLLUnitTests.ValidationTest
{
    public class ValidationAuthorTests
    {
        private ValidationAuthor validator;
        [SetUp]
        public void SetUp()
        {
            validator = new ValidationAuthor();
        }
        //FirstName English Language
        [Test]
        public void Eng_SpaceBeforeFirstName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = " Alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_SpaceAfterFirstName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Alex ",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_SpaceAfterAndBeforeFirstName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = " Alex ",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_FirstNameWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_FirstNameBeginingHyphen_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "-Alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_FirstNameEndHyphen_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Alex-",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_FirstNameTwoWordSeparatedHyphenAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Alex-alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_FirstNameTwoWordSeparatedSpaceAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Alex alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_TwoWordSeparatedSpaceAfterUppercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Alex Alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Eng_FirstNameFirstNameWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.FirstName);
        }
      
        [Test]
        public void Eng_TwoWordSeparatedHyphenAfterLowercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Alex-Alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.FirstName);
        }
        //LastName English Language
        [Test]
        public void Eng_SpaceBeforeLastName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = " Ovechkin",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_SpaceAfterLastName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Ovechkin ",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_SpaceAfterAndBeforeLastName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = " Ovechkin ",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "ovechkin",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameBeginingHyphen_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "-Ovechkin",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameEndHyphen_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Ovechkin-",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameTwoWordSeparatedHyphenAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Ovechkin-ovechkin",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameTwoWordSeparatedSpaceAfterWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Ovechkin ovechkin",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameTwoWordSeparatedPrepositionAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Benissio del torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameTwoWordSeparatedPrepositionAfterHyphenAfterWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Benissio del-torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameSeparatedApostropheAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Benissio'del",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameTwoWordSeparatedPrepositionAfterApostropheWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Benissio del'torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameBeginingWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Alex",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameSepararatedSpaceAfterWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Del Torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameSepararatedHypnehAfterWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Del-Torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameSepararatedPrepositionAfterSpaceWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Benissio del Torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameSepararatedPrepositionAfterHyphenWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Benissio del-Torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameSepararatedApostropheAfterWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Del'Torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Eng_LastNameSepararatedPrepositionAfterApostropheAfterWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Benissio del'Torro",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        //FirstName Russian Language
        [Test]
        public void Ru_SpaceBeforeFirstName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = " Алекс",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_SpaceAfterFirstName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Алекс ",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_SpaceAfterAndBeforeFirstName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = " Алекс ",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_FirstNameWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "алекс",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_FirstNameBeginingHyphen_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "-Алекс",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_FirstNameEndHyphen_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Алекс-",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_FirstNameTwoWordSeparatedHyphenAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Алекс-алекс",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_FirstNameTwoWordSeparatedSpaceAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Алекс алекс",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_TwoWordSeparatedSpaceAfterUppercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Алекс Алекс",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_FirstNameFirstNameWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Алекс",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.FirstName);
        }
        [Test]
        public void Ru_TwoWordSeparatedHyphenAfterLowercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                FirstName = "Алекс-Алекс",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.FirstName);
        }
        //LastName English Language
        [Test]
        public void Ru_SpaceBeforeLastName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = " Овечкин",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_SpaceAfterLastName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Овечкин ",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_SpaceAfterAndBeforeLastName_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = " Овечкин ",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "овечкин",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameBeginingHyphen_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "-Овечкин",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameEndHyphen_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Овечкин-",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameTwoWordSeparatedHyphenAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Овечкин-овечкин",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameTwoWordSeparatedSpaceAfterWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Овечкин овечкин",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameTwoWordSeparatedPrepositionAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Бениссио дель торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameTwoWordSeparatedPrepositionAfterHyphenAfterWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Бениссио дель-торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameSeparatedApostropheAfterLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Бениссио'дель",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameTwoWordSeparatedPrepositionAfterApostropheWithLowercase_ValidNotCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Бениссио дель'торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameBeginingWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Дель",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameSepararatedSpaceAfterWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Дель Торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameSepararatedHypnehAfterWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Дель-Торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameSepararatedPrepositionAfterSpaceWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Бениссио дель Торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameSepararatedPrepositionAfterHyphenWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Бениссио дель-Торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameSepararatedApostropheAfterWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Дель'Торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        [Test]
        public void Ru_LastNameSepararatedPrepositionAfterApostropheAfterWithUppercase_ValidCorrect()
        {
            var author = new AuthorDto
            {
                LastName = "Бениссио дель'Торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldNotHaveValidationErrorFor(author => author.LastName);
        }
        //FirstName MixLanguage Russian and English
        [Test]
        public void RuEngFirstNameMixLanguage_NotValid() 
        {
            var author = new AuthorDto
            {
                FirstName = "Benisсио",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
        //Last MixLanguage Russian and English
        [Test]
        public void RuEngLastNameMixLanguage_NotValid()
        {
            var author = new AuthorDto
            {
                FirstName = "Benisсио Торро",
            };
            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(author => author.LastName);
        }
    }
}
