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
        public int[,] GetGrid()
        {
            return _grid;
        }
        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            _grid = new int[rows, columns];
            _grid[(Rows / 2) - 1, (Columns / 2) - 1] = 1;
            _grid[(Rows / 2), (Columns / 2)] = 1;
            _grid[(Rows / 2) - 1, (Columns / 2)] = 2;
            _grid[(Rows / 2), (Columns / 2) - 1] = 2;
        }

        public int CalculateScoreRed()
        {
            int SR = 0;
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (_grid[i, j] == 1)
                        SR++;
            return SR;

        }

        public int CalculateScoreBlue()
        {
            int SB = 0;
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (_grid[i, j] == 2)
                        SB++;
            return SB;

        }

        public Position[] ValidMove(Position currentPosition, int currentPlayer, int row, int col)
        {
            Position position = new Position(currentPosition.Row + row, currentPosition.Column + col);
            if (position.IsValid() && _grid[currentPosition.Row + row, currentPosition.Column + col] != currentPlayer && _grid[currentPosition.Row + row, currentPosition.Column + col] != 0)
            {

                int count = 1;
                for (int i = currentPosition.Row + 2 * row, j = currentPosition.Column + 2 * col; i >= 0 && i < 8 && j >= 0 && j < 8; i = i + row, j = j + col)
                {
                    position = new Position(i, j);
                    if (position.IsValid() && _grid[i, j] != currentPlayer && _grid[i, j] != 0)
                        count++;
                }
                var newPosition = new Position(currentPosition.Row + row * (count + 1), currentPosition.Column + col * (count + 1));
                if (newPosition.IsValid() && _grid[newPosition.Row, newPosition.Column] == 0)
                    return new Position[] { newPosition };
            }
            return new Position[] { };
        }

        public Position[] GetValidMoves(Position currentPosition, int currentPlayer)
        {

            //miscari valide pt 1 singura piesa 
            Position[] p = new Position[] { };
            Position c1 = new Position(currentPosition.Row, currentPosition.Column);
            if (currentPosition.IsValid())
            {
                if (_grid[currentPosition.Row, currentPosition.Column] == currentPlayer)
                {  
                    p = p.Concat(ValidMove(currentPosition, currentPlayer, -1, 0)).ToArray();
                    p = p.Concat(ValidMove(currentPosition,currentPlayer,1, 0)).ToArray();
                    p=  p.Concat(ValidMove(currentPosition, currentPlayer, 0, -1)).ToArray();
                    p = p.Concat(ValidMove(currentPosition, currentPlayer, 0, 1)).ToArray();
                    p = p.Concat(ValidMove(currentPosition, currentPlayer, 1, 1)).ToArray();
                    p = p.Concat(ValidMove(currentPosition, currentPlayer, 1, -1)).ToArray();
                    p = p.Concat(ValidMove(currentPosition, currentPlayer, -1, 1)).ToArray();
                    p = p.Concat(ValidMove(currentPosition, currentPlayer, -1, -1)).ToArray();


                }
            }
            return p;


        }



        public Position[] GetAllValidMoves(int currentPlayer)
        {
            //todo foreach piesa a playerului GetValidMoves
            Position[] p = new Position[] { };
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    if (_grid[i, j] == currentPlayer)
                    {
                        Position poz = new Position(i, j);

                        p = p.Concat(GetValidMoves(poz, currentPlayer)).ToArray(); //fix
                    }

            return p;
        }

        public void Update(Position pressedPosition, int player)
        {
            // propaga capturarea
            if (player == 1)

                _grid[pressedPosition.Row, pressedPosition.Column] = 1;
            if (player == 2)
                _grid[pressedPosition.Row, pressedPosition.Column] = 2;

            Capture(pressedPosition, player);




        }

        public void DoCapture(Position pressedPosition, int currentPlayer, int row, int col)
        {
            int i, j, poz = 0, poz_row = 0, poz_col = 0;

            Position p=new Position(pressedPosition.Row+row,pressedPosition.Column+col);
            if (p.IsValid() && _grid[pressedPosition.Row + row, pressedPosition.Column + col] != currentPlayer && _grid[pressedPosition.Row + row, pressedPosition.Column + col] != 0)
            {
                for (i = pressedPosition.Row + 2 * row, j = pressedPosition.Column + 2 * col; (i >= 0 && i < 8) && (j >= 0 && j < 8); i = i + row, j = j + col)
                {
                    if (_grid[i, j] == currentPlayer || _grid[i,j]==0)
                    {
                        poz_row = i;
                        poz_col = j;
                        break;
                    }
                    
                }

                Position position = new Position(poz_row, poz_col);
                if (position.IsValid() && _grid[poz_row, poz_col] == currentPlayer) //?.?
                {
                    int poz1 = pressedPosition.Row;
                    int poz2 = pressedPosition.Column;
                    while (poz1 != poz_row || poz2!=poz_col ) 
                    {
                        if (poz1 >= 0 && poz2 >= 0)
                        {
                            _grid[poz1, poz2] = currentPlayer;
                            poz1 += row;
                            poz2 += col;
                        }
                    }
                }
            }
        }


        public void Capture(Position pressedPosition, int player)
        {

            DoCapture(pressedPosition, player, 0, 1);
            DoCapture(pressedPosition, player, 0, -1);
            DoCapture(pressedPosition, player, 1, 0);
            DoCapture(pressedPosition, player, 1, -1);
            DoCapture(pressedPosition, player, -1, 0);
            DoCapture(pressedPosition, player, 1, 1);
            DoCapture(pressedPosition, player, -1, -1);
            DoCapture(pressedPosition, player, -1, 1);
          


        }
    }
}
