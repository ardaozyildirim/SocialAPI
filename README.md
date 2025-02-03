# Social API

A .NET 7 Web API project built using Clean Architecture principles, implementing CQRS pattern with MediatR, and using Entity Framework Core for data access.

## Features

- Clean Architecture implementation
- CQRS pattern using MediatR
- Entity Framework Core with SQL Server
- JWT Authentication
- Swagger documentation
- Docker support
- AutoMapper for object mapping
- FluentValidation for request validation
- Generic Repository Pattern
- Unit of Work Pattern

## Prerequisites

- .NET 7 SDK
- Docker Desktop
- SQL Server (if running locally)

## Getting Started

### Running with Docker

1. Clone the repository
2. Navigate to the project root directory
3. Run the following commands:

```bash
docker-compose build
docker-compose up
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: http://localhost:5000/swagger

### Running Locally

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run the following commands:

```bash
dotnet restore
dotnet build
dotnet run --project SocialAPI.API
```

## API Endpoints

### Authentication

- POST /api/auth/register - Register a new user
- POST /api/auth/login - Login and get JWT token

### Users

- GET /api/users/{id} - Get user by ID
- POST /api/users - Create a new user

### Posts

- GET /api/posts - Get all posts
- GET /api/posts/{id} - Get post by ID
- POST /api/posts - Create a new post
- PUT /api/posts/{id} - Update a post
- DELETE /api/posts/{id} - Delete a post

### Comments

- GET /api/posts/{postId}/comments - Get comments for a post
- POST /api/posts/{postId}/comments - Add a comment to a post
- PUT /api/comments/{id} - Update a comment
- DELETE /api/comments/{id} - Delete a comment

## Project Structure

- **SocialAPI.API**: Presentation layer with controllers and configuration
- **SocialAPI.Application**: Business logic, DTOs, CQRS handlers
- **SocialAPI.Domain**: Core entities and interfaces
- **SocialAPI.Infrastructure**: Data access and external services implementation

## Authentication

The API uses JWT Bearer token authentication. To access protected endpoints:

1. Register a new user or login
2. Use the returned JWT token in the Authorization header:
   `Authorization: Bearer {your-token}`

## Development

### Adding Migrations

```bash
dotnet ef migrations add MigrationName --project SocialAPI.Infrastructure --startup-project SocialAPI.API
dotnet ef database update --project SocialAPI.Infrastructure --startup-project SocialAPI.API
```

### Running Tests

```bash
dotnet test
``` 