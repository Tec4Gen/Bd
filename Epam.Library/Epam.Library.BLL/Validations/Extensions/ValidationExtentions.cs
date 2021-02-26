using Epam.Library.Entities;
using FluentValidation;
using System;

namespace Epam.Library.BLL.Validations.Extensions
{
    public static class ValidationExtentions
    {
        public static IRuleBuilderOptions<T, string> LengthInclusiveBetween<T>(this IRuleBuilder<T, string> ruleBuilder, int lowerBound, int upperBound)
        {
            return ruleBuilder
                .Must((rootObject, field, context) =>
                {
                  
                    context.MessageFormatter
                      .AppendArgument("MinValue", lowerBound)
                      .AppendArgument("MaxValue", upperBound);

                    return field.Length <= upperBound && field.Length >= lowerBound;
                })
                .WithMessage("{PropertyName} length cannot be less than {MinValue} or more than {MaxValue}");
        }

        public static IRuleBuilderOptions<T, string> LengthEqual<T>(this IRuleBuilder<T, string> ruleBuilder, int value)
        {
            return ruleBuilder
                .Must((rootObject, field, context) =>
                {
                    context.MessageFormatter
                      .AppendArgument("Value", value);

                    return field.Length == value;
                })
                .WithMessage($"{{PropertyName}} the length must not be equal to {value} characters");
        }

        public static IRuleBuilderOptions<T, string> CustomEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("The {PropertyName} cannot be empty");              
        }

        public static IRuleBuilderOptions<T, DateTime> DatePublicationForPatentValid<T>(this IRuleBuilder<T, DateTime> ruleBuilder) where T: PatentDto
        {
            return ruleBuilder.Must((objectRoot, yearPublication) =>
            {
                return yearPublication >= objectRoot.SubmissionDate && yearPublication <= DateTime.Now;

            }).WithMessage("The date must not be more than the current date, and earlier than the application submission date");
        }

        public static IRuleBuilderOptions<T, DateTime> DatePublicationValid<T>(this IRuleBuilder<T, DateTime> ruleBuilder, int year) where T : AbstractPrintedProducts
        {
            return ruleBuilder.Must((yearPublication) =>
            {
                return yearPublication.Year >= year && yearPublication <= DateTime.Now;

            }).WithMessage("The date must not be more than the current date, and earlier than the application submission date");
        }

        public static IRuleBuilderOptions<T, int> NumberValid<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be less than zero.");
        }
    }
}
