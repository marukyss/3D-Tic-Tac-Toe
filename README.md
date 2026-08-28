# 3D-Tic-Tac-Toe
An implementation of 3D Tic-Tac-Toe in C#.

## Introduction


3D Tic-Tac-Toe is a three-dimensional extension of the world-known classic game. Instead of playing on a flat 3x3 grid, the game is presented in a form of a three-dimensional cube (NxNxN). In order to win, the player must align a full row along X, Y, or Z axes, planar diagonals or 3D space diagonals.

## Key features


- **Custom Board Dimensions** (Play on standard 3x3x3 grids or scale up to larger sizes like 10x10x10)
- **Multi-Player Support** (Configure games for 2 or more players with customizable names and unique symbols)
- **Match History Logging** (Export the result of the game to a .txt file)
- **Input Validation** (Prevents out-of-bounds placements, cell overwrites and handles non-integer inputs)

## Requirements


**Programming language + version:** C# (.NET 10.0)

**Used libraries:** System, System.IO, System.Text, System.Collections.Generic

**Start of the program:** Open the terminal in the project directory and run: "dotnet run"

## Game rules

Same rules as in a classic Tic-Tac-Toe: players take turns and place their symbols in the cells that weren't previously occupied. The first player to align N of their symbols in a continuous straight line(orthogonal lines, 2d planar diagonals, 3d space diagonals) wins immediately.
