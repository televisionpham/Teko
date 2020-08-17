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
        
        public static bool InBound(int row, int col, int colsCount, int rowsCount)
        {
            return (row >= 0 && col >= 0 && row < rowsCount && col < colsCount);
        }
        
        public static List<Seat> GetNextSeats(int row, int col)
        {
            var nextSeats = new List<Seat> {
                new Seat(row - 1, col),
                new Seat(row + 1, col),
                new Seat(row, col - 1),
                new Seat(row, col + 1)
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
