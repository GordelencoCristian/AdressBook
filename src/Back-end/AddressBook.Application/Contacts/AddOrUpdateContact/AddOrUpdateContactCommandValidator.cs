using AddressBook.Application.Infrastructure.ValidationCodes;
using AddressBook.Application.Infrastructure.ValidationMessages;
using AdressBook.Persistance.Contexts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Application.Contacts.AddOrUpdateContact
{
    public class AddOrUpdateContactCommandValidator : AbstractValidator<AddOrUpdateContactCommand>
    {
        public AddOrUpdateContactCommandValidator(AppDbContext context)
        {
            RuleFor(r => r.Data.FirstName)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_NAME);

            RuleFor(r => r.Data.LastName)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_NAME);

            RuleFor(r => r.Data.Address)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            RuleFor(r => r.Data.Email)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT)
                    .WithMessage(ValidationMessages.InvalidReference)
                .EmailAddress()
                    .WithErrorCode(ValidationCodes.INVALID_EMAIL_FORMAT)
                        .WithMessage(ValidationMessages.InvalidEmail)
                .MustAsync(async (email, cancellation) =>
                    !await context.Contacts.AnyAsync(c => c.Email == email, cancellation))
                .WithErrorCode(ValidationCodes.DUPLICATE_EMAIL)
                    .WithMessage(ValidationMessages.InvalidEmail);

            RuleFor(r => r.Data.PhoneNumber)
                .GreaterThan(0)
                .WithErrorCode(ValidationCodes.INVALID_PHONE_NUMBER)
                .WithMessage(ValidationMessages.InvalidPhone);
        }
    }
}
