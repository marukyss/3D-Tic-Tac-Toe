namespace TicTacToe3D
{
    public class GameEngine
    {
        public bool IsGameOver { get; private set; } = false; //to make it impossible for Program.cs to randomly interrupt the game
        public Player Winner { get; private set; } = null; //to make it impossible for Program.cs to rewrite the winner
        private readonly Board board;
        private readonly List<Player> players;
        private int currentPlayerIndex = 0;

        public Board Board => board;
        public Player CurrentPlayer => players[currentPlayerIndex];
        private readonly (int dx, int dy, int dz)[] directions = new[]
        {
                (1,0,0), (0,1,0), (0,0,1),      // 3 straight axes
                (1,1,0), (1,-1,0),              // 6 face diagonals
                (1,0,1), (1,0,-1),
                (0,1,1), (0,1,-1),
                (1,1,1), (1,1,-1), (1,-1,1), (-1,1,1) // 4 main 3D diagonals
        };

        public GameEngine(int size, List<Player> players)
        {
            board = new Board(size);
            this.players = players;
        }

        private void ChangeTurn() => currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        private bool CheckWin(int x, int y, int z) //check in all directions if there a winning row, column or diagonal
        {
            char currentSymbol = CurrentPlayer.Symbol;
            foreach (var (dx, dy, dz) in directions){
                int count = 1;
                int new_dx = x + dx, new_dy = y + dy, new_dz = z + dz;

                while (board.IsInBounds(new_dx, new_dy, new_dz) && board.GetCell(new_dx, new_dy, new_dz) == currentSymbol)
                {
                    count++;
                    new_dx += dx; new_dy += dy; new_dz += dz;
                }

                new_dx = x - dx; new_dy = y - dy; new_dz = z - dz;
                while (board.IsInBounds(new_dx, new_dy, new_dz) && board.GetCell(new_dx, new_dy, new_dz) == currentSymbol)
                {
                    count++;
                    new_dx -= dx; new_dy -= dy; new_dz -= dz;
                }

                if (count>= board.Size)
                    return true;
            }
            return false;
        }
        
        public bool MakeMove(int x, int y, int z)
        {
            if(IsGameOver) return false;

           if (!board.SetCell(x, y, z, CurrentPlayer.Symbol))
                return false;

            if(CheckWin(x, y, z))
            {
                IsGameOver = true;
                Winner = CurrentPlayer;
                return true;
            }

            if (board.IsFull())
            {
                IsGameOver = true;
                Winner = null; //null indicates that there are no winners
                return true;
            }

            ChangeTurn();
            return true;
        }
    }
}