using AdressBook.Persistance.Contexts;
using AdressBook.Persistance.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();
var connectionString = configuration.GetConnectionString("Default");

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlServer(connectionString);

using var sc = new AppDbContext(optionsBuilder.Options);

Console.WriteLine("Starting migration");
sc.Database.Migrate();

var dbInit = new DbInitializer(sc);
await dbInit.Seed();
Console.WriteLine("End migration");

public class DbInitializer(AppDbContext context)
{
    public async Task Seed()
    {
        await BulkAddContacts();
        await context.SaveChangesAsync();
    }

    public async Task BulkAddContacts()
    {
        if (await context.Contacts.AnyAsync())
        {
            return;
        }

        var contacts = new List<Contact>();

        for (int i = 0; i < 1_000; i++)
        {
            contacts.Add(new Contact
            {
                FirstName = $"FirstNameExample{i}",
                LastName = $"LastNameExample{i}",
                PhoneNumber = 123123 + i,
                Email = $"email{i}@example.com",
                Address = $"Address example{i}",
                CreateDate = DateTime.Now
            });
        }

        await context.BulkInsertAsync(contacts);
    }
}