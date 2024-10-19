using AddressBook.Application.Contacts.AddOrUpdateContact;
using AddressBook.Application.Contacts.DeleteContact;
using AddressBook.Application.Contacts.GetContact;
using AddressBook.Application.Contacts.GetContacts;
using AddressBook.Application.Infrastructure.Pagination.Model;
using AddressBook.DataTrasnferObjects.Contacts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactsController(ISender sender) : Controller
{
    [HttpGet("{id}")]
    public async Task<ContactDto> GetAddress([FromRoute] int id)
    {
        return await sender.Send(new GetContactQuery(id));
    }

    [HttpGet]
    public async Task<PaginatedModel<ContactDto>> GetArticles([FromQuery] GetContactsQuery query)
    {
        return await sender.Send(query);
    }

    [HttpPost]
    public async Task<int> AddContact([FromBody] AddOrUpdateContactCommand command)
    {
        return await sender.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<Unit> DeleteContact([FromRoute] int id)
    {
        return await sender.Send(new DeleteContactCommand(id));
    }
}