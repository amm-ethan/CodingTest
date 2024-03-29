# Readme
This is repo of Coding Test (API).

# Built With
- .Net Core 6
- MSSQL Server

# Design Pattern
- Code-First Apporach + EF core 6.
- Onion Architecture + Repository Pattern

# Remark
No database query is required and the database will be created automatically on startup.

# Todo before running
- Tester need to change SqlConnection in appsettings.json.
- ASPNETCORE_ENVIRONMENT to "Production" in Properties/launchSettings.json. (Optional. This will disable Swagger UI)
  - Both files are under API Project.

# Controller End Point.
- POST  api/transactions/import
  to import data from csv or xml files.
  
- GET   api/transactions/
  - to get transactions. this GET support pagination, searching, flitering and ordering. (Check Swagger for parameters)
  - date format must be in yyyy-MM-dd
  
- GET   api/transactions/by-currency/{currency}
  - to get transactions by currency type.
 
- GET   api/transactions/by-date-range/{fromDate}/{toDate}
  - to get transactions by date range. 
  - date format must be in yyyy-MM-dd
  
- GET   api/transactions/by-status/{status}
  - to get transactions by date range. This accept both short form and long forms. (Either A or Accepted is accepted)
