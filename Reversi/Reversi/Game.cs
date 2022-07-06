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

        private bool _gameOver;
        public bool GameOver
        {
            get => _gameOver;
            private set
            {
                _gameOver = value;
            }
        }

        private int _scoreBlack;

        public int ScoreBlack
        {
            get => _scoreBlack;
            private set
            {
                _scoreBlack = value;
            }
        }
        private int _scoreWhite;

        public int ScoreWhite
        {
            get => _scoreWhite;
            private set
            {
                _scoreWhite = value;
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
            GameGrid = new GameGrid(rows: 8, columns: 8);
            Reset();
        }

        public void Reset()
        {
            _gameOver = false;
            _player = 1;
            _scoreBlack = 0;
            _scoreWhite = 0;
        }
    }
}
