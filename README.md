# JobScheduler

JobScheduler is a .NET application designed to automate and manage recurring tasks and jobs efficiently. This project provides a clean and modular implementation for scheduling tasks, processing files, and running background jobs using a cron-based system.

---

## Features

- **Job Scheduling**: Implements recurring job execution using a cron scheduler.
- **File Processing**: Includes jobs for handling and processing files efficiently.
- **Environment Configuration**: Supports multiple environments using `appsettings.json`.
- **REST API**: Exposes REST endpoints to trigger or manage jobs via `JobSchedulerController`.
- **CI/CD Integration**: GitHub Actions workflow for automated CI.

---

## Directory Structure

```
JobScheduler/
├── README.md
├── JobScheduler.sln
├── JobScheduler/
│   ├── JobScheduler.csproj
│   ├── JobScheduler.http
│   ├── Program.cs
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── Controllers/
│   │   └── JobSchedulerController.cs
│   ├── Jobs/
│   │   └── FileProcessingJob.cs
│   └── Properties/
│       └── launchSettings.json
└── .github/
    └── workflows/
        └── dotnet.yml
```

---

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A text editor or IDE (e.g., Visual Studio, VS Code)

---

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/shubhambisht541/JobScheduler
   cd JobScheduler
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Configure `appsettings.json` with your environment-specific settings.

---

### Running the Application

1. Start the application locally:
   ```bash
   dotnet run --project JobScheduler/JobScheduler.csproj
   ```

2. Access the REST API endpoints using a tool like [Postman](https://www.postman.com/) or the built-in `JobScheduler.http` file.

---

## Configuration

- **`appsettings.json`**: Contains global configuration settings for the application.
- **`appsettings.Development.json`**: Contains environment-specific overrides.

Sample `appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "CronExpression": "0 */5 * * * *" // Runs every 5 minutes
}
```

---

## Key Components

### Controllers
- **`JobSchedulerController`**: Manages API requests for job operations.

### Jobs
- **`FileProcessingJob`**: Handles file processing tasks as a scheduled job.

---

## CI/CD Workflow

This project includes a GitHub Actions workflow (`.github/workflows/dotnet.yml`) for continuous integration:

- **Build**: Ensures the project builds correctly.

To use this workflow:
1. Push your code to a GitHub repository.
2. Ensure GitHub Actions is enabled in your repository settings.

---

## Contributing

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature-name
   ```
3. Make your changes and commit them.
4. Push the branch and create a pull request.

---

## Authors

- **shubhambisht541** - [GitHub Profile](https://github.com/shubhambisht541)

---
