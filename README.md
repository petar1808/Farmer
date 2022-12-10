
# Project
<table>
<tr>
<td>
A web application that aims to help for managing the resources of small agriculture companies.
Started as a university project, then was developed to cover the basic needs for managing of small agriculture company.
</td>
</tr>
</table>


## Demo
Here is a working live demo :  https://lemon-meadow-0a1e92e03.2.azurestaticapps.net/

Test Users

 - **Username**: admin@farmer.com **Password**: P@ssw0rd **Role**: Admin
 - **Username**: farmer_user@yahoo.com **Password**: P@ssw0rd **Role**: User
 
## Functionalities
  - Managing various types of articles: 
    - Seeds 
    - Fertilizers
    - Preparations
  - Management of arable land
  - User management
  - Management of working seasons with the following functionalities: 
    - Income, 
    - Expenses 
    - Profit 
    - Seeding 
    - Performed work 
    - Treatment

## Arhitecture

### Frontend
Built with: **Blazor Webassembly**, **Bootstrap**

Languages: **C#**, **HTML**, **CSS**


### Backend
Built with: **ASP.NET 6 Web API**, **Entity Framework 6**, **Microsoft Identity Server**

Languages: **C#**

DB: **Microsoft SQL Server** and **SQLite** for development

The Backed follows the Clean Architecture Pattern

- **Domain** - database objects with their business rules
- **Application** - the business logic of the application
- **Infrastructure** - the database connection and external services
- **WebAPI** - handles the HTTP requests


<p>
    <img src="https://www.dandoescode.com/static/f89816449cce6517b5ae83403212b6ca/99f37/clean-architecture.png" height="300px">
</p>

## Build Process

Use the dotnet commands: **dotnet build** and **dotnet run**, or use **Visual Studio 2022**

- The start-up project for the **frontend** is **WebUI**

- The start-up project for the **backend** is **WebApi**. The application settings can be configured through the appsettings.json file


