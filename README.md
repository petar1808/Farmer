
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
Here is a working live demo :  https://jolly-dune-075677e03.2.azurestaticapps.net/

Test Users

 - **Username**: system_farmer@mail.bg **Password**: P@ssw0rd **Role**: System Admin
   - This user can manage the tenants and the admins of the tenants
 
 - **Username**: farmer_amin@mail.bg **Password**: P@ssw0rd **Role**: Admin
   - This user can manage the users of a specific tenant and also operate with all agriculture functionalities in the system
   
 - **Username**: farmer_test_user@mail.bg **Password**: P@ssw0rd **Role**: User
   - This user can operate with all agriculture functionalities in the system
 
## Functionalities
  - Multitenancy 
  - User management
  - Managing various types of articles: 
    - Seeds 
    - Fertilizers
    - Preparations
  - Management of arable land
  - Management of working seasons with the following functionalities: 
    - Income, 
    - Expenses 
    - Profit 
    - Seeding 
    - Performed work 
    - Treatment
    - Subsidy

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
    <img src="https://netsharpdev.com/images/posts/shape.png" height="300px">
</p>

## Build Process

Use the dotnet commands: **dotnet build** and **dotnet run**, or use **Visual Studio 2022**

- The start-up project for the **frontend** is **WebUI**. 

- The start-up project for the **backend** is **WebApi**. The application settings can be configured through the appsettings.json file


