# Lease Bridge - Property Leasing & Maintenance Platform

## Project Overview

Lease Bridge is a web-based Property Leasing and Maintenance Management Platform developed for property management companies to simplify and centralize leasing operations and maintenance services. The system addresses common challenges faced by property managers, including tracking vacant units, managing tenant applications, handling lease agreements, monitoring payments, and organizing maintenance requests efficiently.

## Architecture & Technology Stack

* The system follows a multi-layered architecture consisting of three integrated projects: the MVC Application, the Web API, and a dedicated Reporting Application.


* The platform was developed using ASP.NET Core MVC, ASP.NET Core Web API, Entity Framework Core, SQL Server, and SignalR to support real-time functionality.


* The MVC Application provides the primary user interface.


* The Web API handles business logic and secure data communication.


* The Reporting Application provides operational reports and analytics for Property Managers.


* JWT authentication and role-based authorization are implemented to secure the platform.



## Features by Role

### Tenant

* Browse available units, submit lease applications, track lease information, and create maintenance requests through an intuitive user interface.


* Receive real-time updates and notifications regarding lease applications to stay informed about status changes.



### Property Manager

* Manage properties, buildings, units, lease applications, payments, and maintenance assignments.


* Access a centralized visual overview of real estate operations by tracking real-time portfolio metrics, monthly invoice statuses, occupancy rates, and maintenance request volumes.



### Maintenance Staff

* View assigned maintenance requests and update their progress in real time.


* Receive automated, real-time notifications immediately upon being assigned a new maintenance request.



### Public / Guest

* Check the status and history of maintenance requests without logging in by entering a maintenance ticket number and registered phone number.


* Browse property listings without logging in, and enter an Application ID to view rental application statuses.



## Key Database Design Decisions

* **Hybrid Identity Architecture:** ASP.NET identity tables were separated from business-specific user data by introducing the AppUsers table linked through IdentityUserId.


* **Normalized Financial Workflow:** The financial workflow was redesigned using a normalized architecture where invoices represent financial obligations while payments represent actual transaction records, supporting partial payments and overdue tracking.


* **Soft Availability Tracking:** An IsAvailable flag was implemented inside AppUsers to support maintenance staff assignment workflows without losing historical data.


* **Audit and Tracking:** Entities such as Applications, Payments, MaintenanceRequests, and MaintenanceUpdates include audit fields like CreatedAt, UpdatedAt, and CompletedAt to improve traceability.



## API Endpoints

The Web API handles secure data communication and exposes endpoints for the public lookup page and the Reporting Application.

* **POST** `/api/Auth/login` - Authenticate user and generate JWT token (Anonymous).


* **GET** `/api/Properties` - Retrieve all properties (Anonymous).


* **GET** `/api/Units` - Retrieve available units (Anonymous).


* **POST** `/api/MaintenanceRequests` - Submit a new maintenance request (Authenticated Tenant).


* **GET** `/api/Dashboard/overview` - Retrieve dashboard statistics and reporting data (Authenticated Manager).



## Demo Credentials

| Role | Username / Email | Password |
| --- | --- | --- |
| Manager | manager@test.com | Test123! |
| Staff | staff@test.com | Test123! |
| Tenant | tenant@test.com | Test123! |

These credentials apply to both the local and deployed environments.

## Deployment

* **Web API:** `api-leasebridge-s4g2-88-engaewbzbgghbkac.westeurope-01.azurewebsites.net`

* **Reporting Application:** `reporting-leasebridge-s4g2-88-h0bcanc8a8ewdfcz.westeurope-01.azurewebsites.net`

* **Database (Azure SQL):** `sql-leasebridge-s4g2-21.database.windows.net`


The MVC Application is not currently deployed due to a Visual Studio publish conflict involving duplicate `appsettings.json` files between the MVC and API projects.

## Development Team (Group S4-G2)

* Fatema Abdulla (202304063)


* Fatima Alshabbaq (202203512)


* Emama Mohamed (202301722)


* Maryam Abdulla (202302082)


* Sarah Abdulla (202304907)
