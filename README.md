## Prerequisites

Before running the application, ensure you have the following installed:
- Node.js (recommended LTS version)
- Yarn package manager
- .NET Core SDK (.NET 8)

## Setting Up the API
1. Navigate to the `api` directory in your terminal.
2. Change connection string in appsettings.Development.json.
3. Run the following command to apply migrations and update the database: dotnet ef database update

## Setting Up the Client
1. Navigate to the `client` directory in your terminal.
2. Run the following command to install dependencies: yarn install

## Running the Application
1. Start the .NET Core API.
2. Start the React client application: yarn start.

## Application overview
Once both the API and client are running, open your web browser and go to the appropriate URL to access the application.
The application initializes by seeding the database with 3 tasks for each user fetched from an external API. User data is also stored in the database. Upon restarting the application, the database resets, allowing the restoration to its initial state by simply restarting the application.
Users log into the application by selecting one of three predefined users. Once logged in, they gain access to manage tasks associated with their selected user. This includes viewing tasks, modifying task statuses, reassigning tasks, and adding new tasks.

## What to add
1. Authentication/Authorization.
2. Better client performance.
3. Client tests and more api tests.
4. Some kind of hosting like docker/cloud.
5. CI/CD.

## Technologies used:
- Backend: .NET Core for API development, Entity Framework for database operations, Serilog for logging, Automapper for mapping.
- Frontend: React.js for the user interface, Axios for API communication, Context API for store management, Materiel UI for styling.
- Database: SQL Server for data storage and management.
