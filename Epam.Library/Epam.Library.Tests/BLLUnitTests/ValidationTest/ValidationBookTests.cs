using Epam.Library.BLL.Validations;
using Epam.Library.Entities;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Epam.Library.Tests.BLLUnitTests.ValidationTest
{
    public class ValidationBookTests
    {
        private ValidationBook validator;
        [SetUp]
        public void SetUp()
        {
            validator = new ValidationBook();
        }
        //Empty,Null,Overflow
        [Test]
        public void AllEmptyField_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                Title = "",
                Note = "",
                Authors = new List<AuthorDto>(),
                City = "",
                ISBN = "",
                PublishingHouse = "",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.Authors);
            result.ShouldHaveValidationErrorFor(p => p.City);
            result.ShouldHaveValidationErrorFor(p => p.ISBN);
            result.ShouldHaveValidationErrorFor(p => p.PublishingHouse);

        }
        [Test]
        public void AllNullField_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                Title = null,
                Note = null,
                Authors = null,
                City = null,
                ISBN = null,
                PublishingHouse = null,
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.Authors);
            result.ShouldHaveValidationErrorFor(p => p.City);
            result.ShouldHaveValidationErrorFor(p => p.ISBN);
            result.ShouldHaveValidationErrorFor(p => p.PublishingHouse);
        }
        [Test]
        public void AllFieldWithOneChar_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                Title = "A",
                Note = "A",
                Authors = new List<AuthorDto>(),
                City = "A",
                ISBN = "A",
                PublishingHouse = "A",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.Authors);
            result.ShouldHaveValidationErrorFor(p => p.City);
            result.ShouldHaveValidationErrorFor(p => p.ISBN);
            result.ShouldHaveValidationErrorFor(p => p.PublishingHouse);
        }
        [Test]
        public void AllFieldWithOverflowСharacters_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                Title = new string('A', 301),
                Note = new string('A', 2001),
                Authors = new List<AuthorDto>(),
                City = new string('A', 201),
                ISBN = new string('A', 301),
                PublishingHouse = new string('A', 301),
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.Authors);
            result.ShouldHaveValidationErrorFor(p => p.City);
            result.ShouldHaveValidationErrorFor(p => p.ISBN);
            result.ShouldHaveValidationErrorFor(p => p.PublishingHouse);
        }
        //ReliseDate
        [Test]
        public void DateIsGreaterNowDate_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                ReliseDate = new DateTime(2022, 12, 12)
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.ReliseDate);
        }
        [Test]
        public void DateIsLessLowerBound_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                ReliseDate = new DateTime(1399, 12, 12)
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.ReliseDate);
        }
        [Test]
        public void DateBetweenBorders_ValidCorrectly()
        {
            var book = new BookDto
            {
                ReliseDate = DateTime.Now
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.ReliseDate);
        } 
        //City English Language
        [Test]
        public void Eng_TwoHyphensCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "New--York",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_HyphensAtBeginingCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "-New York",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_HyphensAtEndCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "New York-",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_SpaceAtBeginingCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = " New York",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_SpaceAtBeginingAndAtEndCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = " New York ",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_SpaceAtEndCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "New York ",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_PrepositionWithACapitalLetter_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov Na-Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordsTogether_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "RostovDony",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoHyphensBetweenPreposition_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov-na-dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordWithCapitalLetert_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordSeparatedByHyphen_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov-Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordSeparatedByPreposition_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov na Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_ThreeWordSeparatedByPreposition_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov na na Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordSeparatedByPrepositionAfterSpaceAndCapitalLetter_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov na Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordSeparatedByPrepositioAfterHyphenAndCapitalLetter_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov na-Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Eng_TwoWordSeparatedByPrepositionAfterAndAfterHyphenAfterTwoHyphenCapitalLetter_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Rostov-na-Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        //City Russian Language
        [Test]
        public void Ru_TwoHyphensCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "Нью--Йорк",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_HyphensAtBeginingCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "-Нью Йорк",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_HyphensAtEndCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "Нью Йорк-",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_SpaceAtBeginingCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = " Нью Йорк",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_SpaceAtBeginingAndAtEndCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = " Нью Йорк ",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_SpaceAtEndCity_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "Нью Йорк ",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_PrepositionWithACapitalLetter_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов На-Дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordsTogether_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "РостовДону",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoHyphensBetweenPreposition_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов-на-дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordWithCapitalLetert_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов Дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordSeparatedByHyphen_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов-Дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordSeparatedByPreposition_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов на Дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_ThreeWordSeparatedByPreposition_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов на на Дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordSeparatedByPrepositionAfterSpaceAndCapitalLetter_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов на Дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordSeparatedByPrepositioAfterHyphenAndCapitalLetter_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов на-Дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        [Test]
        public void Ru_TwoWordSeparatedByPrepositionAfterAndAfterHyphenAfterTwoHyphenCapitalLetter_ValidCorrectly()
        {
            var book = new BookDto
            {
                City = "Ростов-на-Дону",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.City);
        }
        //MixLanguage Russian and English
        [Test]
        public void RuEngNameCityMixLanguage_NotValid()
        {
            var book = new BookDto
            {
                City = "Ростов на Dony",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.City);
        }
        //NumberOfPages
        [Test]
        public void NumberLessZero_ValidNotCorrectly()
        {
            var book = new BookDto
            {
                NumberOfPages = -100,
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.NumberOfPages);
        }
        [Test]
        public void NumberGreaterZero_ValidCorrectly() 
        {
            var book = new BookDto
            {
                NumberOfPages = 100,
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.NumberOfPages);
        }
        //ISBN
        [Test]
        public void ISBNLengthGreater18Chars_ValidaNotCorrectly() 
        {
            var book = new BookDto
            {
                ISBN = "ISBN 99999-7777-1-1",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.ISBN);
        }
        [Test]
        public void ISBNLengthLess18Chars_ValidaNotCorrectly()
        {
            var book = new BookDto
            {
                ISBN = "ISBN 950-7777-1-1",
            };

            var result = validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(p => p.ISBN);
        }
        [Test]
        public void ISBNLengthEqual18Chars_ValidaCorrectly()
        {
            var book = new BookDto
            {
                ISBN = "ISBN 9989-7777-1-1",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.ISBN);
        }
        [Test]
        public void ISBNAtTheEndTheSympbolX_ValidaCorrectly()
        {
            var book = new BookDto
            {
                ISBN = "ISBN 9989-7777-1-X",
            };

            var result = validator.TestValidate(book);

            result.ShouldNotHaveValidationErrorFor(p => p.ISBN);
        }
    }
}
