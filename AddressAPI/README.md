# Introduction
This project represents a basic address API to manage address information. 

# Requirements
This project uses the .NET 5 Runtime & SDK along with the ASP.NET Core Framework.

# Quickstart
Just clone the project and run the AddressAPI launch configuration. Your browser will be opened and you'll be redirected to the SwaggerUI page to see all the endpoints currently in use.

# API Features
- Pagination: To avoid overfetching and to lessen the load on the database, we use pagination to give consumers the option to specify how much data they'd like to have.
- Filters: Consumers are able to orderBy ASC or DESC.
- CRUD: Consumers are able to 
- Address CRUD operations: addresses may be filtered by using a querystring where the consumer can request by using certain address attributes.
- Location distance calculator: calculates the distance between two cities that are supplied in the querystring. Currently implementations are: afstand.org

# Project Structure
The projects consists of the following directories and structure:
- Controllers: Houses all the relevant controllers.
- Database: Everything related to connecting with the database is found here.
- Entities: Application level POCO's. 
- Services: Various non-statefull classes to perform tasks (such as pagination or calculation).
- Utilities: Extension methods and helper methods.




