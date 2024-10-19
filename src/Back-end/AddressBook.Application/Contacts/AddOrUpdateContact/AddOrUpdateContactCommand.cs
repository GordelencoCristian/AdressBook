using AddressBook.DataTrasnferObjects.Contacts;
using MediatR;

namespace AddressBook.Application.Contacts.AddOrUpdateContact
{
    public class AddOrUpdateContactCommand(ContactDto data) : IRequest<int>
    {
        public ContactDto Data { get; set; } = data;
    }
}
