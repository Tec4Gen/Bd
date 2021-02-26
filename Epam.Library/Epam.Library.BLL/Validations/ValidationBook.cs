using Epam.Library.Entities;
using FluentValidation;
using Epam.Library.BLL.Validations.Extensions;

namespace Epam.Library.BLL.Validations
{
    public class ValidationBook : AbstractValidator<BookDto>
    {
        public ValidationBook()
        {
            RuleFor(p => p.Title)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2, 300);
            RuleFor(p => p.Note)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2, 2000);
            RuleFor(p => p.PublishingHouse)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2, 300);
            RuleFor(p => p.ReliseDate)
                .DatePublicationValid(1400);
            RuleFor(p => p.NumberOfPages)  
                .NumberValid();
            RuleFor(p => p.Authors)
                .NotEmpty().WithMessage("List authors cannot be empty");

            string patternCityRuEng = @"^((([А-ЯЁ][а-яё]*((((([ ](([а-яё]){2,}|([А-ЯЁ][а-яё]*))){1,}))|(([-]((([а-яё]){2,6})[-][А-ЯЁ][а-яё]*)))|(([ ]((([а-яё]){2,6})[-][А-ЯЁ][а-яё]*([ ](([А-ЯЁ][а-яё]*)|([а-яё]*)))?)))|((([-]((([А-ЯЁ][а-яё]){1,6})[ ][А-ЯЁ][а-яё]*))))|(([ ]([А-ЯЁ][а-яё]*)))|((([-]([А-ЯЁ][а-яё]*)))))?)))|(([A-Z][a-z]*((((([ ](([a-z]){2,}|([A-Z][a-z]*))){1,}))|(([-]((([a-z]){2,6})[-][A-Z][a-z]*)))|(([ ]((([a-z]){2,6})[-][A-Z][a-z]*([ ](([A-Z][a-z]*)|([a-z]*)))?)))|((([-]((([A-Z][a-z]){1,6})[ ][A-Z][a-z]*))))|(([ ]([A-Z][a-z]*)))|((([-]([A-Z][a-z]*)))))?))))$";

            string patternISBN = @"^ISBN (((([0-7])|((8[0-9])|(9[0-4]))|(9([5-8][0-9])|(9[0-3]))|(9{2}(([4-7][0-9])|(8[0-9])))|(9{3}[0-9][0-9]))-([0-9]{1,7})-([0-9]{1,7})-(([0-9])|X)))$";


            RuleFor(p => p.City)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2,200)
                .Matches(patternCityRuEng).WithMessage("The name of the city must begin with a capital letter, may contain a space and a hyphen in this case, the next letter is capital, may contain a paired hyphen in this case, the capital letter comes after the second hyphen");

            RuleFor(p => p.ISBN)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthEqual(18)
                .Matches(patternISBN).WithMessage("International standard book number. It consists of ISBN characters and 10 digits. The ISBN abbreviation and the first digit are separated by a space. The code consists of 4 zones separated by hyphens:");

        }
    }


}
