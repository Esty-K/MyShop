# MyShop  


# MyShop API

## Introduction
This project is a .NET 8 WEB API meticulously designed according to REST architectural principles. The API provides endpoints for managing categories, orders, products, and users.

## API Documentation
The API documentation can be accessed via the following Swagger link: https://localhost:7260/swagger/v1/swagger.json

## Project Structure
The project is divided into several layers to ensure a clean architecture:

1. **Entity Layer**: Contains the entity models representing the database tables.
2. **DTO Layer**: Contains Data Transfer Objects (DTOs) used for communication between different layers.
3. **Repository Layer**: Handles database operations.
4. **Service Layer**: Contains business logic.
5. **API Layer**: Contains the controllers that handle HTTP requests.

### Why Layered Architecture?
Layered architecture helps separate concerns, making the application easier to maintain and expand. Each layer is responsible for a specific domain, leading to more organized and modular code.

### DTOs
DTOs (Data Transfer Objects) are used to transfer data between layers. They help decouple the internal data structures from the external contracts of the API, ensuring that changes in internal models do not affect API consumers.

### AutoMapper for Layer Conversion
We use AutoMapper for converting between entities and DTOs. AutoMapper helps map properties from one object to another, reducing the amount of repetitive code and potential for errors.

### Dependency Injection (DI)
The layers communicate with each other using Dependency Injection (DI). DI helps manage dependencies and promotes loose coupling, making the code more testable and maintainable.

## Scalability with Async/Await
The project uses async/await for asynchronous programming to improve scalability. Asynchronous methods help handle more requests concurrently without blocking threads, leading to better performance.

## Database
We use a SQL database with Code First approach. To create the database, you can run the following commands:

## Configuration Files
The project uses configuration files to manage settings. This helps keep the settings separate from the code, making it easier to manage and change configurations without modifying the code.

## Error Handling
All errors are handled by an error handling middleware. Errors are logged using a Logger, and critical errors are sent via email. This ensures that the application can handle errors smoothly and notify developers in case of critical issues.

## Request Logging
Every request to the system is logged for ranking and request analysis purposes. This helps monitor the usage and performance of the API.

## Clean Code
The project follows clean code principles to ensure readability, maintainability, and scalability. This includes adhering to proper naming conventions, modular code, and following SOLID principles.

## Conclusion
This project demonstrates a robust and scalable API designed using best practices. It leverages modern .NET features and follows clean architecture principles to provide a maintainable and efficient solution.

## Contact
38214804460@mby.co.il