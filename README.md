# Product Management API — Clean Architecture in ASP.NET Core (.NET 10)

A RESTful Web API for managing products, built to demonstrate **Clean Architecture** with **ASP.NET Core**, **Entity Framework Core** and **SQL Server**.

## Tech Stack

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core 10 (Code First, SQL Server / LocalDB)
- Repository pattern and Service layer
- DTOs to keep entities out of the API surface
- Swagger UI (Swashbuckle)

## Architecture

The solution is organised into layers inside a single project. Dependencies point inward: controllers depend on services, services depend on repository interfaces, and the domain depends on nothing.

## System architecture
<img width="2720" height="2496" alt="product_management_system_architecture" src="https://github.com/user-attachments/assets/1398d3bb-27ca-4aae-a370-8ac8e3236b9f" />

## Folder structure
```
ProductManagement/
├── Domain/
│   └── Entities/                 # Product entity and business rules
├── Application/
│   ├── DTOs/                     # ProductDTO, CreateProductDTO, UpdateProductDTO
│   ├── Interfaces/
│   │   ├── Repositories/         # IProductRepository
│   │   └── Services/             # IProductService
│   └── ProductService/           # Business logic / use cases
├── Infrastructure/
│   ├── Data/                     # ApplicationDbContext (EF Core)
│   └── Repositories/             # ProductRepository
├── InterfaceAdapters/
│   └── Controllers/              # ProductController (HTTP layer)
├── Migrations/                   # EF Core migrations
└── Program.cs                    # Dependency injection and middleware setup
```

## API Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/Product` | Get all products |
| GET | `/api/Product/{id}` | Get a product by id |
| POST | `/api/Product` | Create a product |
| PUT | `/api/Product/{id}` | Update a product |
| DELETE | `/api/Product/{id}` | Delete a product |

## Request flow (GET /api/Product/1)
```mermaid
sequenceDiagram
    participant C as Client
    participant Ctrl as ProductController
    participant S as ProductService
    participant R as ProductRepository
    participant DB as SQL Server
    C->>Ctrl: GET /api/Product/1
    Ctrl->>S: GetProductByIdAsync(1)
    S->>R: GetByIdAsync(1)
    R->>DB: SELECT via EF Core
    DB-->>R: Product row
    R-->>S: Product
    S-->>Ctrl: ProductDTO
    Ctrl-->>C: 200 OK or 404 Not Found
```
## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (installed with Visual Studio) or any SQL Server instance

### Run locally

1. Clone the repository
   ```bash
   git clone https://github.com/Milind300/CleanArchitecture-ProductManagement-API.git
   cd CleanArchitecture-ProductManagement-API/ProductManagement
   ```
2. Check the connection string in `appsettings.json` (defaults to LocalDB)
   ```json
   "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True;"
   ```
3. Create the database
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update
   ```
4. Run the API
   ```bash
   dotnet run
   ```
5. Open Swagger at `https://localhost:<port>/swagger`

## Screenshots

![Swagger UI](docs/<img width="1876" height="691" alt="Swagger" src="https://github.com/user-attachments/assets/8e3cbf12-af5a-4234-9bdf-5ca110a254d9" />
.png)

## Roadmap

- [ ] Global exception handling middleware
- [ ] Return `201 Created` from POST
- [ ] FluentValidation for DTOs
- [ ] Unit tests (xUnit + Moq)
- [ ] Pagination and filtering
- [ ] GitHub Actions CI build

## License

MIT
