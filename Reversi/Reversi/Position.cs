using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public Position Apply(Offset offset)
        {
            var position=new Position(Row, Column); 
            position.Row += offset.RowOffset;
            position.Column+=offset.ColumnOffset;
            return position;
        }

        public bool IsValid()
        {
            return (Row>=0&&Row<8)&&(Column>=0&&Column<8);
        }
    }
}
