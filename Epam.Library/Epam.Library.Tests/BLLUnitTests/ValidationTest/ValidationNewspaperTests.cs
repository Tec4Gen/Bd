using Epam.Library.BLL.Validations;
using Epam.Library.Entities;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;

namespace Epam.Library.Tests.BLLUnitTests.ValidationTest
{
    class ValidationNewspaperTests
    {
        private ValidationNewspaper validator;
        [SetUp]
        public void SetUp()
        {
            validator = new ValidationNewspaper();
        }
        //Empty,Null,Overflow
        [Test]
        public void AllEmptyField_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                Title = "",
                Note = "", 
                City = "",
                ISSN = "",
                PublishingHouse = "",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.City);
            result.ShouldHaveValidationErrorFor(p => p.ISSN);
            result.ShouldHaveValidationErrorFor(p => p.PublishingHouse);

        }
        [Test]
        public void AllNullField_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                Title = null,
                Note = null,
                City = null,
                ISSN = null,
                PublishingHouse = null,
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.City);
            result.ShouldHaveValidationErrorFor(p => p.ISSN);
            result.ShouldHaveValidationErrorFor(p => p.PublishingHouse);
        }
        [Test]
        public void AllFieldWithOneChar_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                Title = "A",
                Note = "A", 
                City = "A",
                ISSN = "A",
                PublishingHouse = "A",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.City);
            result.ShouldHaveValidationErrorFor(p => p.ISSN);
            result.ShouldHaveValidationErrorFor(p => p.PublishingHouse);
        }
        [Test]
        public void AllFieldWithOverflowСharacters_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                Title = new string('A', 301),
                Note = new string('A', 2001),
                City = new string('A', 201),
                ISSN = new string('A', 301),
                PublishingHouse = new string('A', 301),
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.City);
            result.ShouldHaveValidationErrorFor(p => p.ISSN);
            result.ShouldHaveValidationErrorFor(p => p.PublishingHouse);
        }
        //ReliseDate
        [Test]
        public void DateIsGreaterNowDate_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                ReliseDate = new DateTime(2022, 12, 12)
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.ReliseDate);
        }
        [Test]
        public void DateIsLessLowerBound_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                ReliseDate = new DateTime(1399, 12, 12)
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.ReliseDate);
        }
        [Test]
        public void DateBetweenBorders_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                ReliseDate = DateTime.Now
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.ReliseDate);
        }
        //City Englist Language
        [Test]
        public void Eng_TwoHyphensCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "New--York",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_HyphensAtBeginingCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "-New York",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_HyphensAtEndCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "New York-",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_SpaceAtBeginingCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = " New York",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_SpaceAtBeginingAndAtEndCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = " New York ",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_SpaceAtEndCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "New York ",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_PrepositionWithACapitalLetter_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Rostov Na-Dony",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordsTogether_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "RostovDony",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoHyphensBetweenPreposition_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Rostov-na-dony",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordWithCapitalLetert_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Rostov Dony",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordSeparatedByHyphen_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Rostov-Dony",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordSeparatedByPrepositionAfterSpaceAfterCapitalLetter_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Rostov na Dony",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordSeparatedByPrepositionAfterHyphenАfterCapitalLetter_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Rostov na-Dony",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        //City Russian Language
        [Test]
        public void Ru_TwoHyphensCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Нью--Йорк",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_HyphensAtBeginingCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "-Нью Йорк",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_HyphensAtEndCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Нью Йорк-",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_SpaceAtBeginingCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = " Нью Йорк",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_SpaceAtBeginingAndAtEndCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = " Нью Йорк ",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_SpaceAtEndCity_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Нью Йорк ",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_PrepositionWithACapitalLetter_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Ростов На-Дону",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordsTogether_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "РостовДону",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoHyphensBetweenPreposition_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Ростов-на-дону",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordWithCapitalLetert_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Ростов Дону",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordSeparatedByHyphen_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Ростов-Дону",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordSeparatedByPrepositionAfterSpaceAfterCapitalLetter_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Ростов на Дону",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordSeparatedByPrepositionAfterHyphenАfterCapitalLetter_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                City = "Ростов на-Дону",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        //MixLanguage Russian and English
        [Test]
        public void RuEngNameCityMixLanguage_NotValid()
        {
            var newspaper = new NewspaperDto
            {
                City = "Ростов на Dony",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.City);
        } 
        //NumberOfPages
        [Test]
        public void NumberLessZero_ValidNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                NumberOfPages = -100,
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.NumberOfPages);
        }
        [Test]
        public void NumberGreaterZero_ValidCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                NumberOfPages = 100,
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.NumberOfPages);
        }
        //ISSN
        [Test]
        public void ISSNLengthGreater13Chars_ValidaNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                ISSN = "ISSN99999-7777",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.ISSN);
        }
        [Test]
        public void ISSNLengthLess13Chars_ValidaNotCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                ISSN = "ISSN950-7777",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldHaveValidationErrorFor(p => p.ISSN);
        }
        [Test]
        public void ISSNLengthEqual13Chars_ValidaCorrectly()
        {
            var newspaper = new NewspaperDto
            {
                ISSN = "ISSN9789-7777",
            };

            var result = validator.TestValidate(newspaper);

            result.ShouldNotHaveValidationErrorFor(p => p.ISSN);
        }
    }
}
