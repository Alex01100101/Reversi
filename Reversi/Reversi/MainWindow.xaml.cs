using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reversi
{
    public partial class MainWindow : Window
    {
        private Game _game;
        private Button[,] _tiles = new Button[0, 0];
      
        public MainWindow()
        {
            InitializeComponent();
            _game = new Game();
            CreateGridElements();
            Setup();
            labelScoreRed.Content ="Score Red Player=" +_game.ScoreRed;
            labelScoreBlue.Content ="Score Blue Player="+_game.ScoreBlue;


        }
        private void CreateGridElements()
        {
            for (var i = 0; i < _game.GameGrid.Rows; i++)
            {
                this.visualGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (var i = 0; i < _game.GameGrid.Columns; i++)
            {
                this.visualGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            _tiles = new Button[_game.GameGrid.Rows, _game.GameGrid.Columns];

            for (var row = 0; row < _game.GameGrid.Rows; row++)
            {
                for (var col = 0; col < _game.GameGrid.Columns; col++)
                {
                    var tile = new Button();

                    tile.SetValue(Grid.RowProperty, row);
                    tile.SetValue(Grid.ColumnProperty, col);
                    tile.BorderBrush = Brushes.LightGray;
                    tile.BorderThickness = new Thickness(1);
                    this.visualGrid.Children.Add(tile);
                    _tiles[row, col] = tile;
                    _tiles[row, col].Click += btn_Click;
                    _tiles[row, col].MouseDoubleClick += ShowSuggestion;
                }
            }
        }

        private void Setup()
        {
            var row = _game.GameGrid.Rows ;
            var col = _game.GameGrid.Columns;
            _tiles[(row / 2)-1, (col / 2)-1].Background = Brushes.Crimson;
            _tiles[(row / 2) , (col / 2) ].Background = Brushes.Crimson;
            _tiles[(row / 2)-  1, (col / 2) ].Background = Brushes.DarkBlue;
            _tiles[(row / 2) , (col / 2)-1].Background = Brushes.DarkBlue;

        }


        private void btn_Click(object sender, RoutedEventArgs e)
        {
              
        

            if (_game.Player == 1)
            {
               
                for (int i = 0; i < _game.GameGrid.Rows; i++)
                    for (int j = 0; j < _game.GameGrid.Columns; j++)
                       

                    {
                        Position poz = new Position(i, j);
                        if (_tiles[i, j] == sender && _tiles[i, j].Background == Brushes.Green)
                        {
                            _tiles[i, j].Background = Brushes.Crimson;
                            _game.Move(poz, _game.Player);
                            Refresh();
                            
                        }
                    }
                
            }
            else
            if(_game.Player==2)
            {

                for (int i = 0; i < _game.GameGrid.Rows; i++)
                    for (int j = 0; j < _game.GameGrid.Columns; j++)


                    {
                        Position poz = new Position(i, j);
                        if (_tiles[i, j] == sender && _tiles[i, j].Background == Brushes.Green)
                        {
                            _tiles[i, j].Background = Brushes.DarkBlue;
                            _game.Move(poz, _game.Player);
                            Refresh();
                           

                        }
                    }
            }

        }

        private void Refresh()
        {

            for (int i = 0; i < _game.GameGrid.Rows; i++)
                for (int j = 0; j < _game.GameGrid.Columns; j++)
                {
                    if (_game.GameGrid[i, j] == 1)
                        _tiles[i, j].Background = Brushes.Crimson;
                  
                    if (_game.GameGrid[i, j] == 2)
                        _tiles[i, j].Background = Brushes.DarkBlue;
                    if (_tiles[i, j].Background == Brushes.Green)
                    {
                        _tiles[i, j].BorderBrush = Brushes.LightGray;
                        _tiles[i, j].Background = Brushes.LightGray;
                    }
                }
            labelScoreRed.Content = "Score Red Player=" + _game.GameGrid.CalculateScoreRed();
            labelScoreBlue.Content = "Score Blue Player=" + _game.GameGrid.CalculateScoreBlue();

        }

       

        private void ShowSuggestion(object sender, RoutedEventArgs e)
        {
            if (_game.Player == 1)
            {
               
                Position[] moves1 = new Position[] { };

                moves1 = moves1.Concat(_game.ShowMoves(1)).ToArray();
                for (int i = 0; i < moves1.Length; i++)
                {
                    int row = moves1[i].Row;
                    int col = moves1[i].Column;



                  //  label1.Content = "Jucator 1 " + row + " " + col;
                    _tiles[row, col].Background = Brushes.Green;

                }
            }


           else
            if(_game.Player == 2)
            {
                Position[] moves2 = new Position[] { };

                moves2 = moves2.Concat(_game.ShowMoves(2)).ToArray();
                for (int i = 0; i < moves2.Length; i++)
                {
                    int row = moves2[i].Row;
                    int col = moves2[i].Column;



                   // label1.Content = "Jucator 2 " + row + " " + col;
                    _tiles[row, col].Background = Brushes.Green;

                }



            }

        }

       

    }

}   
    

