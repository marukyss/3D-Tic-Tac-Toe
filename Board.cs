using System;
namespace TicTacToe3D
{
    public class Board
    {
        public int Size { get; }
        private char[, ,] grid;
        
        public Board(int size = 3){
            Size=size;
            grid = new char[Size, Size, Size];
            Clear();
        }

        public bool IsInBounds(int x, int y, int z) =>
            x >= 0 && x < Size &&
            y >= 0 && y < Size &&
            z >= 0 && z < Size;
        public char GetCell(int x, int y, int z){
            if(!IsInBounds(x, y, z))
                throw new ArgumentOutOfRangeException($"Coordinates ({x}, {y}, {z}) are outside the range.");

            return grid[x, y, z];
        }
        public bool SetCell(int x, int y, int z, char symbol){
            if(!IsInBounds(x, y, z) || !IsCellEmpty(x, y, z))
                return false;
            
            grid[x, y, z]=symbol;
            return true;
        }

        public bool IsCellEmpty(int x, int y, int z) => GetCell(x, y, z)=='.';
        
        public void DisplayGrid(){
            for(int z=0; z<Size; ++z){
                Console.WriteLine($"Layer {z+1} (z={z+1})");
                Console.Write("   ");
                for(int i=0; i<Size; ++i)
                {
                    Console.Write($"{(char) (i+'A')}   ");
                }
                Console.WriteLine();
                for(int y=0; y<Size; ++y){
                    Console.Write($"{(char)(y+'a')} ");
                    for(int x=0; x<Size; ++x){
                        char symbol=GetCell(x, y, z);
                        Console.Write($" {symbol} ");
                        if(x!=Size-1) Console.Write("|");
                    }
                    if(y!=Size-1){
                        Console.WriteLine();
                        Console.Write("  ");
                        for(int l=0; l<Size; ++l){
                            if(l!=Size-1) Console.Write("---+");
                            else Console.Write("---");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            
        }

        public void Clear(){
            for(int i=0; i<Size; ++i){
                for(int j=0; j<Size; ++j){
                    for(int k=0; k<Size; ++k){
                        grid[i, j, k]='.';
                    }
                }
            }
        }

        public bool IsFull(){
            for(int i=0; i<Size; ++i){
                for(int j=0; j<Size; ++j){
                    for(int k=0; k<Size; ++k){
                        if(grid[i, j, k]=='.') return false;
                    }
                }
            }
            return true;
        }
    }
}