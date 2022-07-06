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
        public void Apply(Offset offset)
        {
            Row += offset.RowOffset;
            Column+=offset.ColumnOffset;
        }

        public bool IsValid()
        {
            return (Row>0&&Row<=8)&&(Column>0&&Column<=8);
        }
    }
}
