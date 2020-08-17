using System;
using System.Collections.Generic;
using System.Drawing;

namespace Acme.Models
{
    public class Cinema
    {
        private int _rowsCount;
        private int _colsCount;
        private int _minDistance;

        public Cinema(int rowsCount, int colsCount, int minDistance)
        {
            if (rowsCount < 1 || colsCount < 1)
            {
                throw new ArgumentException("Invalid parameter");
            }
            _rowsCount = rowsCount;
            _colsCount = colsCount;
            _minDistance = minDistance;
            Seats = new int[rowsCount, colsCount];
            for (int y = 0; y < rowsCount; y++)
            {
                for (int x = 0; x < colsCount; x++)
                {
                    Seats[x, y] = 0;
                }
            }

            AvailableSeats.AddRange(FindRegions());
        }

        public int[,] Seats { get; }
        public List<List<Seat>> AvailableSeats { get; } = new List<List<Seat>>();

        /// <summary>
        /// Tìm các vùng ghế còn trống sao cho các ghế cạnh nhau
        /// </summary>
        /// <returns>Danh sách tọa độ các ghế của các vùng</returns>
        public List<List<Seat>> FindRegions()
        {
            var g = new int[_rowsCount, _colsCount];
            var regions = new List<List<Seat>>();
            var region = new List<Seat>();
            var queue = new Queue<Seat>();
            for (int y = 0; y < _rowsCount; y++)
            {
                for (int x = 0; x < _colsCount; x++)
                {
                    if (g[y,x] == 0 && Seats[y,x] == 0)
                    {
                        var p = new Seat(x, y);                        
                        g[y, x] = 1;
                        region.Add(p);
                        queue.Enqueue(p);
                        while (queue.Count > 0)
                        {
                            var s = queue.Dequeue();
                            var nextSeats = Util.GetNextSeats(s.Col, s.Row);
                            foreach (var item in nextSeats)
                            {
                                if (Util.InBound(item.Col, item.Row, _colsCount, _rowsCount) && g[item.Row, item.Col] == 0 && Seats[item.Row, item.Col] == 0)
                                {
                                    g[item.Row, item.Col] = 1;
                                    queue.Enqueue(item);
                                    region.Add(item);
                                }
                            }
                        }

                        regions.Add(region);
                        region = new List<Seat>();
                    }
                }
            }

            return regions;
        } 
        
        public void Reserve(List<Seat> seats)
        {
            foreach (var s in seats)
            {
                if (Seats[s.Row, s.Col] != 0)
                {
                    throw new Exception($"Không thể đặt ghế ({s.Row}, {s.Col})");
                }
                
                Seats[s.Row, s.Col] = 1;
            }

            for (int i = 0; i < _rowsCount; i++)
            {
                for (int j = 0; j < _colsCount; j++)
                {
                    if (Seats[i, j] == 0)
                    {
                        foreach (var s in seats)
                        {
                            if (Util.ManhattanDistance(i, j, s.Col, s.Row) <= _minDistance)
                            {
                                Seats[i, j] = -1;
                            }
                        }
                    }
                }
            }

            AvailableSeats.Clear();
            AvailableSeats.AddRange(FindRegions());
        }
    }
}
