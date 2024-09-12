# Product Management API

This is a simple ASP.NET Core Web API for managing products. The API allows you to perform CRUD operations on products. The interface for Edit, Details and Delete products are not implemented yet.

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

These instructions will help you set up and run the project on your local machine for development and testing purposes.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any other database you prefer)

## Installation

1. Clone the repository:
```
    git clone https://github.com/gnaohuv22/Lab01_ASP.NETCoreWebAPI.git
```
2. Navigate to the project directory:
```
    cd Lab01_ASP.NETCoreWebAPI
```

3. Restore the dependencies:
```
    dotnet restore
```

## Setting up the Database

1. Open the solution in Visual Studio:
```
    start Lab01_ASP.NETCoreWebAPI.sln
```

2. Open the `appsettings.json` file and configure your database connection string:
```
    {
    "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=ProductManagementDB;User Id=your_user;Password=your_password;"
        },
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*"
    }
```

3. Open the Package Manager Console in Visual Studio (Tools > NuGet Package Manager > Package Manager Console) and run the following commands to  update the database:
```
    Update-Database
```

## Running the Application

1. Build the solution:
```
    dotnet build
```

2. Navigate to the API project
```
    cd ProductManagementAPI
```
   
3. Run the application:
```
    dotnet run --launch-profile https
```

4. Do the same with the WebClient project with another Terminal window
```
    cd ProductManagementWebClient
    dotnet run --launch-profile https
```

The API will be available at [https://localhost:7254/swagger/index.html](https://localhost:7254/swagger/index.html).
The Web interface will be available at [https://localhost:7102/Product](https://localhost:7102/Product)

## API Endpoints

### Get All Products

- **URL:** [`GET /api/Product`](https://localhost:7254/api/Product)
- **Response:**
```
[
    {
        "productId": 1,
        "productName": "Product 1",
        "categoryId": 1,
        "unitsInStock": 100,
        "unitPrice": 10.0,
        "category": "Category 1"
    },
    ...
]
```
### Get Product by ID

- **URL:** [`GET /api/Product/Details/{id}`](https://localhost:7254/api/Product/Details/1)
- **Response:**
```
{
    "productId": 1,
    "productName": "Product 1",
    "categoryId": 1,
    "unitsInStock": 100,
    "unitPrice": 10.0,
    "category": "Category 1"
}
```

### Create a New Product

- **URL:** [`POST /api/Product/Create`](https://localhost:7254/api/Product/Create)
- **Request Body:**
```
{
    "productId": 0,
    "productName": "Product 1",
    "categoryId": 1,
    "unitsInStock": 100,
    "unitPrice": 10.0
}
```
- **Response:** `204 No Content`

### Update an Existing Product

- **URL:** [`PUT /api/Product/Edit/{id}`](https://localhost:7254/api/Product/Edit/1)
- **Request Body:**
```
{
    "productId": 1,
    "productName": "Product 1",
    "categoryId": 1,
    "unitsInStock": 100,
    "unitPrice": 10.0
}
```

- **Response:** `204 No Content`

### Delete a Product

- **URL:** [`DELETE /api/Product/Delete/{id}`](https://localhost:7254/api/Product/Delete/1)
- **Response:** `204 No Content`

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
    
