APIBook API Documentation
This project is an API built using ASP.NET Core that demonstrates JWT-based authentication and role-based access control for managing users, roles, and books. The project includes various controllers for authentication and resource management.

Requirements
.NET Core SDK
ASP.NET Core 6.0 or later
SQL Server (for database)
Setup
Clone the repository:

bash
Salin kode
git clone https://github.com/your-repository-url.git
cd APIBook
Install required dependencies:

bash
Salin kode
dotnet restore
Update the database connection string in appsettings.json:

json
Salin kode
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

bash
Salin kode
dotnet ef database update
Run the project:

bash
Salin kode
dotnet run
API Endpoints
Authentication
POST /api/Auth Authenticate a user and receive a JWT token.

Request:

json
Salin kode
{
  "email": "user@example.com",
  "password": "yourpassword"
}
Response (200 OK):

json
Salin kode
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
GET /api/Books Retrieve a list of books or a specific book by ID.

Request (all books):

bash
Salin kode
curl -H "Authorization: Bearer jwt_token_here" -X GET "http://localhost:5000/api/Books"
Request (specific book):

bash
Salin kode
curl -H "Authorization: Bearer jwt_token_here" -X GET "http://localhost:5000/api/Books?id=BK1"
Response (200 OK):

json
Salin kode
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

POST /api/Books Create a new book entry.

Request:

json
Salin kode
{
  "book_title": "New Book",
  "author": "Author Name",
  "publisher": "Publisher Name",
  "stock": 10,
  "price": 50000,
  "id_user": "USR-1"
}
Response (200 OK):

json
Salin kode
{
  "id": "BK2",
  "book_title": "New Book",
  "author": "Author Name",
  "publisher": "Publisher Name",
  "stock": 10,
  "price": 50000,
  "id_user": "USR-1"
}
PUT /api/Books/{id} Update a specific book's information.

Request:

json
Salin kode
{
  "book_title": "Updated Title",
  "author": "Updated Author",
  "publisher": "Updated Publisher",
  "stock": 5,
  "price": 75000
}
Response (200 OK):

json
Salin kode
{
  "id": "BK1",
  "book_title": "Updated Title",
  "author": "Updated Author",
  "publisher": "Updated Publisher",
  "stock": 5,
  "price": 75000,
  "id_user": "USR-1"
}
DELETE /api/Books/{id} Delete a book by its ID.

Request:

bash
Salin kode
curl -H "Authorization: Bearer jwt_token_here" -X DELETE "http://localhost:5000/api/Books/BK1"
Response (204 No Content): The book is successfully deleted.

Users Management
GET /api/Users Retrieve a list of users.

Request:

bash
Salin kode
curl -H "Authorization: Bearer jwt_token_here" -X GET "http://localhost:5000/api/Users"
Response (200 OK):

json
Salin kode
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
POST /api/Users Create a new user.

Request:

json
Salin kode
{
  "roles_id": "ROL01",
  "email": "newuser@example.com",
  "password_user": "password",
  "name_user": "New User",
  "birthdate": "1995-05-05"
}
Response (200 OK):

json
Salin kode
{
  "id": "USR-2",
  "roles_id": "ROL01",
  "email": "newuser@example.com",
  "password_user": "password",
  "name_user": "New User",
  "birthdate": "1995-05-05",
  "status_user": "Active"
}
PUT /api/Users/{id} Update a user's status.

Request:

json
Salin kode
{
  "status_user": "Inactive"
}
Response (200 OK):

json
Salin kode
{
  "id": "USR-1",
  "status_user": "Inactive"
}
Role Management
GET /api/Role Retrieve a list of roles.

Request:

bash
Salin kode
curl -H "Authorization: Bearer jwt_token_here" -X GET "http://localhost:5000/api/Role"
Response (200 OK):

json
Salin kode
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

bash
Salin kode
Authorization: Bearer your_jwt_token
Contribution
Fork the repository.

Create a new feature branch:

bash
Salin kode
git checkout -b feature/your-feature-name
Commit your changes:

bash
Salin kode
git commit -m 'Add new feature'
Push to the branch:

bash
Salin kode
git push origin feature/your-feature-name
Open a Pull Request.

License
This project is licensed under the MIT License - see the LICENSE file for details.

Authors
Hilkia Farrel Azaria - Developer and Maintainer
