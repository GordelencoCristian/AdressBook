using AdressBook.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdressBook.Persistance.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : TrackableDbContext(options)
    {
        public DbSet<Contact> Contacts { set; get; }
    };
}
