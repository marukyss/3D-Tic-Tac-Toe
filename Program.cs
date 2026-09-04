using System;
using System.Collections.Generic;
using System.IO;
namespace TicTacToe3D
{
    public class Player
    {
        public string Name { get; }
        public char Symbol { get; }

        public Player(string name, char symbol){
            Name = name;
            Symbol = symbol;
        }
    }

    internal class Program
    {
        private static List<Player> SetupPlayers(int playerCount)
        {
            var players = new List<Player>();

            for(int i=0; i<playerCount; ++i)
            {
                Console.WriteLine($"\n --- Player {i+1} Setup ---");
                Console.Write("Enter player name: ");
                string name = Console.ReadLine()?.Trim();
                while (!IsValidName(name, players))
                {
                    Console.Write("Invalid or taken name. Please enter a different name: ");
                    name = Console.ReadLine()?.Trim();
                }

                Console.Write("Enter player symbol (single character): ");
                string symbol = Console.ReadLine()?.Trim();
                while (!IsValidSymbol(symbol, players))
                {
                    Console.Write("Invalid or taken symbol. Enter a different symbol: ");
                    symbol = Console.ReadLine()?.Trim();
                }
                players.Add(new Player(name, char.Parse(symbol)));
            }

            return players;
        }   
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to 3D Tic-Tac-Toe! Let the fun begin.");
            int boardSize = 0;
            var players = new List<Player>();
            bool keepPlaying = true;

            while (keepPlaying)
            {
                if(players.Count == 0)
                {
                    boardSize = ReadInt("Enter the board size: ", 3, 10);
                    int playerCount = ReadInt("Enter number of players: ", 2, 10);
                    players = SetupPlayers(playerCount);
                }

                var engine = new GameEngine(boardSize, players);
                while (!engine.IsGameOver)
                {
                    Console.Clear();
                    engine.Board.DisplayGrid();
                    Console.WriteLine($"It is {engine.CurrentPlayer.Name}'s (Symbol: {engine.CurrentPlayer.Symbol}) turn.");

                    int x=-1, y=-1, z=-1;                   
                    bool validCoordinatesFlag=false;

                    while(!validCoordinatesFlag)
                    {
                        Console.WriteLine("Enter the coordinates (z, y, x): "); 
                        string? coordinates = Console.ReadLine()?.Trim();
                        string[] parts = coordinates.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        if(parts.Length == 3 &&
                            int.TryParse(parts[0], out int layerInput) &&
                            char.TryParse(parts[1], out char yChar) &&
                            char.TryParse(parts[2], out char xChar))
                        {
                            x = xChar - 'A';
                            y = yChar - 'a';
                            z = layerInput-1;
                            if(x>= 0 && x<boardSize && y>= 0 && y<boardSize && z>= 0 && z<boardSize) validCoordinatesFlag=true;
                            else Console.WriteLine("Coordinates are out of bounds. Try again.");
                        } else {
                            Console.WriteLine("Invalid format. Expected format: '[int] [lowercaseChar] [uppercaseChar]'");
                        }
                    }

                    if(!engine.MakeMove(x, y, z))
                    {
                        Console.WriteLine("\n Invalid move! Press any key to try again...");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                engine.Board.DisplayGrid();

                if (engine.Winner != null)
                    Console.WriteLine($"\nCongratulations! {engine.Winner.Name} ({engine.Winner.Symbol}) wins!");
                else
                    Console.WriteLine($"\nIt is a draw! No more available moves.");

                if(ReadYesNo("\nDo you want to save this match result? (y/n): "))
                    SaveMatchResult(engine.Winner, players, boardSize);

                Console.WriteLine("\nWhat would you like to do next?");
                Console.WriteLine("1. Play again with the same settings.");
                Console.WriteLine("2. Play a completely new game (change board and players)");
                Console.WriteLine("3. Quit");
                int choice = ReadInt("Enter your choice (1-3): ", 1, 3);

                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        players.Clear();
                        break;
                    case 3:
                        keepPlaying = false;
                        break;
                }
            }
        }

        private static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim().ToLower();

                if(input == "y" || input == "yes") return true;
                if(input == "n" || input == "no") return false;

                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }
        private static void SaveMatchResult(Player Winner, List<Player> players, int boardSize)
        {
            //factor out the winner and add the string to the file
            string result = (Winner != null) ? $"Winner: {Winner.Name}" : "Result: Draw";
            string log = $"[{DateTime.Now}] Board: {boardSize}x{boardSize}x{boardSize} | Players: {players.Count} | {result}\n";

            try
            {
                File.AppendAllText("game_history.txt", log);
                Console.WriteLine("\nMatch result saved!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save result. {ex.Message}");
            }
        }
        private static bool IsValidName(string name, List<Player> existingPlayers)
        {
            if(string.IsNullOrWhiteSpace(name)) return false;
            for(int i=0; i<existingPlayers.Count; ++i)
            {
                if(name==existingPlayers[i].Name) return false;
            }
            return true;
        }

        private static bool IsValidSymbol(string symbol, List<Player> existingPlayers)
        {
            if(!char.TryParse(symbol, out char parsedSymbol) || char.IsWhiteSpace(parsedSymbol)) return false;
            for(int i=0; i<existingPlayers.Count; ++i)
            {
                if(parsedSymbol==existingPlayers[i].Symbol) return false;
            }
            return true;
        }
        private static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if(int.TryParse(input, out int result) && result>= min && result<=max)
                    return result;

                Console.WriteLine($"Invalid input. Please enter an integer between {min} and {max}.");
            }
        }
    }
}