using AddressBook.DataTrasnferObjects.Contacts;
using AdressBook.Persistance.Entities;
using AutoMapper;

namespace AddressBook.Application.Contacts
{
    public class ContactsMappingProfile : Profile
    {
        public ContactsMappingProfile()
        {
            CreateMap<ContactDto, Contact>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Contact, ContactDto>();
        }
    }
}
