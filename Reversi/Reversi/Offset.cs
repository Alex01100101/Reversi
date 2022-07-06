using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    public class Offset
    {
        public int RowOffset { get; set; }
        public int ColumnOffset { get; set; }
        public Offset(int rowOffset, int columnOffset)
        {
            RowOffset = rowOffset;
            ColumnOffset = columnOffset;
        }
    }
}
