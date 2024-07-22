# Acquisitions with .NET / Angular

## Application
https://nitro-acquisitions.azurewebsites.net

## Acquisitions Collection API
[<img src="https://run.pstmn.io/button.svg" alt="Run In Postman" style="width: 128px; height: 32px;">](https://app.getpostman.com/run-collection/17436300-d094a0a4-886b-4930-9aa3-99e4cff5fa0c?action=collection%2Ffork&source=rip_markdown&collection-url=entityId%3D17436300-d094a0a4-886b-4930-9aa3-99e4cff5fa0c%26entityType%3Dcollection%26workspaceId%3D8dceb780-7c6a-44a3-bba7-90c59549f93b)

## Swagger
https://nitro-acquisitions-api.azurewebsites.net/swagger/index.html

# The main architectural patterns and styles that guide this solution are

- CQRS (Command Query Responsibility Segregation)

# Technical specifications:

- Ready to containerize with Docker.
- Entity Framework Core 6
- Generic Repository (very useful with aggregate management)
- MediaTR : register command handlers and queries automatically (via reflection does scan of the assembly)
- Global Exception Handler
- Logs : Console
- Swagger

# Build & Run

## Visual Studio 2022

To run the project open the solution in visual studio, check the database connection string and run.

## Docker and Docker Compose

To startup the whole solution, execute the following command:

```
docker-compose build --no-cache
docker-compose up -d
```

Then the following containers should be running on `docker ps`:

| Application      | URL                                                                                |
| ---------------- | ---------------------------------------------------------------------------------- |
| Adres API        | https://localhost:8000                                                             |
| Adres APP        | https://localhost:8001                                                             |
| SQL Server       | Server=localhost;User Id=sa;Password=<YourStrong!Passw0rd>;Database=AdresDB;       |


Browse to [http://localhost:8000/swagger/index.html](http://localhost:8000/swagger/index.html) and view the swagger documentation

Browse to [http://localhost:8001](http://localhost:8001) and view the app
