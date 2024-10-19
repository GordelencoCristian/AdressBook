using AdressBook.Persistance.TrakingEntity;

namespace AdressBook.Persistance.Entities
{
    public class Contact : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
    }
}
