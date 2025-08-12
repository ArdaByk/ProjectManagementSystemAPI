# Project Management System API

A comprehensive project management system that simplifies project creation and management for teams. This system enables project managers to efficiently create projects, manage team members, and assign tasks to individuals.

## 🚀 Features

- **Project Creation & Management**: Create and organize projects with ease
- **Team Management**: Add and manage team members
- **Task Assignment**: Create tasks and assign them to specific team members

## 🏗️ Project Structure

The solution is organized into two main components:

### PMS.Core.Packages
A shared library project containing common services and mechanisms that can be utilized across all services and projects within the system.

### ProjectManagementSystem
The main application project that implements the core functionality, built with references to the PMS.Core.Packages shared library.

## 🏛️ Architecture

This project follows **Clean Architecture** principles with the following design patterns and technologies:

- **Mediator Pattern**: Implemented using MediatR library for loose coupling between components
- **Repository Pattern**: Advanced repository classes for data access abstraction
- **Caching Strategy**: Redis integration for improved performance

## 🛠️ Built With

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET Core](https://img.shields.io/badge/.NET_Core-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![MediatR](https://img.shields.io/badge/MediatR-FF6B6B?style=for-the-badge&logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?style=for-the-badge&logo=redis&logoColor=white)
