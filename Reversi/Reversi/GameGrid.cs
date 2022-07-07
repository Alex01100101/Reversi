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

        private readonly Offset[] _offsets=new Offset[8];

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
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    _grid[i, j] = 0;
            _grid[rows / 2-1,columns / 2-1] = 1;
            _grid[rows / 2, columns / 2-1] = 2;
            _grid[rows / 2-1, columns / 2] = 2;
            _grid[rows / 2, columns / 2] = 1;

            _offsets = GetOffsets();
        }

        private Position[] GetValidMoves(Position currentPosition)
        {
            Position[] positions = new Position[] { };
            foreach(var offset in _offsets)
            {
                var nextPosition=currentPosition.Apply(offset);
                if (nextPosition.IsValid())
                    while (nextPosition.IsValid() &&
                    _grid[currentPosition.Row, currentPosition.Column] 
                    != _grid[nextPosition.Row,nextPosition.Column] &&
                    _grid[nextPosition.Row, nextPosition.Column] != 0)
                {
                    nextPosition = nextPosition.Apply(offset);
                }

                if (nextPosition.IsValid() && 
                    _grid[nextPosition.Row, nextPosition.Column] == 0 && (
                    nextPosition.Row != currentPosition.Apply(offset).Row ||
                    nextPosition.Column != currentPosition.Apply(offset).Column))
                    positions = positions.Concat(new Position[] { nextPosition }).ToArray();
            }
            return positions;
        }

        public Position[] GetAllValidMoves(int currentPlayer)
        {
            Position[] positions=new Position[] { };
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (_grid[i, j] == currentPlayer)
                        positions=positions.Concat(GetValidMoves(new Position(i, j))).ToArray();
            return positions;
        }

        public int Update(Position pressedPosition,int currentPlayer)
        {
            int numberTurned = 0;
            _grid[pressedPosition.Row, pressedPosition.Column] = currentPlayer;
            foreach (var offset in _offsets)
            {
                var nextPosition = pressedPosition.Apply(offset);
                if(nextPosition.IsValid())
                while (nextPosition.IsValid()&&
                    _grid[pressedPosition.Row, pressedPosition.Column]
                    != _grid[nextPosition.Row, nextPosition.Column] &&
                    _grid[nextPosition.Row, nextPosition.Column] != 0)
                {
                    nextPosition = nextPosition.Apply(offset);
                }
                if (nextPosition.IsValid() &&
                    _grid[nextPosition.Row, nextPosition.Column] != 0 &&
                    _grid[nextPosition.Row, nextPosition.Column] == currentPlayer && (
                    nextPosition.Row != pressedPosition.Apply(offset).Row ||
                    nextPosition.Column != pressedPosition.Apply(offset).Column))
                {
                    nextPosition = pressedPosition.Apply(offset);
                        if (nextPosition.IsValid())
                            while (nextPosition.IsValid() &&
                        _grid[pressedPosition.Row, pressedPosition.Column]
                        != _grid[nextPosition.Row, nextPosition.Column] &&
                        _grid[nextPosition.Row, nextPosition.Column] != 0)
                    {
                        _grid[nextPosition.Row, nextPosition.Column] = currentPlayer;
                            numberTurned++;
                        nextPosition = nextPosition.Apply(offset);
                    }
                    
                }
            }
            return numberTurned;
        }

        private Offset[] GetOffsets()
        {
            var offsets = new Offset[8];
            offsets[0] = new Offset(-1, 0);
            offsets[1] = new Offset(-1, 1);
            offsets[2] = new Offset(0, 1);
            offsets[3] = new Offset(1, 1);
            offsets[4] = new Offset(1, 0);
            offsets[5] = new Offset(1, -1);
            offsets[6] = new Offset(0, -1);
            offsets[7] = new Offset(-1, -1);
            return offsets;

        }
    }
}
