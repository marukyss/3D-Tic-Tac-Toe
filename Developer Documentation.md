## System overview
---
This is a scalable, 3-dimensional Tic-Tac-Toe console application. It supports dynamic board sizes (from 3 to 10) and a given number of players (from 2 to 10).   
## Core architecture
---
There are three files, each with a distinct responsibility: **Board.cs**, **GameEngine.cs** and **Program.cs**.
- **Board.cs: ** Defines Board with two properties: Size and grid. It manages all board states and interactions.
- **GameEngine.cs: ** Defines GameEngine class with five properties: IsGameOver, Winner, Board, a list of players and currentPlayerIndex. It handles turn chnaginf, move validation and win-condition check. 
- **Program.cs: ** Contains the Player and Program classes. The Player class holds three properties: Name, Symbol and Color. Program.cs manages game setup, interactions with players and saving results. 

## Board.cs
---
-  **DisplayGrid(List<Player> players)** - dynamically calculates how many 3D grid layers can fit horizontally based on current terminal width and renders them in rows;
-   **DisplayGridRange(int startZ, int endZ, int blockWidth, List<Player> players)** - displays the current grid from the given starting layer index (inclusive) to ending layer index (exclusive), colors every symbol by the color of its player and creates formatted grid borders.

## GameEngine.cs
---
- **CheckWin(int x, int y, int z)** - evaluates win conditions around the newly placed symbol at (x, y, z). Takes each of 13 distinct 3D direction vectors (3 straight axes, 6 face diagonals, 4 space diagonals) and goes as far as possible to the positive side, and analogously, to the negative side. Accumulates consecutive symbols and returns true, if count reaches board's size; 
- **MakeMove(int x, int y, int z)** - validates game states, tries to set a symbol to the cell, calls win/draw evaluation, update state flag and advances the turn. Returns $true$ if the move was successfully placed.

## Program.cs
---
- **Main(string[] args)** - the entry poin of the game. Manages the game lifecycle, including the initial setup, game loop execution, parsing of the coordinates $(z, y, x)$, outcome checks, file saving and replay/exit transitions;
- **SetupPlayers(int playerCount)** - handles the interactive setup for all players. Prompts for unique names and symbols, and dynamically assigns terminal colors from a given array based on the player's index;
- **SaveMatchResult(Player? winner, List<Player> players, int boardSize)** - suggests saving game results and appends it to the game_history.txt;
- **ReadInt(string prompt, int min, int max)** - repeatedly prompts user to enter the integer between provided min and max values;
- **ReadYesNo(string prompt)** - repeadetly prompts user to answer yes/y or no/n.