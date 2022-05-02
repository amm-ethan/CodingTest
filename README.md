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
Tester need to change SqlConnection in appsettings.json and ASPNETCORE_ENVIRONMENT to "Production" in Properties/launchSettings.json.
Both files are under API Project.
