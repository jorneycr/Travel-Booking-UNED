# Travel Booking System

This is a bus travel booking system, developed with ASP.NET Core MVC and Entity Framework Core. The application allows users to register, log in, and manage their travel bookings. It also includes an authentication system based on ASP.NET Identity.

## Features
User Registration and Login: Users must be able to register and authenticate in the system. Input data validation and error handling.

Route Search: Allow users to search for bus routes by origin, destination, and date. Display relevant and filterable search results.

Seat Selection: Users must be able to view the seat layout and select one or more available seats.

Payment Processing: Integrate a simulated payment system (it can be a fictitious payment gateway). Confirm payments and generate a digital receipt or ticket.

Booking Management: Users can view their booking history and details. Implement functionality to cancel bookings within a specified time.

## Project Structure

- **Models**:
Bus Routes: Information about buses, schedules, prices, and destinations.

Users: Data on registered users, purchase history, and personal information.

Bookings: Booking details, including user, route, selected seat, and payment status.

- **Views**:
Home Page

Search Results

Route Details

Booking Form

Booking Confirmation

- **Controllers**:
Search Controller

Booking Controller

Payment Controller

User Controller

## Technologies Used

ASP.NET Core MVC: Main framework of the application.

Entity Framework Core: ORM used to interact with the SQL Server database.

ASP.NET Identity: To manage authentication and user registration.

SQL Server: Relational database to store users, routes, and bookings.

## Installation

1. Clone the repository:

```js
git clone https://github.com/jorneycr/Travel-Booking-UNED
```

2. Restore dependencies

```js
dotnet restore
```

3. SQL SERVER Installation

```js
 Sql Server
```

3. Adjust credentials in the appsettings.json file

```js
     "DefaultConnection": "Server=localhost;Database=TravelBooking;Trusted_Connection=True;TrustServerCertificate=True;"
```

5. Run the Project

```js
dotnet run
```

6. Run the Project with watch

```js
dotnet watch run
```

## Database Migrations using the .NET CLI

1. Create a new migration; if one already exists, go to step 2

```js
dotnet ef migrations add InitialMigration
```

2. Apply the migrations

```js
dotnet ef database update
```
