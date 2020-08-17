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
        public Seat(int x, int y)
        {
            Col = x;
            Row = y;
        }

        public int Col { get; set; }
        public int Row { get; set; }
    }
}
