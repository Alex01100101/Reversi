using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class GameGrid
    {
        private readonly int[,] _grid;

        public int Rows { get; }

        public int Columns { get; }

        public int this[int r, int c]
        {
            get => _grid[r, c];
            set => _grid[r, c] = value;
        }

        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            _grid = new int[rows, columns];
        }

        public Position[] GetValidMoves(Position currentPosition)
        {
            //todo
            return new Position[];
        }

        public Position[] GetAllValidMoves(int currentPlayer)
        {
            //todo foreach piesa a playerului GetValidMoves
            return new Position[];
        }

        public void Update(Position pressedPosition)
        {
            //todo propaga caturarea
        }
    }
}
