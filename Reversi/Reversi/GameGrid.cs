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

        public Position[] GetValidMoves(Position currentPosition)
        {

            //miscari valide pt 1 singura piesa 
            //todo
            Position[] p = new Position[] { };
            Position c1 = new Position(currentPosition.Row, currentPosition.Column);
            if (currentPosition.IsValid())
            {

                if (_grid[currentPosition.Row, currentPosition.Column] == 1)
                {
                    Position position = new Position(currentPosition.Row-1, currentPosition.Column);
                    if (position.IsValid() && _grid[currentPosition.Row - 1, currentPosition.Column] == 2 )
                    {

                        int count = 1;
                        for (int i = currentPosition.Row - 2; i >= 1; i--)
                        {
                            position=new Position(i, currentPosition.Column);
                            if (position.IsValid() && _grid[i, currentPosition.Column] == 2)
                                count++;
                        }
                        position = new Position(currentPosition.Row-count-1, currentPosition.Column);
                        if (position.IsValid() && _grid[currentPosition.Row - count - 1, currentPosition.Column] == 0)
                            p = p.Append(new Position(currentPosition.Row - (count + 1), currentPosition.Column)).ToArray();

                    }



                     position= new Position(currentPosition.Row , currentPosition.Column+1);
                    if (position.IsValid() && _grid[currentPosition.Row, currentPosition.Column + 1] == 2)
                    {
                        
                        int count = 1;
                        for (int j = currentPosition.Column + 2; j < Columns; j++)
                        {
                            position = new Position(currentPosition.Row, j);
                            if (position.IsValid() && _grid[currentPosition.Row, j] == 2)
                                count++;
                        }//numaram cate piese albastre avem in dreapta
                        position = new Position(currentPosition.Row, currentPosition.Column + 1+count);
                        if (position.IsValid() && _grid[currentPosition.Row, currentPosition.Column + count + 1] == 0)                          //daca dupa ultima piesa numarata avem o celula cu val 0
                                                                                                                          //=> este o miscare valida
                        {
                            p = p.Append(new Position(currentPosition.Row, currentPosition.Column + count + 1)).ToArray();//exista piese albastre la dreapta celei rosii
                        }
                    }

                    position = new Position(currentPosition.Row, currentPosition.Column -1);
                    if (position.IsValid() && _grid[currentPosition.Row, currentPosition.Column - 1] == 2)
                    {
                        int count = 1;
                        for (int j = currentPosition.Column - 2; j >= 1; j--)
                        {
                            position = new Position(currentPosition.Row, j);
                            if (position.IsValid() && _grid[currentPosition.Row, j] == 2)
                                count++;
                        }
                        position = new Position(currentPosition.Row, currentPosition.Column - 1 - count);
                        if (position.IsValid() && _grid[currentPosition.Row, currentPosition.Column - count - 1] == 0)                             //daca dupa ultima piesa numarata avem o celula cu val 0
                        {                                                                                                    //=> este o miscare valida
                            p = p.Append(new Position(currentPosition.Row, currentPosition.Column - count - 1)).ToArray();   //exista piese albastre la stanga  celei rosii
                        }
                    }

                    position = new Position(currentPosition.Row+1, currentPosition.Column);
                    if (position.IsValid() && _grid[currentPosition.Row + 1, currentPosition.Column] == 2)
                    {
                        int count = 1;

                        for (int i = currentPosition.Row + 2; i < Rows; i++)
                        {
                            position = new Position(i, currentPosition.Column);
                            if (position.IsValid() && _grid[i, currentPosition.Column] == 2)
                                count++;
                        }
                        position = new Position(currentPosition.Row+count+1, currentPosition.Column);
                        if (position.IsValid() && _grid[currentPosition.Row + count + 1, currentPosition.Column] == 0)
                        {
                            p = p.Append(new Position(currentPosition.Row + count + 1, currentPosition.Column)).ToArray();//ex piese albastre sub piesa rosie
                        }
                    }

                    //diagonale
                    position = new Position(currentPosition.Row-1, currentPosition.Column -1);
                    if (position.IsValid() && _grid[currentPosition.Row - 1, currentPosition.Column - 1] == 2)
                    {
                        int count = 1;
                        for (int i = currentPosition.Row - 2; i >= 1; i--)
                        {
                            for (int j = currentPosition.Column - 2; j >= 1; j--)
                            {
                                position = new Position(i, j);
                                if (position.IsValid() && _grid[i, j] == 2)
                                    count++;
                            }
                        }
                        position = new Position(currentPosition.Row-(count-1), currentPosition.Column - (count-1));

                        if (position.IsValid() && _grid[currentPosition.Row - (count - 1), currentPosition.Column - (count - 1)] == 0)
                            p = p.Append(new Position(currentPosition.Row - count - 1, currentPosition.Column - count - 1)).ToArray();//ex piese albastre pe diag sec stanga-sus celei rosii

                    }


                    position = new Position(currentPosition.Row+1, currentPosition.Column - 1);
                    if (position.IsValid() && _grid[currentPosition.Row + 1, currentPosition.Column - 1] == 2)
                    {
                        int count = 1;
                        for (int i = currentPosition.Row + 2; i < Rows; i++)
                            for (int j = currentPosition.Column - 2; j >= 1; j--)
                            {
                                position = new Position(i, j);
                                if (position.IsValid() && _grid[i, j] == 2)
                                    count++;

                            }
                        position = new Position(currentPosition.Row+count+1, currentPosition.Column - 1 - count);
                        if (position.IsValid() && _grid[currentPosition.Row + count + 1, currentPosition.Column - count - 1] == 0)
                            p = p.Append(new Position(currentPosition.Row + count + 1, currentPosition.Column - count - 1)).ToArray();//ex piese albastre in stanga jos celei rosii
                    }


                    position = new Position(currentPosition.Row+1, currentPosition.Column + 1 );
                    if (position.IsValid() && _grid[currentPosition.Row + 1, currentPosition.Column + 1] == 2)
                    {
                        int count = 1;
                        for (int i = currentPosition.Row + 2; i < Rows; i++)
                            for (int j = currentPosition.Column + 2; j < Columns; j++)
                            {
                                position = new Position(i, j);
                                if (position.IsValid() && _grid[i, j] == 2)
                                    count++;
                            }
                        position = new Position(currentPosition.Row+count+1, currentPosition.Column + 1 + count);
                        if (position.IsValid() && _grid[currentPosition.Row + count + 1, currentPosition.Column + count + 1] == 0)
                            p = p.Append(new Position(currentPosition.Row + 2, currentPosition.Column + 2)).ToArray();//ex piese albastre la dreapta jos celei rosii
                    }


                    position = new Position(currentPosition.Row-1, currentPosition.Column + 1 );
                    if (position.IsValid() && _grid[currentPosition.Row - 1, currentPosition.Column + 1] == 2)
                    {
                        int count = 1;
                        for (int i = currentPosition.Row - 2; i >= 1; i--)
                            for (int j = currentPosition.Column + 2; j < Columns; j++)
                            {
                                position = new Position(i, j);
                                if (position.IsValid() && _grid[i, j] == 2)
                                    count++;
                            }

                        position = new Position(currentPosition.Row-count-1, currentPosition.Column + 1 + count);
                        if (position.IsValid() && _grid[currentPosition.Row - count - 1, currentPosition.Column + count + 1] == 0)
                            p = p.Append(new Position(currentPosition.Row - (count + 1), currentPosition.Column + (1 + count))).ToArray();//ex piese albastre la dreapta sus celei rosii


                    }












                    //if (_grid[currentPosition.Row - 1, currentPosition.Column+1] == 2)
                    //    p = p.Append(new Position(currentPosition.Row - 2, currentPosition.Column+2)).ToArray();//ex piesa albastra pe diag sec a piesei rosii

                    //if (_grid[currentPosition.Row + 1, currentPosition.Column - 1] == 2)
                    //    p = p.Append(new Position(currentPosition.Row + 2, currentPosition.Column - 2)).ToArray();//ex piesa albastra pe diag principala a piesei rosii

                }
                else
                {


                    Position position = new Position(currentPosition.Row - 1, currentPosition.Column);
                    if (position.IsValid() && _grid[currentPosition.Row - 1, currentPosition.Column] == 1)
                    {

                        int count = 1;
                        for (int i = currentPosition.Row - 2; i >= 1; i--)
                        {
                            position = new Position(i, currentPosition.Column);
                            if (position.IsValid() && _grid[i, currentPosition.Column] == 1)
                                count++;
                        }
                        position = new Position(currentPosition.Row - count - 1, currentPosition.Column);
                        if (position.IsValid() && _grid[currentPosition.Row - count - 1, currentPosition.Column] == 0)
                            p = p.Append(new Position(currentPosition.Row - (count + 1), currentPosition.Column)).ToArray();

                    }



                    position = new Position(currentPosition.Row, currentPosition.Column + 1);
                    if (position.IsValid() && _grid[currentPosition.Row, currentPosition.Column + 1] == 1)
                    {

                        int count = 1;
                        for (int j = currentPosition.Column + 2; j < Columns; j++)
                        {
                            position = new Position(currentPosition.Row, j);
                            if (position.IsValid() && _grid[currentPosition.Row, j] == 1)
                                count++;
                        }//numaram cate piese albastre avem in dreapta
                        position = new Position(currentPosition.Row, currentPosition.Column + 1 + count);
                        if (position.IsValid() && _grid[currentPosition.Row, currentPosition.Column + count + 1] == 0)                          //daca dupa ultima piesa numarata avem o celula cu val 0
                                                                                                                                                //=> este o miscare valida
                        {
                            p = p.Append(new Position(currentPosition.Row, currentPosition.Column + count + 1)).ToArray();//exista piese albastre la dreapta celei rosii
                        }
                    }

                    position = new Position(currentPosition.Row, currentPosition.Column - 1);
                    if (position.IsValid() && _grid[currentPosition.Row, currentPosition.Column - 1] == 1)
                    {
                        int count = 1;
                        for (int j = currentPosition.Column - 2; j >= 1; j--)
                        {
                            position = new Position(currentPosition.Row, j);
                            if (position.IsValid() && _grid[currentPosition.Row, j] == 1)
                                count++;
                        }
                        position = new Position(currentPosition.Row, currentPosition.Column - 1 - count);
                        if (position.IsValid() && _grid[currentPosition.Row, currentPosition.Column - count - 1] == 0)                             //daca dupa ultima piesa numarata avem o celula cu val 0
                        {                                                                                                    //=> este o miscare valida
                            p = p.Append(new Position(currentPosition.Row, currentPosition.Column - count - 1)).ToArray();   //exista piese albastre la stanga  celei rosii
                        }
                    }

                    position = new Position(currentPosition.Row + 1, currentPosition.Column);
                    if (position.IsValid() && _grid[currentPosition.Row + 1, currentPosition.Column] == 1)
                    {
                        int count = 1;

                        for (int i = currentPosition.Row + 2; i < Rows; i++)
                        {
                            position = new Position(i, currentPosition.Column);
                            if (position.IsValid() && _grid[i, currentPosition.Column] == 1)
                                count++;
                        }
                        position = new Position(currentPosition.Row + count + 1, currentPosition.Column);
                        if (position.IsValid() && _grid[currentPosition.Row + count + 1, currentPosition.Column] == 0)
                        {
                            p = p.Append(new Position(currentPosition.Row + count + 1, currentPosition.Column)).ToArray();//ex piese albastre sub piesa rosie
                        }
                    }

                    //diagonale
                    position = new Position(currentPosition.Row - 1, currentPosition.Column - 1);
                    if (position.IsValid() && _grid[currentPosition.Row - 1, currentPosition.Column - 1] == 1)
                    {
                        int count = 1;
                        for (int i = currentPosition.Row - 2; i >= 1; i--)
                        {
                            for (int j = currentPosition.Column - 2; j >= 1; j--)
                            {
                                position = new Position(i, j);
                                if (position.IsValid() && _grid[i, j] == 1)
                                    count++;
                            }
                        }
                        position = new Position(currentPosition.Row - (count - 1), currentPosition.Column - (count - 1));

                        if (position.IsValid() && _grid[currentPosition.Row - (count - 1), currentPosition.Column - (count - 1)] == 0)
                            p = p.Append(new Position(currentPosition.Row - count - 1, currentPosition.Column - count - 1)).ToArray();//ex piese albastre pe diag sec stanga-sus celei rosii

                    }


                    position = new Position(currentPosition.Row + 1, currentPosition.Column - 1);
                    if (position.IsValid() && _grid[currentPosition.Row + 1, currentPosition.Column - 1] ==1)
                    {
                        int count = 1;
                        for (int i = currentPosition.Row + 2; i < Rows; i++)
                            for (int j = currentPosition.Column - 2; j >= 1; j--)
                            {
                                position = new Position(i, j);
                                if (position.IsValid() && _grid[i, j] == 1)
                                    count++;

                            }
                        position = new Position(currentPosition.Row + count + 1, currentPosition.Column - 1 - count);
                        if (position.IsValid() && _grid[currentPosition.Row + count + 1, currentPosition.Column - count - 1] == 0)
                            p = p.Append(new Position(currentPosition.Row + count + 1, currentPosition.Column - count - 1)).ToArray();//ex piese albastre in stanga jos celei rosii
                    }


                    position = new Position(currentPosition.Row + 1, currentPosition.Column + 1);
                    if (position.IsValid() && _grid[currentPosition.Row + 1, currentPosition.Column + 1] == 1)
                    {
                        int count = 1;
                        for (int i = currentPosition.Row + 2; i < Rows; i++)
                            for (int j = currentPosition.Column + 2; j < Columns; j++)
                            {
                                position = new Position(i, j);
                                if (position.IsValid() && _grid[i, j] == 1)
                                    count++;
                            }
                        position = new Position(currentPosition.Row + count + 1, currentPosition.Column + 1 + count);
                        if (position.IsValid() && _grid[currentPosition.Row + count + 1, currentPosition.Column + count + 1] == 0)
                            p = p.Append(new Position(currentPosition.Row + 2, currentPosition.Column + 2)).ToArray();//ex piese albastre la dreapta jos celei rosii
                    }


                    position = new Position(currentPosition.Row - 1, currentPosition.Column + 1);
                    if (position.IsValid() && _grid[currentPosition.Row - 1, currentPosition.Column + 1] == 1)
                    {
                        int count = 1;
                        for (int i = currentPosition.Row - 2; i >= 1; i--)
                            for (int j = currentPosition.Column + 2; j < Columns; j++)
                            {
                                position = new Position(i, j);
                                if (position.IsValid() && _grid[i, j] == 1)
                                    count++;
                            }

                        position = new Position(currentPosition.Row - count - 1, currentPosition.Column + 1 + count);
                        if (position.IsValid() && _grid[currentPosition.Row - count - 1, currentPosition.Column + count + 1] == 0)
                            p = p.Append(new Position(currentPosition.Row - (count + 1), currentPosition.Column + (1 + count))).ToArray();//ex piese albastre la dreapta sus celei rosii


                    }
                }

                //_grid[currentPosition.Row+2, currentPosition.Column ] = 1; //(+2,0)
                //Offset o1 = new Offset(-2, 0);
                //currentPosition.Apply(o1); // <=>      p[0].Row=currentPosition.Row-2; //pe coloana

                //p = p.Append(currentPosition).ToArray();
                //Offset o2 = new Offset(0, -2);
                //c1.Apply(o2);
                //p = p.Append(c1).ToArray();

                //Offset o3=new Offset(-1, +1);
                //c2.Apply(o3);
                //p=p.Append(c2).ToArray();   

                //Offset o4=new Offset(+1, -1);
                //c3.Apply(o4);
                //p=p.Append(c3).ToArray();



                //  Offset o1 = new Offset(-2, 0);
                // currentPosition.Apply(o1); // <=>      p[0].Row=currentPosition.Row-2; //pe coloana

                //p= p.Append(currentPosition).ToArray();
                //  Offset o2 = new Offset(0, -2);
                // c.Apply(o2);
                //p= p.Append(c).ToArray();
                //p[0].Row = currentPosition.Row;
                //p[0].Column = currentPosition.Column;



                //p[1].Row = currentPosition.Row;
                //p[1].Column = currentPosition.Column; // <=>  p[1].Column = currentPosition.Column - 2; // pe linie






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

                        p = p.Concat(GetValidMoves(poz)).ToArray(); //fix
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

        public void Capture(Position pressedPosition, int player)
        {
            int i, j, poz = 0,poz_row=0,poz_col = 0;
            //capturare pe coloana in jos
            Position p = new Position(pressedPosition.Row + 1, pressedPosition.Column);
            if (p.IsValid() && _grid[pressedPosition.Row + 1, pressedPosition.Column] != player && _grid[pressedPosition.Row + 1, pressedPosition.Column] != 0)
            {
                for (i = pressedPosition.Row+2; i < Rows; i++)
                {
                    if (_grid[i, pressedPosition.Column] == player)
                    {
                        poz = i;   break;
                    }
                 

                }
                
                Position position= new Position(poz, pressedPosition.Column);
                if (position.IsValid() && _grid[poz, pressedPosition.Column] == player)
                {
                    while (position.IsValid() && poz > pressedPosition.Row)
                    {

                        _grid[poz, pressedPosition.Column] = player;
                        poz--;
                    }
                }
                
            }
            //verificare pe coloana in sus
            if (pressedPosition.Row > 0)
            {
              p = new Position(pressedPosition.Row - 1, pressedPosition.Column);
                if (p.IsValid() && _grid[pressedPosition.Row - 1, pressedPosition.Column] != player && _grid[pressedPosition.Row - 1, pressedPosition.Column] != 0)
                {
                    for (i = pressedPosition.Row - 2; i >= 0; i--)
                        if (_grid[i, pressedPosition.Column] == player)
                        {
                            poz = i; break;
                        }


                    Position position = new Position(poz, pressedPosition.Column);
                    if (position.IsValid() && _grid[poz, pressedPosition.Column] == player)
                    {
                        while (position.IsValid() && poz < pressedPosition.Row)
                        {
                            _grid[poz, pressedPosition.Column] = player;
                            poz++;
                        }
                    }

                }
            }

            p = new Position(pressedPosition.Row, pressedPosition.Column+1);
            if (p.IsValid() && _grid[pressedPosition.Row, pressedPosition.Column + 1] != player && _grid[pressedPosition.Row, pressedPosition.Column + 1] != 0)
            {
                for (j = pressedPosition.Column + 2; j < Columns; j++)
                    if (_grid[pressedPosition.Row, j] == player)
                    {
                        poz = j; break;
                    }
                Position position = new Position(pressedPosition.Row, poz);
                if (position.IsValid() && _grid[pressedPosition.Row, poz] == player)
                {
                    while (position.IsValid() && poz > pressedPosition.Column)
                    {
                        _grid[pressedPosition.Row, poz] = player;
                        poz--;
                    }
                }


            }


            p = new Position(pressedPosition.Row, pressedPosition.Column-1);
            if (p.IsValid() && _grid[pressedPosition.Row, pressedPosition.Column - 1] != player && _grid[pressedPosition.Row, pressedPosition.Column - 1] != 0)
            {
                for (j = pressedPosition.Column-2; j>=0;j--)
                    if (_grid[pressedPosition.Row, j] == player)
                    {
                        poz = j; break;
                    }
                Position position=new Position(pressedPosition.Row, poz);
                if (position.IsValid() && _grid[pressedPosition.Row, poz] == player)
                {
                    while (position.IsValid() && poz < pressedPosition.Column)
                    {
                        _grid[pressedPosition.Row, poz] = player;
                        poz++;
                    }
                }


            }
            //diag principala dreapta sus
            if (pressedPosition.Row > 0)
            {
                p = new Position(pressedPosition.Row - 1, pressedPosition.Column+1);
                if (p.IsValid() && _grid[pressedPosition.Row - 1, pressedPosition.Column + 1] != player && _grid[pressedPosition.Row - 1, pressedPosition.Column + 1] != 0)
                {
                    for (i = pressedPosition.Row - 2; i >= 0; i--)
                        for (j = pressedPosition.Column + 2; j < Columns; j++)
                            if (_grid[i, j] == player)
                            {
                                poz_row = i;
                                poz_col = j;
                                break;
                            }
                    Position position = new Position(poz_row, poz_col);
                    if (position.IsValid() && _grid[poz_row, poz_col] == player)
                    {
                        while (position.IsValid() && poz_row < pressedPosition.Row && poz_col > pressedPosition.Column)
                        {
                            _grid[poz_row, poz_col] = player;
                            poz_row++;
                            poz_col--;
                        }
                    }

                }
            }
            //diag principala dreapta jos
            p = new Position(pressedPosition.Row + 1, pressedPosition.Column+1);
            if (p.IsValid() && _grid[pressedPosition.Row+1,pressedPosition.Column+1]!=player && _grid[pressedPosition.Row + 1, pressedPosition.Column +1] != 0)
            {
                for(i=pressedPosition.Row+2;i<Rows;i++)
                    for(j=pressedPosition.Column+2;j<Columns;j++)
                        if (_grid[i,j]==player)
                        {
                            poz_row = i;
                            poz_col = j;
                            break;
                        }
                Position position = new Position(poz_row, poz_col);
                if (position.IsValid() && _grid[poz_row, poz_col] == player)
                {
                    while (position.IsValid() && poz_row > pressedPosition.Row && poz_col > pressedPosition.Column)
                    {
                        _grid[poz_row, poz_col] = player;
                        poz_row--;
                        poz_col--;
                    }

                }
            }

            //diag secundara stanga sus
            if (pressedPosition.Row > 0)
            {
                p = new Position(pressedPosition.Row - 1, pressedPosition.Column-1);
                if (p.IsValid() && _grid[pressedPosition.Row - 1, pressedPosition.Column - 1] != player && _grid[pressedPosition.Row - 1, pressedPosition.Column - 1] != 0)
                {
                    for (i = pressedPosition.Row - 2; i >= 0; i--)
                        for (j = pressedPosition.Column - 2; j >= 0; j--)
                            if (_grid[i, j] == player)
                            {
                                poz_row = i;
                                poz_col = j;
                                break;
                            }
                    Position position = new Position(poz_row, poz_col);
                    if (position.IsValid() && _grid[poz_row, poz_col] == player)
                    {
                        while (position.IsValid() && poz_row < pressedPosition.Row && poz_col < pressedPosition.Column)
                        {
                            _grid[poz_row, poz_col] = player;
                            poz_row++;
                            poz_col++;
                        }
                    }
                }
            }

            //diag secundara stanga jos
            p = new Position(pressedPosition.Row + 1, pressedPosition.Column-1);
            if (p.IsValid() && _grid[pressedPosition.Row+1,pressedPosition.Column-1]!=player && _grid[pressedPosition.Row + 1, pressedPosition.Column - 1] != 0)
            {
                for(i=pressedPosition.Row+2;i<Rows;i++)
                    for(j=pressedPosition.Column-2;j>=0;j--)
                        if(_grid[i,j]==player)
                        {
                            poz_row = i;
                            poz_col = j;
                            break;
                        }
                Position position=new Position(poz_row, poz_col);
                if (position.IsValid() && _grid[poz_row, poz_col] == player)
                {
                    while (position.IsValid() && poz_row > pressedPosition.Row && poz_col < pressedPosition.Column)
                    {
                        _grid[poz_row, poz_col] = player;
                        poz_row--;
                        poz_col++;
                    }
                }
            }




        }
    }
}
