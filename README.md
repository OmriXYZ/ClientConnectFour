# Connect Four Game - ASP.NET Server & .NET WinForms Client

Welcome to the Connect Four Game project! This project consists of a server built using ASP.NET, Entity Framework, and Razor Pages/Web API Core, along with a .NET WinForms client that communicates with the server using Web API core.

## Project Overview

Connect Four is a classic two-player board game in which players take turns dropping colored discs from the top into a vertically suspended grid. The objective is to be the first to connect four of one's own discs of the same color in a row, vertically, horizontally, or diagonally, before the opponent.

This project aims to provide a digital version of the Connect Four game, allowing players to compete against each other through the WinForms client while utilizing the server for game state management and communication.

For server project: https://github.com/OmriXYZ/ServerConnectFour

## Project Structure

The project consists of two separate components: the server and the client.

1. **Server (ASP.NET, Entity Framework, Razor Pages, Web API Core):**

   The server is responsible for managing the game state, processing moves, and facilitating communication with the client.

   - **ConnectFour.Server:** This is the ASP.NET project for the server.
   - **ConnectFour.Server.Data:** This project contains the Entity Framework data models and database context for managing game state.
   - **ConnectFour.Server.Pages:** The Razor Pages for rendering game UI and handling user interactions.
   - **ConnectFour.Server.Api:** The Web API controllers for handling game-related API requests from the client.

2. **Client (.NET WinForms, Entity Framework):**

   The client provides a graphical interface for players to interact with the game.

   - **ConnectFour.Client:** This is the .NET WinForms project for the client.
   - **ConnectFour.Client.Models:** This project contains the Entity Framework data models for representing game state on the client.
   - **ConnectFour.Client.Services:** Contains services for interacting with the server through Entity Framework.

## Getting Started

Before running the project, ensure you have the necessary tools and dependencies installed:

- Visual Studio (or Visual Studio Code) with ASP.NET, .NET Core, and WinForms support.
- Entity Framework Core.

To set up and run the project:

1. **Server:**

   - Open the `ConnectFour.Server` project in Visual Studio.
   - Update the database connection string in `appsettings.json` to point to your desired database server.
   - Open the Package Manager Console and run migrations to create the database schema: `Update-Database`.
   - Build and run the server.

2. **Client:**

   - Open the `ConnectFour.Client` project in Visual Studio.
   - Ensure the server endpoint URL in `App.config` matches the server's URL.
   - Open the Package Manager Console and run migrations to create the database schema: `Update-Database`.
   - Build and run the client.

## Game Rules and Interactions

The game follows the standard rules of Connect Four. Players take turns placing discs in one of the columns, and the game continues until a player wins or the board is full (resulting in a draw).

The client communicates with the server to perform moves and retrieve game state updates. The client handles the game logic, ensures valid moves, and broadcasts updates to the server.
