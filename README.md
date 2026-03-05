# Appointment Management System

## Overview
This project is a simple Appointment Management System built using **WPF (MVVM)** and **ASP.NET Core Web API**.

The solution follows a **layered architecture** to maintain clear separation of concerns:
- **Domain** – Contains core domain entities and business concepts.
- **Application** – Contains service logic and use cases.
- **Infrastructure** – Handles external concerns such as API communication.
- **API** – Exposes endpoints for appointment operations.
- **WPF Client** – Provides the user interface using the MVVM pattern.

The WPF client communicates with the API using **HttpClient** with **async/await** to avoid blocking the UI thread.

## How to Run

1. Clone the repository.
2. Open the solution in **Visual Studio**.
3. Run the **Web API project**.
4. Verify the API is running via **Swagger**.
5. Set the solution to **Multiple Startup Projects**.
6. Run both the **API** and **WPF Client** projects together.


## Additional Features (With More Time)

- Introduce **DTOs for Create, Update, and Get operations** to hide internal domain fields and control the data exposed by the API.
- Add unit tests for services and API endpoints.
- Improve logging and observability.
- Improve UI/UX for the WPF client.