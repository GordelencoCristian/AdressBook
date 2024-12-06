# Address Book Application

## Overview
This application is built with a modern front-end and robust back-end architecture. It features:
- **Frontend**: Angular with Angular Material
- **Backend**: .NET with microservices
- **Middleware**: Mediator and Fluent Validation
- **Database**: MS SQL Server with migrations
- **Swagger**: Integrated for API documentation
- **Authentication**: Implemented using JWT with Bearer tokens

---

## Getting Started

### Prerequisites
- Angular CLI
- .NET SDK
- MS SQL Server

### Setup Instructions

1. **Database Initialization**
   - Navigate to the `Migrator` directory.
   - Run the migrator to initialize the database, apply migrations, and seed data.
     ```sh
     dotnet run
     ```

2. **Run the Backend**
   - Navigate to the `AddressBook.API` solution directory.
   - Start the API server.
     ```sh
     dotnet run
     ```

3. **Run the Frontend**
   - Navigate to the frontend `AddressBook` directory.
   - Install dependencies and serve the Angular application.
     ```sh
     npm i
     ng serve
     ```

---

## Features

### Backend Features
- **CRUD Operations** for managing contacts, implemented in the `ContactsController`:
  - `GET` contact by ID or list of contacts with pagination.
  - `POST` for adding new contacts.
  - `DELETE` for removing a contact.
  - Example controller snippet:
    ```csharp
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController(ISender sender) : Controller
    {
        [HttpGet("{id}")]
        public async Task<ContactDto> GetContact([FromRoute] int id)
        {
            return await sender.Send(new GetContactQuery(id));
        }

        [HttpGet]
        public async Task<PaginatedModel<ContactDto>> GetContacts([FromQuery] GetContactsQuery query)
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
    ```

- **Swagger Integration**:
  - Easily explore and test APIs via Swagger UI.
  - JWT Bearer token authentication added for secure testing.
  - Example Swagger setup:
    ```csharp
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "AddressBook API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });
    ```

- **CORS Configuration**:
  - Supports cross-origin requests, essential for communication with the Angular frontend.
    ```csharp
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
    ```

---

### Frontend Features
- **CRUD Functionalities**:
  - Full support for creating, updating, and deleting contacts.
  - Integrated with Angular services to communicate with the backend.
  - Table-based view for displaying contacts using Angular Material.

- **Example Table in Angular**:
  ```html
  <table mat-table [dataSource]="dataSource" class="mat-elevation-z8">
      <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef> Name </th>
          <td mat-cell *matCellDef="let contact"> {{contact.name}} </td>
      </ng-container>

      <ng-container matColumnDef="email">
          <th mat-header-cell *matHeaderCellDef> Email </th>
          <td mat-cell *matCellDef="let contact"> {{contact.email}} </td>
      </ng-container>

      <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef> Actions </th>
          <td mat-cell *matCellDef="let contact">
              <button mat-button (click)="editContact(contact)">Edit</button>
              <button mat-button color="warn" (click)="deleteContact(contact.id)">Delete</button>
          </td>
      </ng-container>

      <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
      <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
  </table>
