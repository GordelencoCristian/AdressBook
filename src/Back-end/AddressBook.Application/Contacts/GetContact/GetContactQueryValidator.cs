using AddressBook.Application.Infrastructure.ValidationCodes;
using AddressBook.Application.Infrastructure.ValidationMessages;
using AddressBook.Application.Infrastructure.Validators;
using AdressBook.Persistance.Contexts;
using AdressBook.Persistance.Entities;
using FluentValidation;

namespace AddressBook.Application.Contacts.GetContact
{
    public class GetContactQueryValidator : AbstractValidator<GetContactQuery>
    {
        public GetContactQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .SetValidator(new ItemMustExistValidator<Contact>(appDbContext, ValidationCodes.INVALID_ID, ValidationMessages.NotFound));
        }
    }
}
