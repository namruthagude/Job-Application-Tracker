# Job Application Tracker API

A RESTful API built with ASP.NET Core to help job seekers 
track and manage their job applications efficiently.

## 🚀 Live Demo
[API Documentation](your-railway-url-here/swagger)

## 🛠️ Built With
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger / OpenAPI

## ✨ Features
- Track job applications with status updates
- Filter applications by status or company
- View statistics — response rate, interviews, offers
- Secure JWT authentication
- Full CRUD operations

## 📋 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/jobapplications | Get all applications |
| GET | /api/jobapplications/{id} | Get application by ID |
| GET | /api/jobapplications/stats | Get statistics |
| POST | /api/jobapplications | Add new application |
| PUT | /api/jobapplications/{id}/status | Update status |
| DELETE | /api/jobapplications/{id} | Delete application |

## 📊 Statistics Endpoint
Returns real-time job search analytics:
- Total applications
- Total interviews
- Total offers
- Response rate percentage

## 🔧 Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- SQL Server Express
- Visual Studio 2022

### Installation
1. Clone the repository
   git clone https://github.com/gudenamrutha/Job-Application-Tracker.git

2. Navigate to project
   cd Job-Application-Tracker

3. Update connection string in appsettings.json

4. Run migrations
   Update-Database

5. Run the project
   dotnet run

## 👩‍💻 Author
**Namrutha Gude**
- LinkedIn: linkedin.com/in/namruthagude
- Portfolio: namruthagude.github.io/Portfolio
- Location: Malta 🇲🇹
