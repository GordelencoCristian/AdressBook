using AddressBook.API.Controllers;
using AddressBook.Application.Contacts.AddOrUpdateContact;
using AddressBook.Application.Contacts.DeleteContact;
using AddressBook.Application.Contacts.GetContact;
using AddressBook.Application.Contacts.GetContacts;
using AddressBook.Application.Infrastructure.Pagination.BasedPagedList;
using AddressBook.Application.Infrastructure.Pagination.Model;
using AddressBook.DataTrasnferObjects.Contacts;
using FluentAssertions;
using MediatR;
using Moq;

namespace AddressBook.Tests.Contacts
{
    public class ContactsControllerTests
    {
        private readonly Mock<ISender> _mediatorMock;
        private readonly ContactsController _contactsController;

        public ContactsControllerTests()
        {
            _mediatorMock = new Mock<ISender>();
            _contactsController = new ContactsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAddress_ShouldReturnContactDto_WhenContactExists()
        {
            var contactId = 1;
            var expectedContact = new ContactDto
            {
                Id = contactId,
                FirstName = "Gordelenco",
                LastName = "Cristian",
                PhoneNumber = 123456789,
                Email = "aa@example.com",
                Address = "123 splai independentei"
            };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetContactQuery>(), default))
                .ReturnsAsync(expectedContact);

            var result = await _contactsController.GetAddress(contactId);

            result.Should().BeEquivalentTo(expectedContact);
            _mediatorMock.Verify(x => x.Send(It.IsAny<GetContactQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task GetAddress_ShouldReturnBadRequest_WhenIdIsInvalid()
        {
            int invalidId = -1;

            var result = await _contactsController.GetAddress(invalidId);
            
            result.Should().BeNull();
            _mediatorMock.Verify(x => x.Send(It.IsAny<GetContactQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task DeleteContact_ShouldReturnNoContent_WhenContactIsDeleted()
        {
            var contactId = 22;

            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteContactCommand>(), default))
                .ReturnsAsync(Unit.Value);

            var result = await _contactsController.DeleteContact(contactId);

            result.Should().BeOfType<Unit>();
            _mediatorMock.Verify(x => x.Send(It.IsAny<DeleteContactCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task DeleteContact_ShouldReturnNotFound_WhenContactDoesNotExist()
        {
            var contactId = 999999999;

            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteContactCommand>(), default))
                .ReturnsAsync(Unit.Value);

            var result = await _contactsController.DeleteContact(contactId);

            result.Should().BeOfType<Unit>();
        }

        [Fact]
        public async Task GetContacts_ShouldReturnPaginatedContacts_WhenContactsExist()
        {
            var query = new GetContactsQuery();
            var expectedResult = new PaginatedModel<ContactDto>(
                new PaginatedList<ContactDto>(
                [
                    new() { Id = 1, FirstName = "Dima", LastName = "Ion" },
                    new() { Id = 2, FirstName = "Natalia", LastName = "Morari" }
                ], 2, 2, 2));

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetContactsQuery>(), default))
                .ReturnsAsync(expectedResult);

            var result = await _contactsController.GetArticles(query);

            result.Should().BeEquivalentTo(expectedResult);
            _mediatorMock.Verify(x => x.Send(It.IsAny<GetContactsQuery>(), default), Times.Once);
        }

        [Fact]
        public async Task AddContact_ShouldReturnBadRequest_WhenCommandIsInvalid()
        {
            var command = new AddOrUpdateContactCommand(new ContactDto());
            
            _mediatorMock.Setup(x => x.Send(It.IsAny<AddOrUpdateContactCommand>(), default))
                .ReturnsAsync(0);

            var result = await _contactsController.AddContact(command);

            result.Should().BeGreaterOrEqualTo(0);
        }
    }
}
