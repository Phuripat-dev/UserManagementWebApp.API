# 🧠 User Management API (.NET 9)

A backend RESTful API built with **.NET 9 Web API** and **SQL Server**, designed for managing users, roles, and permissions.
![Screenshot 2025-05-17 154830](https://github.com/user-attachments/assets/dac074f8-05cc-4f16-954e-9febbb1460a0)

---

## 🚀 Features

- Create, read, update, delete (CRUD) users
- Assign roles to users (e.g., Admin, Super Admin, Employee)
- Assign detailed permissions (Read, Write, Delete)
- Follows clean architecture principles
- Built using Entity Framework Core
- Supports CORS for frontend integration (Angular, React, etc.)

---

## 🛠️ Tech Stack

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (Express or full version)
- Swagger (for API testing)

---

## 📦 Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- (Optional) [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- (Optional) [Postman](https://www.postman.com/) or Swagger UI for testing

---

## 📁 Project Structure

```plaintext
├── Controllers/
├── DTOs/
├── Models/
├── Repositories/
├── Services/
├── Data/
├── Program.cs
├── appsettings.json
```

---

## ⚙️ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Phuripat-dev/UserManagementWebApp.API.git
cd user-management-api
```

### 2. Configure Database

Update your `appsettings.json` with your local SQL Server connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=UserManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> ℹ️ You may replace `localhost` with your actual SQL Server name.

### 3. Apply Migrations and Create Database

```bash
dotnet ef database update
```

> If you haven’t installed EF CLI, do:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### 4. Run the API

```bash
dotnet run
```

The API will start at:

```http
https://localhost:7001
http://localhost:5205
```

---

## 🔌 API Endpoints

Test via Swagger at:  
📎 `https://localhost:7001/swagger`

### User Endpoints

| Method | Endpoint         | Description          |
|--------|------------------|----------------------|
| GET    | `/api/users`     | Get all users        |
| POST   | `/api/users`     | Create a user        |
| PUT    | `/api/users/{id}`| Update a user        |
| DELETE | `/api/users/{id}`| Delete a user        |

### Roles & Permissions

| Method | Endpoint           | Description              |
|--------|--------------------|--------------------------|
| GET    | `/api/roles`       | List available roles     |
| GET    | `/api/permissions` | List default permissions |

---

## 🧪 Testing with Swagger

1. Run the app with `dotnet run`
2. Open `https://localhost:7001/swagger`
3. Use the Swagger UI to test all endpoints

---

## 🛡️ Security Notes

- Make sure CORS is configured in `Program.cs` if you're using a frontend like Angular or React.
- Add proper authentication and authorization before deploying to production.

---

## 📃 License

This project is licensed under the [MIT License](LICENSE).
