# TrelloBoard

## Overview

TrelloBoard is a Scrum and Kanban-inspired web application built with ASP.NET Core using a layered architecture approach. The application allows teams to create, assign, estimate, and manage User Stories through a visual Kanban board similar to Trello.

The solution demonstrates enterprise development practices including Service Layer, Repository Pattern, Dependency Injection, DTOs, Unit Testing, Strategy Pattern, Observer Pattern, Minimal APIs, Fluent API, and integration with external services.

---

## Features

### User Management

* Create and manage users.
* Assign users to User Stories.

### User Story Management

* Create User Stories.
* Assign a responsible user during creation.
* Automatic estimation generation using configurable strategies.
* Update User Story status.
* View all User Stories on a Kanban board.

### Kanban Board

User Stories are automatically displayed in the corresponding column based on their status:

* Backlog
* To Do
* In Progress
* Done

When a status is updated, the card is automatically moved to the appropriate Kanban column.

### Estimation Alerts

User Stories with an estimation value greater than or equal to 13 points are highlighted with a visual warning to indicate high complexity.

### Notifications and Logging

The application implements the Observer Pattern to react to User Story creation events.

Observers currently perform the following actions:

* Display notifications in the console.
* Generate log entries in a local log.txt file.

This design allows additional observers to be added without modifying existing business logic.

### Pokémon Integration

The application integrates with a Pokémon service to provide additional visual content and demonstrate external API consumption.

---

## Architecture

The solution follows a layered architecture:

MVC
↓
Services
↓
API
↓
Repository
↓
Entity Framework Core
↓
SQL Server

The architecture promotes separation of concerns, maintainability, testability, and scalability.

---

## Design Patterns

### Repository Pattern

Abstracts data access and isolates database operations.

### Service Layer

Contains business logic and DTO mapping.

### Strategy Pattern

Allows different estimation calculation strategies without modifying existing code.

Current implementations include:

* API-based estimation strategy.
* Local random estimation strategy.

The application can switch between strategies without changing the User Story service implementation.

### Observer Pattern

Provides event-driven notifications when a new User Story is created.

Current observers include:

* Console notification observer.
* Log file observer.

When a User Story is created, all subscribed observers are notified automatically.

### Dependency Injection

Used throughout the application to manage dependencies and improve testability.

---

## Technologies

### Backend

* ASP.NET Core MVC
* ASP.NET Core Web API
* ASP.NET Core Minimal APIs
* Entity Framework Core
* Fluent API
* SQL Server

### Frontend

* Razor Views
* Bootstrap 5
* HTML5
* CSS3
* JavaScript

### Testing

* xUnit
* Moq

### API Integration

* HttpClientFactory
* Pokémon API
* Custom Minimal APIs

---

## Solution Structure

AgileBoardTests

* Unit tests using xUnit and Moq.

MinimalAPI.Pokemon

* Random Pokémon service.

TrelloBoard

* ASP.NET Core MVC application.

TrelloBoard.API

* REST API.
* Business logic.
* Repository layer.
* Entity Framework Core.

TrelloBoard.MinimalAPI

* Random estimation service.

---

## Logging

The application automatically generates log entries whenever a new User Story is created.

Logging is implemented through the Observer Pattern, where a dedicated observer writes information to a local log.txt file.

This approach keeps logging concerns separated from business logic while providing traceability for important application events.

---

## Unit Testing

The project includes unit tests focused on the Service Layer using xUnit and Moq.

Covered scenarios include:

* Creating User Stories.
* Retrieving all User Stories.
* Retrieving User Stories by Id.
* Handling non-existing User Stories.
* Repository interaction verification.

The tests isolate business logic from database dependencies by mocking repository interactions.

---

## Future Improvements

* Full User Story editing.
* Drag-and-drop Kanban cards.
* Authentication and Authorization.
* Sprint management.
* Team management.
* Dashboard and metrics.
* CI/CD pipeline integration.
* Real-time notifications using SignalR.

---

## Screenshots

## Kanban Board
![Kanban Board](imagesREADME/Kanban%20board.png)

## Create User Story
![Create User Story](imagesREADME/add%20User%20Story.png)

## Sweet Alert
![Sweet Alert](imagesREADME/sweet%20Alert.png)

## Create User
![Create User](imagesREADME/add%20User.png)

## Change Status
![Change User Story status](imagesREADME/Move%20boton.png)