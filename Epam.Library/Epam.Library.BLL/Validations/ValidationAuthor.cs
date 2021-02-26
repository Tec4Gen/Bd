using Epam.Library.Entities;
using FluentValidation;
using Epam.Library.BLL.Validations.Extensions;
namespace Epam.Library.BLL.Validations
{
    public class ValidationAuthor: AbstractValidator<AuthorDto>
    {
        public ValidationAuthor()
        {
            string patternForFristNameRuEng = @"^((([А-ЯЁ][а-яё]*)([-][А-ЯЁ][а-яё]*)?)||((^[A-Z][a-z]*)([-][A-Z][a-z]*)?))$";

            string patternForLastNameRuEng = @"^(((([А-ЯЁ](['][А-ЯЁ])?[а-яё]*)(((((([ ][а-яё]{2,4}){1,}[ \-'][А-ЯЁ][а-яё]*)?)?)?))|((([А-ЯЁ][а-яё]*)((([' -][А-ЯЁ][а-яё]*))?)|((((['][А-ЯЁ][а-яё]*)(((([ ][а-яё]{2,4}){1,})|([ ][А-ЯЁ][а-яё]*))[ -][А-ЯЁ][а-яё]*)?)?)?)))))|((([A-Z](['][A-Z])?[a-z]*)(((((([ ][a-z]{2,4}){1,}[ \-'][A-Z][a-z]*)?)?)?))|((([A-Z][a-z]*)((([' -][A-Z][a-z]*))?)|((((['][A-Z][a-z]*)(((([ ][A-Z]{2,4}){1,})|([ ][A-Z][a-z]*))[ -][A-Z][a-z]*)?)?)?))))))$";


            RuleFor(p => p.FirstName)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2,50)
                .Matches(patternForFristNameRuEng).WithMessage("The {PropertyName} can not contain Latin and Russian letters, the first letter is capitalized, after the prefixes there is a capital letter, if there is a hyphen, then the next letter after it is also capital, the same rules for the apostrophe");
         

            RuleFor(p => p.LastName)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2, 200)
                .Matches(patternForLastNameRuEng).WithMessage("The {PropertyName} can not contain Latin and Russian letters, the first letter is capitalized, it can contain a hyphen while the first letter after the hyphen is capitalized.");
        }
    }

}
