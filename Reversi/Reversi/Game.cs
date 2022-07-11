using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Game : INotifyPropertyChanged
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
                OnPropertyChanged(nameof(GameOver));
            }
        }

        private int _scoreBlack;

        public int ScoreBlack
        {
            get => _scoreBlack;
            private set
            {
                _scoreBlack = value;
                OnPropertyChanged(nameof(ScoreBlack));
            }
        }
        private int _scoreWhite;

        public int ScoreWhite
        {
            get => _scoreWhite;
            private set
            {
                _scoreWhite = value;
                OnPropertyChanged(nameof(ScoreWhite));
            }
        }

        private int _player;

        public int Player
        {
            get => _player;
            private set
            {
                _player = value;
            }
        }

        public Game()
        {
            //GameGrid = new GameGrid(rows: 8, columns: 8);
            Reset();
        }

        public void Reset()
        {
            GameGrid = new GameGrid(rows: 8, columns: 8);
            GameOver = false;
            _player = 1;
            ScoreBlack = 2;
            ScoreWhite = 2;
            ValidMoves = new Position[] { };
        }

        public bool IsGameOver()
        {
            Position[] ValidMoves1 = GameGrid.GetAllValidMoves(1);
            Position[] ValidMoves2 = GameGrid.GetAllValidMoves(2);
            if (ValidMoves1.Length == 0 && ValidMoves2.Length == 0)
            {
                GameOver = true;
                return true;
            }
            return false;
        }

        public Position[] ShowMoves()
        {
            ValidMoves = GameGrid.GetAllValidMoves(_player);
            if (ValidMoves.Length == 0)
            {
                if (_player == 1)
                    _player = 2;
                else
                    _player = 1;
            }
            ValidMoves = GameGrid.GetAllValidMoves(_player);
            return ValidMoves;
        }

        public void Move(Position pressedPosition)
        {
            int numberTurned=GameGrid.Update(pressedPosition,_player);
            if (_player == 1)
            {
                _player = 2;
                ScoreBlack += numberTurned + 1;
                ScoreWhite -= numberTurned;
            }
            else
            {
                _player = 1;
                ScoreWhite += numberTurned + 1;
                ScoreBlack -= numberTurned;
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
