# FinShark

A financial stock portfolio management application built with .NET and C#.

## Overview

FinShark is a web API application designed for managing stock portfolios and financial data. The project leverages modern .NET technologies to provide a robust backend solution for stock market operations.

## Project Structure

```
FinShark/
├── api/                    # API project folder
├── .vscode/               # VS Code configuration
└── Fin Shark.sln         # Visual Studio solution file
```

## Technologies

- **C#** - Primary programming language (100%)
- **.NET** - Framework for building the API
- **ASP.NET Core** - Web API framework

## Features

Based on common FinShark implementations, this project likely includes:

- RESTful API endpoints for stock data management
- User portfolio management
- Stock information retrieval and storage
- CRUD operations for financial entities
- Database integration using Entity Framework Core

## Prerequisites

Before running this project, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher recommended)
- [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- SQL Server or SQL Server Express (for database)

## Getting Started

### Installation

1. Clone the repository:
```bash
git clone https://github.com/BaherElgobashi/FinShark.git
cd FinShark
```

2. Navigate to the API project:
```bash
cd api
```

3. Restore dependencies:
```bash
dotnet restore
```

4. Update the connection string in `appsettings.json` with your database configuration.

5. Apply database migrations:
```bash
dotnet ef database update
```

### Running the Application

1. Build the project:
```bash
dotnet build
```

2. Run the application:
```bash
dotnet run
```

The API should now be running on `https://localhost:5001` or `http://localhost:5000`.

## Configuration

Create an `appsettings.json` file in the API project with the following structure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string here"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## API Endpoints

Documentation for API endpoints will be available when running the application. Navigate to `/swagger` to view the interactive API documentation.

## Development

### Using Visual Studio

1. Open `Fin Shark.sln` in Visual Studio
2. Set the API project as the startup project
3. Press F5 to run with debugging

### Using VS Code

1. Open the project folder in VS Code
2. Use the launch configurations in `.vscode` folder
3. Press F5 to start debugging

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is available for use and modification.

## Contact

Project Link: [https://github.com/BaherElgobashi/FinShark](https://github.com/BaherElgobashi/FinShark)

## Acknowledgments

- Built with .NET and ASP.NET Core
- Inspired by modern financial application architectures
