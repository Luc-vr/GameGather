# Game Gather
Game Gather is a simple test project developed using C# in the .NET framework. It incorporates various technologies such as the Entity Framework, ASP.NET Identity, SCSS, and Bootstrap. The project initiates a web application and provides both a GraphQL API and a REST API.

## Features
The features of Game Gather are:
- **Host Board Game Nights**: Plan and organize your board game events.
- **Join Board Game Nights**: Find and participate in board game nights hosted by others.
- **Board Game Overview**: View details about the available board games.
- **Food Preferences**: Input your dietary preferences and receive a warning if they do not align with the planned food for the game night.
- **Reviews and Ratings**: Leave feedback for the board game nights you've attended and view the average score of a host before deciding to join their event.

## Local Setup
To run this project locally, you need to have MSSQL Server up and running. You will need to create two databases: `GameGatherMasterData` and `GameGatherUserData`. You can modify the database connection settings in the [`appsettings.json`](https://github.com/Luc-vr/GameGather/blob/master/GameGather/appsettings.json) file.

Once the database server is operational, open the solution and run the program (Visual Studio is recommended for this). You can either create a new account or use the provided credentials to log into an existing account with pre-seeded data.

The credentials for the existing account are:

Email: `john@doe.nl`  
Password: `John123`
