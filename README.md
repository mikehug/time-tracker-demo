# Time Tracker Demo

A comprehensive time tracking application built with .NET 9, Blazor, and MudBlazor. This application allows organizations to manage employee time entries, schedules, and generate reports.

## Features

- **Dashboard**: View current clock-in status, today's schedule, and recent time entries
- **Time Entries**: Log, edit, and manage time entries with filtering capabilities
- **Schedules**: Create and manage employee work schedules
- **Employees**: Manage employee information and profiles
- **Reports**: Generate time-based reports for analysis

## Technology Stack

- **Backend**: .NET 9, Entity Framework Core
- **Frontend**: Blazor Server, MudBlazor UI Component Library
- **Database**: SQL Server

## Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server (or SQL Server Express)

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/mikehug/time-tracker-demo.git
   ```

2. Navigate to the project directory:
   ```
   cd time-tracker-demo/TimeTracker.Web
   ```

3. Run the application:
   ```
   dotnet run
   ```

4. Access the application in your browser:
   - http://localhost:5000
   - https://localhost:5001

## Project Structure

- **TimeTracker.Web**: Blazor Server application with UI components
- **TimeTracker.Data**: Data access layer with Entity Framework Core
  - Models: Domain entities
  - Repositories: Data access logic
  - Migrations: Database schema changes

## Database Setup

The application is configured to automatically create and seed the database on first run in development mode. The connection string can be modified in `appsettings.Development.json`.

## Screenshots

(Screenshots would be added here)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details. 