# Agriculture Resource Management Web Application

This web application aims to help small agriculture companies manage their resources. It started as a university project but has been developed to cover the basic needs for managing small agriculture companies.

## Functionalities

- Multitenancy
- User management
- Managing various types of articles:
  - Seeds
  - Fertilizers
  - Preparations
- Management of arable land
- Management of working seasons with the following functionalities:
  - Income
  - Expenses
  - Profit
  - Seeding
  - Performed work
  - Treatment
  - Subsidy

## Architecture

### Frontend

- Built with: **Blazor Webassembly**, **Bootstrap**
- Languages: **C#**, **HTML**, **CSS**

### Backend

- Built with: **ASP.NET 6 Web API**, **Entity Framework 6**, **Microsoft Identity Server**
- Languages: **C#**
- DB: **Microsoft SQL Server** and **SQLite** for development

The Backend follows the Clean Architecture Pattern

- **Domain**: database objects with their business rules
- **Application**: the business logic of the application
- **Infrastructure**: the database connection and external services
- **WebAPI**: handles the HTTP requests

![Architecture Diagram](https://netsharpdev.com/images/posts/shape.png)

## Build and Running the Application

Through the console with the **dotnet** commands or use some IDE like **Visual Studio 2022**

- The start-up project for the **frontend** is **WebUI** The application settings can be configured through the wwwroot/appsettings.json file
- The start-up project for the **backend** is **WebApi**. The application settings can be configured through the appsettings.json file

## Contributing

1. Fork the repository
2. Create a new branch for your feature/fix (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create a new Pull Request

## Roadmap

- Integration with external storage service for managing files 
- Implementing Scheduler
- Implementing CQRS, Mediator and FluentValidation
- Improving the user interface
- Implementing Unit and Integration tests
- Adding support for mobile devices

