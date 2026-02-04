# crud-employees-demo
## Description
A CRUD application for managing employee records with a dockerized setup for easy development and deployment.

## Prerequisites
- Docker
- Docker Compose
- Firebase instance (free-tier)

## Getting Started

### Environment Variables
Before running the application, configure your database environment and api variables. See [MySQL.env](Databases/MySQL/README.md) and [WebApi.env](Services/WebApi/README.MD) for required variables.

### Running with Docker Compose
```bash
docker-compose up
```

The application will start with all necessary services (database, db manager, unit tests and api) configured automatically.

### Stopping the Application
```bash
docker-compose down
```

### Development Routes
- **API Swagger Documentation**: http://localhost:5000/swagger/index.html
- **phpMyAdmin**: http://localhost:8080

## Features
- Create, read, update, and delete employee records
- Persistent data storage with Docker volumes
- Isolated development environment
- User authentication through Google Firebase
