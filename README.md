# CQRS Architect (Under Development)
CQRS Architect is a comprehensive library designed to simplify the implementation of Command Query Responsibility Segregation (CQRS) in .NET applications. This project provides tools and templates to streamline the setup process, allowing developers to focus on business logic rather than infrastructure concerns.

## Overview

CQRS (Command Query Responsibility Segregation) is a pattern that separates read and write operations into different models. This separation helps improve performance, scalability, and security by allowing the read and write workloads to scale independently. CQRS Architect aims to reduce boilerplate code and make it easier to implement CQRS in your .NET projects.

## Features

- **Command and Query Templates:** Predefined templates for creating commands and queries.
- **Base Handlers:** Abstract base classes for command and query handlers to enforce a consistent structure.
- **Automatic Dependency Injection:** Auto-registering commands, queries, and their handlers with the IoC container.
- **Event Sourcing Support:** Optional support for event sourcing to track changes over time.
- **Transaction Management:** Built-in support for handling transactions in a CQRS context.
- **Documentation and Samples:** Comprehensive documentation and sample projects to get started quickly.
- **Middleware Integration:** Easy integration with common middleware like logging, validation, and caching.

## Getting Started

### Prerequisites

- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Visual Studio 2019](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Installing

1. Clone the repository

   ```bash
   git clone https://github.com/oneananda/CQRSArchitect.git
   ```
2. Running the Application

Build and run the application

dotnet run --project CQRS-Architect.Api

The API will be available at https://localhost:5001 or http://localhost:5000
