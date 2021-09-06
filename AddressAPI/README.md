# Introduction
This project represents a basic address API to manage address information. It's build using Swashbuckle and uses OpenAPI standard to work off of.

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

# Retrospective
There's an issue with pagination where the HATEOAS links generated have an invalid ID (at least, if it's omitted from the request.) I initially wanted to handle this by using DTO's (as it's marked required by the domain model), but I've found it to be overkill. In hindsight, bad idea.

There's also an issue with the ignoring of certain model properties, what seems to be a bug in SwaggerUI itself.
For instance, the Id of the Address model class is marked as ReadOnly, and the generated schema does indicate it's a readonly.
However, the SwaggerUI doc seems to just ignore this. Perhaps a bug on their part...

I'm pretty proud of the pagination feature, I've written it before for my current job. The thing I like the most is that it's not coupled to specific domain and can be reused for nearly everything that requires a HATEOAS constraint.



