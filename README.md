# 🌾 Agriculture Resource Management Web Application

This web application is designed to assist small-scale grain producers and agricultural companies in managing their resources efficiently. Initially developed as a university project, the application has evolved to address the foundational needs of managing operations in small-scale agriculture.

## 🚀 Features

- **Catalog Management**
  - **Seed Catalog** for managing seed information.
  - **Fertilizers Catalog** to track various fertilizers.
  - **Crop Protection Products** for pesticide and herbicide management.
  - **Arable Land Records** to manage land parcels and their details.
  - **Season Management** to organize agricultural seasons, timelines, and tasks.

- **Financial Management**
  - **Expense Tracking** for recording and categorizing operational costs.
  - **Subsidy Management** to handle and track agricultural subsidies.

- **Activity Tracking**
  - **Harvesting** management for crop yield and harvest data.
  - **Fieldwork** tracking for soil preparation, planting, and maintenance.
  - **Crop Treatment** tracking for pest control, fertilization, and crop health.

- **Reports** 
  - **Financial Overview** to comprehensively view income, expenses, and profits.
  - **Timeline Comparison** for seasonal comparisons and historical data analysis.

- **User and Organization Management**
  - **User Management** is used to add and manage system users.
  - **Organization and User Management** to support multi-organizational setups with user roles.
## 🏛️ Architecture

### Frontend
- **Built with**: **Blazor WebAssembly** and **Bootstrap**.
- **State Management**: Utilizes **Fluxor** for state management.
- **UI Components**: Incorporates interactive components from **Radzen Blazor**.

### Backend
- **Technologies:** ASP.NET 6 Web API, Entity Framework Core 6, Microsoft Identity
- **Languages:** C#
- **Database:** Microsoft SQL Server for production; SQLite for development
- **Architecture Pattern:** Clean Architecture

The application follows **Clean Architecture** principles, organized into distinct layers to separate concerns and improve scalability.

- **Domain Layer**: Contains core entities and business rules specific to grain production and resource management.

- **Application Layer**: Manages the business logic, using **CQRS** to separate reads and writes, and **MediatR** for a decoupled command and query flow. **FluentValidation** handles validation to ensure consistent data input.

- **Infrastructure Layer**: Provides database access (SQL Server, SQLite for development) and integrates external services like **Microsoft Identity Server** for authentication.

- **WebAPI Layer**: Exposes RESTful endpoints to connect the frontend and backend, with **Swagger** for easy API documentation.

## Build and Run Instructions

1. Use the `.NET CLI` in the command line or an IDE such as Visual Studio 2022.

2. **Frontend (WebUI):**
   - Set `WebUI` as the startup project.
   - Configure application settings in `wwwroot/appsettings.json`.

3. **Backend (WebApi):**
   - Set `WebApi` as the startup project.
   - Configure backend settings in `appsettings.json`.

## Contributing

1. Fork the repository
2. Create a new branch for your feature/fix (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create a new Pull Request

## Roadmap

- Developing unit and integration tests
- Integration with external storage for file management
- Scheduling functionality for automated tasks
- Expanding support to mobile devices
