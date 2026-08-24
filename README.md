# HR Management System

A modern Human Resources Management System built with **ASP.NET Core** following **Clean Architecture** principles. The project is designed to demonstrate enterprise-level application architecture using **CQRS**, **MediatR**, **Repository Pattern**, **Entity Framework Core**, and **SQL Server**.

## Features

* Employee Management
* Department Management
* Position & Job Title Management
* Attendance & Leave Management
* Payroll Management
* Authentication & Authorization (JWT)
* Role-Based Access Control
* File Upload Support
* Validation & Exception Handling
* Logging with Serilog
* RESTful APIs with Swagger Documentation

## Technologies

* ASP.NET Core 8
* C#
* Entity Framework Core
* SQL Server
* Clean Architecture
* CQRS
* MediatR
* AutoMapper
* FluentValidation
* Serilog
* JWT Authentication
* Swagger / OpenAPI

## Architecture

```text
API
│
├── Core (CQRS + MediatR)
│
├── Service (Business Logic)
│
├── Infrastructure ( Repository + EF Core + DbContext )
│
└── Data (Entities)
```

## Project Goal

The main goal of this project is to apply software engineering best practices and build a scalable, maintainable, and production-ready HR Management System while following Clean Architecture principles.
