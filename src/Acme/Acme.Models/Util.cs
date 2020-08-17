using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Acme.Models
{
    public static class Util
    {
        public static int ManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
        
        public static bool InBound(int x, int y, int colsCount, int rowsCount)
        {
            return (x >= 0 && y >= 0 && x < colsCount && y < rowsCount);
        }
        
        public static List<Seat> GetNextSeats(int x, int y)
        {
            var nextSeats = new List<Seat> {
                new Seat(x - 1, y),
                new Seat(x + 1, y),
                new Seat(x, y - 1),
                new Seat(x, y + 1)
            };            
            return nextSeats;
        }        

        public static string PrintMatrix(int[,] m)
        {
            var sb = new StringBuilder();
            var rowLength = m.GetLength(0);
            var colLength = m.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    sb.Append($"{m[i, j]}\t");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
