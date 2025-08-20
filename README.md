# Role-Based Access Control (RBAC) System

A  Role-Based Access Control (RBAC) system with authentication, protected routes, and role-based API access.  
Supports Admin, Editor, and Viewer roles with different access levels.

# Backend
- .NET 8 (C#)** – REST API
- Entity Framework Core – ORM for database
- SQL Server  – Database
- JWT Authentication** – Secure login & authorization
- Swagger  – API documentation

- # Features
- Authentication with JWT** (Login / Register)  
- Role-based access** (Admin, Editor, Viewer)  
- Protected API endpoints** in backend  
- Route guards** and conditional UI in frontend  
- Role & User management** (Admin only)  
- API documentation with Swagger

- # Backend Setup
- Navigate to backend folder
   cd rbac-api
- Restore dependencies
  dotnet restore
- Apply migrations (make sure appsettings.json has DB connection string)
  dotnet ef database update
# API Endpoints (Summary)
# Auth
-POST /api/Auth/register → Register new user

-POST /api/Auth/login → Login & get JWT

# Roles
-GET /api/Roles/GetRoles → List roles

-POST /api/Roles/PostRoles → Create role

-PUT /api/Roles → Update role

-DELETE /api/Roles/{id} → Delete role

# Users
-GET /api/User/GetUsers → List users

-POST /api/User/AddUser → Add user

-PUT /api/User → Update user

-DELETE /api/User/{id} → Delete user

# Secure Data
-GET /api/SecureData/admin-only → Admin only

-GET /api/SecureData/editor-access → Editor access

-GET /api/SecureData/all-users → Any authenticated user

Roles & Permissions
Role	Permissions
Admin	Full access, manage users & roles
Editor	Create & edit content, no user/role management
Viewer	Read-only access
