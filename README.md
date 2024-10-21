**Project Name: APIBook**

# APIBook API Documentation

This project is an API built using ASP.NET Core that demonstrates JWT-based authentication and role-based access control for managing users, roles, and books. The project includes various controllers for authentication and resource management.

## Requirements

- .NET Core SDK
- ASP.NET Core 6.0 or later
- SQL Server (for database)

## Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/your-repository-url.git
   cd APIBook
Install required dependencies:
dotnet restore
Update the database connection string in appsettings.json:
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionString"
  },
  "JWT": {
    "Key": "YourSecretKey",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience"
  }
}
Apply migrations to set up the database:
dotnet ef database update
Run the project:
dotnet run
API Endpoints
Authentication
1. POST /api/Auth
Authenticate a user and receive a JWT token.

Request:
{
  "email": "user@example.com",
  "password": "yourpassword"
}
Response (200 OK):
{
  "id": "USR-1",
  "roles_id": "ROL01",
  "email": "user@example.com",
  "password_user": "hashed_password",
  "name_user": "User Name",
  "birthdate": "1990-01-01T00:00:00",
  "status_user": "Active",
  "token": "jwt_token_here"
}
If authentication fails, a 400 Bad Request response will be returned.

Books Management (Requires Authorization)
2. GET /api/Books
Retrieve a list of books or a specific book by ID.

Request (all books):
GET /api/Books
Authorization: Bearer jwt_token_here
Request (specific book):

GET /api/Books?id=BK1
Authorization: Bearer jwt_token_here
Response (200 OK):
[
  {
    "id": "BK1",
    "book_title": "Book Title",
    "author": "Author Name",
    "publisher": "Publisher Name",
    "stock": 10,
    "price": 150000,
    "id_user": "USR-1"
  }
]
If the user does not have the required role (ROL02), a 400 Bad Request will be returned.

3. POST /api/Books
Create a new book entry.

Request:
{
  "book_title": "New Book",
  "author": "Author Name",
  "publisher": "Publisher Name",
  "stock": 10,
  "price": 50000,
  "id_user": "USR-1"
}
Response (200 OK):
{
  "id": "BK2",
  "book_title": "New Book",
  "author": "Author Name",
  "publisher": "Publisher Name",
  "stock": 10,
  "price": 50000,
  "id_user": "USR-1"
}
4. PUT /api/Books/{id}
Update a specific book's information.

Request:
{
  "book_title": "Updated Title",
  "author": "Updated Author",
  "publisher": "Updated Publisher",
  "stock": 5,
  "price": 75000
}
Response (200 OK):
{
  "id": "BK1",
  "book_title": "Updated Title",
  "author": "Updated Author",
  "publisher": "Updated Publisher",
  "stock": 5,
  "price": 75000,
  "id_user": "USR-1"
}
5. DELETE /api/Books/{id}
Delete a book by its ID.

Request:
DELETE /api/Books/BK1
Authorization: Bearer jwt_token_here
Response (204 No Content): The book is successfully deleted.

Users Management
6. GET /api/Users
Retrieve a list of users.

Request:
GET /api/Users
Authorization: Bearer jwt_token_here
Response (200 OK):
[
  {
    "id": "USR-1",
    "roles_id": "ROL01",
    "email": "user@example.com",
    "password_user": "hashed_password",
    "name_user": "User Name",
    "birthdate": "1990-01-01T00:00:00",
    "status_user": "Active",
    "roles": {
      "id": "ROL01",
      "title": "Admin"
    }
  }
]
7. POST /api/Users
Create a new user.

Request:
{
  "roles_id": "ROL01",
  "email": "newuser@example.com",
  "password_user": "password",
  "name_user": "New User",
  "birthdate": "1995-05-05"
}
Response (200 OK):
{
  "id": "USR-2",
  "roles_id": "ROL01",
  "email": "newuser@example.com",
  "password_user": "password",
  "name_user": "New User",
  "birthdate": "1995-05-05",
  "status_user": "Active"
}
8. PUT /api/Users/{id}
Update a user's status.

Request:
{
  "status_user": "Inactive"
}
Response (200 OK):

{
  "id": "USR-1",
  "status_user": "Inactive"
}
Role Management
9. GET /api/Role
Retrieve a list of roles.

Request:
GET /api/Role
Authorization: Bearer jwt_token_here
Response (200 OK):

[
  {
    "id": "ROL01",
    "title": "Admin"
  },
  {
    "id": "ROL02",
    "title": "User"
  }
]
Security
The API uses JWT for securing endpoints. Make sure to include the Bearer token in the Authorization header for any request that requires authentication.

Authorization: Bearer your_jwt_token

**Contribution**
Fork the repository.
Create a new feature branch (git checkout -b feature/your-feature-name).
Commit your changes (git commit -m 'Add new feature').
Push to the branch (git push origin feature/your-feature-name).
Open a Pull Request.

**License**
This project is licensed under the MIT License - see the LICENSE file for details.

**Authors**
Hilkia Farrel Azaria - Developer and Maintainer
