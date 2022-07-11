using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Game
    {
        public GameGrid GameGrid { get; private set; }

        public Position[] ValidMoves;

        private bool _gameOver;
        public bool GameOver
        {
            get => _gameOver;
            private set
            {
                _gameOver = value;
            }
        }

        private int _scoreBlue;

        public int ScoreBlue
        {
            get => _scoreBlue;
            private set
            {
                _scoreBlue = value;
            }
        }
        private int _scoreRed;

        public int ScoreRed
        {
            get => _scoreRed;
            private set
            {
                _scoreRed =value;
            }
        }

        private int _player;

        public int Player
        {
            get => _player;
             set
            {
                _player = value;
            }
        }

        public Game()
        {
            GameGrid = new GameGrid(rows: 8, columns: 8);
            Reset();
        }

        public void Reset()
        {
            _gameOver = false;
            _player = 1;
            _scoreBlue = 0;
            _scoreRed = 0;
            ValidMoves = new Position[50];
        }

        public bool IsGameOver()
        {
            //todo logic
            ValidMoves = GameGrid.GetAllValidMoves(_player);
            if (ValidMoves.Length == 0)
                return true;
            return false;
        }

        

        public Position[] ShowMoves(int _player)
        {
            ValidMoves = GameGrid.GetAllValidMoves(_player);
            if (ValidMoves.Length == 0)
            {
                if (_player == 1)
                    _player = 2;
                else
                    _player = 1;
            }
            return ValidMoves;
        }

        public void Move(Position pressedPosition, int player)
        {
            if (player == 1)
            {
                _player = 2;
                GameGrid.Update(pressedPosition, player);
            }
            if(player==2)
            {
                _player = 1;
                GameGrid.Update(pressedPosition, player);
            }
        }
    }
}
