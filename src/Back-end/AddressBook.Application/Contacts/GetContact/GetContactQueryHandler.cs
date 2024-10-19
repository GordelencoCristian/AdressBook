using AddressBook.DataTrasnferObjects.Contacts;
using AdressBook.Persistance.Contexts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Application.Contacts.GetContact
{
    public class GetContactQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetContactQuery, ContactDto>
    {
        public async Task<ContactDto> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            var item = await context.Contacts.FirstAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);
            return mapper.Map<ContactDto>(item); ;
        }
    }
}
