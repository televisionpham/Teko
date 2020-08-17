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
            AvailableSeats.Add(new List<Seat>());
            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++)
                {
                    Seats[row, col] = 0;
                    AvailableSeats[0].Add(new Seat(row, col));
                }
            }
            
        }

        public int[,] Seats { get; }
        public List<List<Seat>> AvailableSeats { get; } = new List<List<Seat>>();

        /// <summary>
        /// Find seat regions
        /// </summary>
        /// <returns>List of seats region</returns>
        public List<List<Seat>> FindRegions()
        {
            var g = new int[_rowsCount, _colsCount];
            var regions = new List<List<Seat>>();
            var region = new List<Seat>();
            var queue = new Queue<Seat>();
            for (int row = 0; row < _rowsCount; row++)
            {
                for (int col = 0; col < _colsCount; col++)
                {
                    if (g[row,col] == 0 && Seats[row,col] == 0)
                    {
                        var p = new Seat(row, col);                        
                        g[row, col] = 1;
                        region.Add(p);
                        queue.Enqueue(p);
                        while (queue.Count > 0)
                        {
                            var s = queue.Dequeue();
                            var nextSeats = Util.GetNextSeats(s.Row, s.Col);
                            foreach (var item in nextSeats)
                            {
                                if (Util.InBound(item.Row, item.Col, _colsCount, _rowsCount) && g[item.Row, item.Col] == 0 && Seats[item.Row, item.Col] == 0)
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
        
        /// <summary>
        /// Reserve batch of seats
        /// </summary>
        /// <param name="seats">Seats list to be reserved</param>
        public void Reserve(List<Seat> seats)
        {
            foreach (var s in seats)
            {
                if (Seats[s.Row, s.Col] != 0)
                {
                    throw new Exception($"Unable to reserve seat ({s.Row}, {s.Col})");
                }
                
                Seats[s.Row, s.Col] = 1;
            }

            updateSeatsStatus();

            updateAvailableSeats();
        }

        /// <summary>
        /// Change all seats status to 0
        /// </summary>
        public void Clear()
        {
            AvailableSeats.Clear();
            AvailableSeats.Add(new List<Seat>());
            for (int row = 0; row < _rowsCount; row++)
            {
                for (int col = 0; col < _colsCount; col++)
                {
                    Seats[row, col] = 0;
                    AvailableSeats[0].Add(new Seat(row, col));
                }
            }            
        }

        /// <summary>
        /// Cancel seats
        /// </summary>
        /// <param name="seats">Seats to be cancelled</param>
        public void FreeSeats(List<Seat> seats)
        {
            foreach (var s in seats)
            {
                if (this.Seats[s.Row, s.Col] == 1)
                {
                    this.Seats[s.Row, s.Col] = 0;
                }
            }

            updateSeatsStatus();

            updateAvailableSeats();
        }

        private void updateAvailableSeats()
        {
            AvailableSeats.Clear();
            AvailableSeats.AddRange(FindRegions());
        }

        /// <summary>
        /// Update seats status
        /// </summary>
        private void updateSeatsStatus()
        {
            var edge = new List<Seat>();
            var emptySeats = new List<Seat>();

            for (int row = 0; row < _rowsCount; row++)
            {
                for (int col = 0; col < _colsCount; col++)
                {
                    if (Seats[row, col] != 1)
                    {
                        Seats[row, col] = 0;
                        emptySeats.Add(new Seat(row, col));
                    }
                    else
                    {
                        var nextSeats = Util.GetNextSeats(row, col);
                        foreach (var s in nextSeats)
                        {
                            if (Util.InBound(s.Row, s.Col, _colsCount, _rowsCount) && Seats[s.Row, s.Col] != 1)
                            {
                                edge.Add(new Seat(row, col));
                                break;
                            }
                        }
                    }
                }
            }

            foreach (var rs in edge)
            {
                foreach (var es in emptySeats)
                {
                    var d = Util.ManhattanDistance(rs.Col, rs.Row, es.Col, es.Row);
                    if (d <= _minDistance)
                    {
                        Seats[es.Row, es.Col] = -1;
                    }
                }
            }            
        }
    }
}
