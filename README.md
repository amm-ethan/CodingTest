# Readme
This is repo for Coding Test for 2P2C Company.

# Prerequisite
- .Net Core 6
- MSSQL Server

# Design Pattern
This application use Code-First Apporach and EF core 6.
No database query is required and the database will be created automatically on startup.
Onion Architecture + Repository Pattern is used for better dependency flows, better testing and clean and readable folder structure.

# Todo before running
Tester need to change SqlConnection in appsettings.json.
ASPNETCORE_ENVIRONMENT to "Production" in Properties/launchSettings.json. (Optional. This will disable Swagger UI)
Both files are under API Project.

# Controller End Point.
- POST  api/transactions/import
  to import data from csv or xml files.
  
- GET   api/transactions/
  to get transactions. this GET support Pagination,Searching,Flitering and Ordering. (Check Swagger for parameters)
  Date format must be in yyyy-MM-dd
  
- GET   api/transactions/by-currency/{currency}
  to get transactions by currency type.
 
- GET   api/transactions/by-date-range/{fromDate}/{toDate}
  to get transactions by date range. Date format must be in yyyy-MM-dd
  
- GET   api/transactions/by-status/{status}
  to get transactions by date range. This accept both short form and long forms. (Either A or Accepted is accepted)
