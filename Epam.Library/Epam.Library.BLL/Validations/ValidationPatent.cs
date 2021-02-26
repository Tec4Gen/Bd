using Epam.Library.Entities;
using FluentValidation;
using Epam.Library.BLL.Validations.Extensions;
using System;

namespace Epam.Library.BLL.Validations
{
    public class ValidationPatent : AbstractValidator<PatentDto>
    {
        public ValidationPatent()
        {
            RuleFor(p => p.Title)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2, 300);
            RuleFor(p => p.Note)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2, 300);
            RuleFor(p => p.Authors).NotEmpty().WithMessage("List authors cannot be empty");

            When(p => p.SubmissionDate != null, () =>
            {
                RuleFor(p => p.SubmissionDate.Value)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .DatePublicationValid(1474);

                RuleFor(p => p.ReliseDate).DatePublicationForPatentValid();
            }).Otherwise(() =>
            {
                RuleFor(p => p.ReliseDate).DatePublicationValid(1474);
            });

            RuleFor(p => p.NumberOfPages).NumberValid();
            RuleFor(p => p.RegistrationNumber)
                .InclusiveBetween(1, 999999999).WithMessage("Invalid patent number");


            string CountryRuEng = @"^(([А-ЯЁ][а-я]*([ ]([А-ЯЁ])?[а-я]{2,})*)|([A-Z][a-z]*([ ]([A-Z])?[a-z]{2,})*)|(([A-Z][A-Z])[A-Z]?))$";

            RuleFor(p => p.Country)
                .Cascade(CascadeMode.Stop)
                .CustomEmpty()
                .LengthInclusiveBetween(2, 200)
                .Matches(CountryRuEng).WithMessage("Must start with a capital letter, can contain a space after the space can be both lowercase and uppercase, can contain a hyphen, after the hyphen is written a capital letter");

        }
    }
}
