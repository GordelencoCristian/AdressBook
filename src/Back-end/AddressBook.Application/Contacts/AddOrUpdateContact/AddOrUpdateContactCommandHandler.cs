using AdressBook.Persistance.Contexts;
using AdressBook.Persistance.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Application.Contacts.AddOrUpdateContact
{
    public class AddOrUpdateContactCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<AddOrUpdateContactCommand, int>
    {
        public async Task<int> Handle(AddOrUpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == request.Data.Id, cancellationToken: cancellationToken);

            if (contact != null)
            {
                mapper.Map(request.Data, contact);
                await context.SaveChangesAsync(cancellationToken);
                return contact.Id;
            }

            var mappedContact = mapper.Map<Contact>(request.Data);

            await context.Contacts.AddAsync(mappedContact, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return mappedContact.Id;
        }
    }
}
