# CV Screening Web Application - .NET 8.0 Backend

## Overview
A comprehensive CV screening web application backend built with .NET 8.0, designed specifically for HR professionals to manage candidate applications, conduct CV screening, schedule interviews, and communicate with candidates.

## Technology Stack
- **Framework**: .NET 8.0 (Latest LTS)
- **API**: ASP.NET Core Web API
- **Database**: Entity Framework Core with SQL Server
- **Authentication**: JWT Bearer tokens
- **Security**: Identity Framework, Data Protection API
- **File Storage**: Local file system with Azure Blob Storage support

## Features

### ✅ Core Features
1. **Secure Authentication & Authorization**
   - JWT token-based authentication
   - Role-based access control (Admin, HR, Manager)
   - Secure password hashing

2. **CV Upload & Management**
   - Secure file upload with validation
   - Support for PDF, DOC, DOCX files
   - File size limits (configurable)
   - Malicious file detection

3. **CV Screening & Analysis**
   - Automated keyword extraction
   - Experience level detection
   - Skills identification
   - Customizable scoring criteria

4. **Candidate Management**
   - Comprehensive candidate profiles
   - Status tracking (pending, reviewed, interviewed, hired, rejected)
   - Rating system (1-5 stars)
   - Comments and feedback system

5. **Interview Scheduling**
   - REST API for interview management
   - Date/time validation
   - Conflict detection

6. **Email Communication**
   - Template-based email system
   - SMTP integration
   - Professional email templates

7. **Analytics & Reporting**
   - REST API endpoints for analytics
   - Application statistics
   - Conversion rates
   - Skills analysis

## Quick Start

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or full version)
- Visual Studio 2022 or later

### Installation Steps

1. **Clone or Extract the Project**
   ```bash
   # Navigate to the CVScreeningAPI directory
   cd CVScreeningAPI
   ```

2. **Install Dependencies**
   ```bash
   dotnet restore
   ```

3. **Update Database Connection**
   - Open `appsettings.json`
   - Update the connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=CVScreening;Trusted_Connection=true;TrustServerCertificate=true;"
   }
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

6. **Access the API**
   - Swagger UI: https://localhost:5001/swagger
   - API Base URL: https://localhost:5001/api

### Visual Studio Instructions
1. Open `CVScreeningAPI.sln` in Visual Studio
2. Press F5 to start debugging
3. The application will open in your browser with Swagger UI

## API Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration

### Candidates
- `GET /api/candidates` - Get all candidates
- `GET /api/candidates/{id}` - Get candidate by ID
- `POST /api/candidates` - Create new candidate
- `POST /api/candidates/upload-cv` - Upload CV file
- `PUT /api/candidates/{id}` - Update candidate
- `PATCH /api/candidates/{id}/status` - Update candidate status
- `PATCH /api/candidates/{id}/rating` - Update candidate rating
- `DELETE /api/candidates/{id}` - Delete candidate

### Comments
- `GET /api/candidates/{id}/comments` - Get candidate comments
- `POST /api/candidates/{id}/comments` - Add comment

### Interviews
- `GET /api/interviews` - Get all interviews
- `POST /api/interviews` - Schedule interview
- `PUT /api/interviews/{id}` - Update interview
- `DELETE /api/interviews/{id}` - Cancel interview

## Database Schema

### Tables Created
- **Candidates**: Main candidate information
- **Comments**: Internal feedback and notes
- **Interviews**: Interview scheduling data
- **Skills**: Master list of skills
- **CandidateSkills**: Many-to-many relationship
- **AspNetUsers**: Identity user management
- **AspNetRoles**: Role management

## Configuration

### Environment Variables
Create a `appsettings.Development.json` file for local development:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CVScreening;Trusted_Connection=True;"
  },
  "Jwt": {
    "Secret": "your-development-secret-key-here"
  },
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUsername": "your-email@gmail.com",
    "SmtpPassword": "your-app-password"
  }
}
```

## Demo Data
The application seeds demo data on first run:
- **Admin User**: admin@company.com / Admin@123
- **HR User**: hr@company.com / SecureHR2024!
- **Sample Candidates**: 3 candidates with different statuses

## Security Features

### Authentication & Authorization
- JWT token-based authentication
- Role-based access control
- Secure password hashing with Identity

### Data Protection
- Input validation and sanitization
- SQL injection prevention via Entity Framework
- XSS protection
- File upload security
- Rate limiting
- CORS configuration

### File Security
- File type validation (PDF, DOC, DOCX only)
- File size limits (configurable)
- Malicious file detection
- Secure file storage

## Testing the API

### Using Swagger UI
1. Start the application
2. Navigate to https://localhost:5001/swagger
3. Use the interactive API documentation

### Using Postman
1. Import the endpoints from Swagger
2. Use the demo credentials for authentication
3. Test all CRUD operations

## Deployment

### Local Development
- Use LocalDB for database
- File storage in local Uploads folder

### Production Deployment
- Update connection string for production SQL Server
- Configure Azure Blob Storage for file uploads
- Set up proper SMTP credentials
- Use environment variables for sensitive data

## Troubleshooting

### Common Issues
1. **Database Connection**: Ensure SQL Server is running
2. **Port Conflicts**: Change port in launchSettings.json
3. **SSL Certificate**: Trust the development certificate
4. **File Uploads**: Ensure Uploads directory exists

### Support
- Check logs in Visual Studio Output window
- Use Swagger UI for API testing
- Review Entity Framework migrations if needed

## License
MIT License - Feel free to use this project for commercial purposes.
