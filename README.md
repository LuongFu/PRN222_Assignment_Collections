# PRN222 – Assignment 01

## Building a News Management System with ASP.NET Core MVC

---

## 1. Introduction

A News Management System (NMS) is a software application that helps universities and educational institutions efficiently manage, organize, and publish news and content to their website and other communication channels.

The system allows administrators and staff to create, update, approve, schedule, and publish news content. This improves communication with students, lecturers, and the community while increasing engagement and accessibility of information.

In this assignment, we develop a simplified version of the FUNewsManagementSystem, focusing on account and news management features.

---

## 2. Assignment Objectives

This project aims to strengthen knowledge of ASP.NET Core MVC and C# programming through the following:

* Building an ASP.NET Core MVC application using Visual Studio.
* Performing CRUD operations using Entity Framework Core.
* Using LINQ to query, sort, and filter data.
* Applying a 3-layer architecture:

  * Presentation Layer (MVC)
  * Business Logic Layer (Services)
  * Data Access Layer (Repository and DAO)
* Applying design patterns such as:

  * Repository Pattern
  * Singleton Pattern
* Implementing searching, filtering, and validation.
* Running and testing a complete web application.

---

## 3. Technologies Used

* ASP.NET Core MVC (.NET 6, .NET 7, or .NET 8)
* C#
* Entity Framework Core
* LINQ
* SQL Server
* Bootstrap, JavaScript, AJAX
* Visual Studio 2019 or higher

---

## 4. Database Design

The system includes the following relationships:

* A News Article belongs to exactly one Category.
* A Staff Account can create many News Articles.
* A News Article can have multiple Tags.
* A Tag belongs to zero or one News Article.
* Category and News status:

  * Active = 1
  * Inactive = 0.

---

## 5. Roles and Permissions

### Public Users

* Can view active news articles without authentication.

### Lecturer

* Can log in.
* Can view active news articles.

### Staff

* Manage Category information.
* Manage News Articles including tags.
* Manage personal profile.
* View personal news history.
* Cannot delete categories that are associated with any news.

### Admin

* Manage account information.
* Generate statistical reports by date range.
* Sort reports in descending order.

---

## 6. Default Admin Account

The default admin account is stored in the appsettings.json file:

Email: [admin@FUNewsManagementSystem.org](mailto:admin@FUNewsManagementSystem.org)
Password: @@abc123@@

---

## 7. Main Features

### Authentication

* Login using email and password.
* Role-based authorization.

### Account Management

* Create, read, update, delete, and search.
* Validation for all fields.

### Category Management

* CRUD and search.
* Conditional delete.

### News Article Management

* CRUD and search.
* Tag management.
* Popup dialogs for create and update.
* Confirmation before delete.

### Reporting

* Statistics based on date range.
* Sorted results.

### Validation

* DataAnnotations and custom validation.
* Popup form validation.

---

## 8. Architecture

This project applies a 3-layer architecture:

* Presentation Layer (MVC)
* Business Layer (Services)
* Data Access Layer (Repository and DAO)

Controllers do not directly access the database.

---

## 9. Project Structure

StudentName_ClassCode_A01.sln

* StudentNameMVC (ASP.NET Core MVC)
* BusinessObjects
* DataAccess
* Repositories
* Services

---

## 10. How to Run the Project

### Step 1: Clone the repository

```
git clone https://github.com/YourUsername/FUNewsManagementSystem.git
```

### Step 2: Create the database

Run the SQL script:

```
FUNewsManagement.sql
```

This will create the FUNewsManagement database.

### Step 3: Configure the connection string

Update the appsettings.json file:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=FUNewsManagement;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Step 4: Run the project

* Open the solution in Visual Studio.
* Build and run the application.
* The default user interface is the Login page.

---

## 11. Testing

The following functions should be tested:

* Authentication and authorization.
* CRUD operations.
* Searching and filtering.
* Validation.
* Reports.

---

## 12. Notes

* Visual Studio 2019 or higher.
* SQL Server 2012 or higher.
* MVC architecture only.
* No direct database access from controllers.
* Repository and service layers are required.
* Follow clean and maintainable coding practices.

---

## 13. Learning Outcomes

After completing this assignment, students will:

* Understand MVC architecture.
* Apply design patterns in real-world systems.
* Work with EF Core and LINQ.
* Build scalable and maintainable applications.
* Implement authentication and role management.

---

## 14. Contact

Course: PRN222 – C# Programming
Lecturer: QuangLTN3
University: FPT University Da Nang

This project is for academic and learning purposes only.
