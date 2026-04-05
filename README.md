# 🛒 SuperMarket Management System API

A scalable and maintainable **ASP.NET Core Web API** designed using a **layered (3-tier) architecture**.
This project demonstrates solid backend engineering practices, including clean separation of concerns, abstraction through interfaces, and structured data access patterns.

---

## 📌 Overview

The **SuperMarket Management System API** provides a foundation for managing core supermarket operations such as products and categories.
It is designed with extensibility and maintainability in mind, making it suitable as a base for real-world enterprise applications.

---

## 🧱 Architecture

The solution follows a **3-Tier Architecture** with clear boundaries between layers:

Presentation (API)
    ↓
Business Logic
    ↓
Data Access Layer
    ↓
Domain (Core Models)


### 🔍 Design Principles

* **Separation of Concerns** — each layer has a single responsibility
* **Dependency Inversion** — higher-level modules depend on abstractions
* **Loose Coupling** — achieved via interfaces and dependency injection
* **Scalability** — structure allows easy extension of features

---

## 📁 Solution Structure

SuperMarketManagementSystem/
│
├── SuperMarket.API           # Entry point (controllers, middleware, configuration)
├── SuperMarket.Business      # Business logic (services, interfaces)
├── SuperMarket.Data          # Data access (EF Core, repositories, DbContext)
├── SuperMarket.Domain        # Core domain models (entities)
│
├── SuperMarketManagementSystem.sln
└── README.md


---

## ⚙️ Technology Stack

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* LINQ

---

## 🧩 Core Components

### 🔹 API Layer

* Handles HTTP requests and responses
* Uses DTOs for data transfer
* Delegates processing to business services

### 🔹 Business Layer

* Implements application use cases
* Encapsulates business rules
* Communicates with data layer via abstractions

### 🔹 Data Layer

* Manages persistence using EF Core
* Implements Repository pattern
* Responsible for querying and saving data

### 🔹 Domain Layer

* Contains core entities and domain models
* Free of external dependencies

---

## ✨ Features

* Product management (CRUD operations)
* Category management
* Structured layered architecture
* Repository pattern implementation
* Clean and maintainable codebase


## 🧠 Architectural Notes

* The **Domain layer is independent** and does not depend on any other layer
* The **Business layer depends on abstractions**, not implementations
* The **Data layer encapsulates EF Core** and database concerns
* The **API layer remains thin**, focusing only on request handling

---

## 🚀 Future Enhancements

* Authentication & Authorization (JWT)
* Global Exception Handling
* Logging
* Unit & Integration Testing

---

## 👨‍💻 Author

**Sidali Baloul**



## ⭐ Acknowledgment

This project was built as part of continuous learning and practice in backend development using ASP.NET Core.
