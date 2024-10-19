using AddressBook.Application.Infrastructure.ValidationCodes;
using AddressBook.Application.Infrastructure.ValidationMessages;
using AddressBook.Application.Infrastructure.Validators;
using AdressBook.Persistance.Contexts;
using AdressBook.Persistance.Entities;
using FluentValidation;

namespace AddressBook.Application.Contacts.DeleteContact
{
    public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
    {
        public DeleteContactCommandValidator(AppDbContext context)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Contact>(context, ValidationCodes.INVALID_ID, ValidationMessages.NotFound));
        }
    }
}
