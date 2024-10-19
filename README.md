# Address Book Application

## Overview
This application is built with a modern front-end and robust back-end architecture. It features:
- **Frontend**: Angular with Angular Material
- **Backend**: .NET with microservices
- **Middleware**: Mediator and Fluent Validation
- **Database**: MS SQL Server with migrations

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
   - Serve the Angular application.

     ```sh
     npm i
     ng serve
     ```

## Features
- Full CRUD operations for managing contacts.
- Pagination for handling large datasets.
- Responsive design with Angular Material.
- Robust validation with Fluent Validation.

## Technical Stack
- **Frontend**: Angular, Angular Material
- **Backend**: .NET, Microservices
- **Database**: MS SQL Server
- **Middleware**: Mediator, 
