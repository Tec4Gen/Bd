using Epam.Library.BLL.Validations;
using Epam.Library.Entities;
using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Library.Tests.BLLUnitTests.ValidationTest
{
    public class ValidationPatentTests
    {
        private ValidationPatent validator;
        [SetUp]
        public void SetUp()
        {
            validator = new ValidationPatent();
        }
        //Empty,Null,Overflow
        [Test]
        public void AllEmptyField_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                Title = "",
                Note = "",
                Authors = new List<AuthorDto>(),
                Country = "",
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.Authors);
            result.ShouldHaveValidationErrorFor(p => p.Country);

        }
        [Test]
        public void AllNullField_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                Title = null,
                Note = null,
                Authors = null,
                Country = null,
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.Authors);
            result.ShouldHaveValidationErrorFor(p => p.Country);
        }
        [Test]
        public void AllFieldWithOneChar_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                Title = "A",
                Note = "A",
                Authors = new List<AuthorDto>(),
                Country = "A",
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.Authors);
            result.ShouldHaveValidationErrorFor(p => p.Country);
        }
        [Test]
        public void AllFieldWithOverflowСharacters_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                Title = new string('A', 301),
                Note = new string('A', 2001),
                Authors = new List<AuthorDto>(),
                Country = new string('A', 201),
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Title);
            result.ShouldHaveValidationErrorFor(p => p.Note);
            result.ShouldHaveValidationErrorFor(p => p.Authors);
            result.ShouldHaveValidationErrorFor(p => p.Country);
        }
        //ReliseDate
        [Test]
        public void ReliseDateIsGreaterNowDateAndSubmissionNull_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                ReliseDate = new DateTime(2022, 12, 12)
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.ReliseDate);
        }
        [Test]
        public void ReliseDateIsLessLowerBoundAndSubmissionNull_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                ReliseDate = new DateTime(1473, 12, 12)
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.ReliseDate);
        }
        [Test]
        public void ReliseDateBetweenBordersAndSubmissionNull_ValidCorrectly()
        {
            var patent = new PatentDto
            {
                ReliseDate = DateTime.Now,
                 SubmissionDate = null
            };

            var result = validator.TestValidate(patent);

            result.ShouldNotHaveValidationErrorFor(p => p.ReliseDate);
        }
        [Test]
        public void ReliseDateAndSubmissionDateEqual_ValidCorrectly()
        {
            var patent = new PatentDto
            {
                ReliseDate = new DateTime(2012,12,12),
                SubmissionDate = new DateTime(2012, 12, 12)
            };

            var result = validator.TestValidate(patent);

            result.ShouldNotHaveValidationErrorFor(p => p.ReliseDate);
        }
        [Test]
        public void ReliseDateEarlierSubmissionDate_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                ReliseDate = new DateTime(2012,12,12),
                SubmissionDate = DateTime.Now
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.ReliseDate);
        }
        //County English Language
        [Test]
        public void Eng_CountryWithLowercase_ValidNotCorrectly() 
        {
            var patent = new PatentDto
            {
                Country = "canada"
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Country);
        }
        [Test]
        public void Eng_CountryWithSpaceAfterAndBefore_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                Country = " Canada "
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Country);

        }
        [Test]
        public void Eng_CountryWithSpaceUpperCase_ValidCorrectly()
        {
            var patent = new PatentDto
            {
                Country = "Canada"
            };

            var result = validator.TestValidate(patent);

            result.ShouldNotHaveValidationErrorFor(p => p.Country);
        }
        [Test]
        public void Eng_CountryAbbreviationWithLowcase_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                Country = "cnd"
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Country);
        }
        [Test]
        public void Eng_CountryAbbreviationWithLowcase_ValidCorrectly()
        {
            var patent = new PatentDto
            {
                Country = "RUS"
            };

            var result = validator.TestValidate(patent);

            result.ShouldNotHaveValidationErrorFor(p => p.Country);
        }
        //County Russian Language
        [Test]
        public void Ru_CountryWithLowercase_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                Country = "канада"
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Country);
        }
        [Test]
        public void Ru_CountryWithSpaceAfterAndBefore_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                Country = " Канада "
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Country);

        }
        [Test]
        public void Ru_CountryWithSpaceUpperCase_ValidCorrectly()
        {
            var patent = new PatentDto
            {
                Country = "Канада"
            };

            var result = validator.TestValidate(patent);

            result.ShouldNotHaveValidationErrorFor(p => p.Country);
        }
        //MixLanguage Russian and English
        [Test]
        public void RuEngCountryMixLanguage_NotValid()
        {
            var patent = new PatentDto
            {
                Country = "Канаda",
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.Country);
        }
        //NumberOfPages
        [Test]
        public void NumberLessZero_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                NumberOfPages = -100,
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.NumberOfPages);
        }
        [Test]
        public void NumberGreaterZero_ValidCorrectly()
        {
            var patent = new PatentDto
            {
                NumberOfPages = 100,    
            };

            var result = validator.TestValidate(patent);

            result.ShouldNotHaveValidationErrorFor(p => p.NumberOfPages);
        }
        //RegistrationNumberOfPages
        [Test]
        public void RegistrationNumberLessZero_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                RegistrationNumber = -100,    
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.RegistrationNumber);
        }
        [Test]
        public void RegistrationNumberGreaterUpperbound_ValidNotCorrectly()
        {
            var patent = new PatentDto
            {
                RegistrationNumber = 1000000000,
            };

            var result = validator.TestValidate(patent);

            result.ShouldHaveValidationErrorFor(p => p.RegistrationNumber);
        }
        [Test]
        public void RegistrationNumberBetweenTheBorders_ValidCorrectly()
        {
            var patent = new PatentDto
            {
                RegistrationNumber = 546456,
            };

            var result = validator.TestValidate(patent);

            result.ShouldNotHaveValidationErrorFor(p => p.RegistrationNumber);
        }

    }
}
