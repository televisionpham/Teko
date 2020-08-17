using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.Models
{
    public class Seat
    {
        public Seat()
        {

        }
        public Seat(int row, int col)
        {
            Col = col;
            Row = row;
        }

        public int Col { get; set; }
        public int Row { get; set; }
    }
}
