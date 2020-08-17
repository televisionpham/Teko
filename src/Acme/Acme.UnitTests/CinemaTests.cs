using Acme.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Acme.UnitTests
{
    [TestClass]
    public class CinemaTests
    {
        private Cinema _cinema;

        public CinemaTests()
        {
            var rowsCount = 5;
            var colsCount = 5;
            _cinema = new Cinema(rowsCount, colsCount, 2);
            using (var sr = new StreamReader("seats.txt"))
            {
                var line = sr.ReadLine();
                var row = 0;
                while (line != null)
                {
                    var parts = line.Split(' ');
                    for (int i = 0; i < parts.Length; i++)
                    {
                        _cinema.Seats[row, i] = int.Parse(parts[i]);
                    }
                    line = sr.ReadLine();
                    row += 1;
                }
            }
        }

        [TestMethod]
        public void Can_find_regions()
        {                       
            var regions = _cinema.FindRegions();
            Assert.AreEqual(2, regions.Count);
            Assert.AreEqual(6, regions[0].Count);
            Assert.AreEqual(11, regions[1].Count);
        }

        [TestMethod]
        public void Can_free_seats()
        {
            var seats = new List<Seat>
            {
                new Seat(0, 3),
                new Seat(1,3),
                new Seat(2,2)
            };

            _cinema.FreeSeats(seats);
            Console.WriteLine(Util.PrintMatrix(_cinema.Seats));
            Assert.AreEqual(2, _cinema.AvailableSeats.Count);
            Assert.AreEqual(6, _cinema.AvailableSeats[0].Count);
            Assert.AreEqual(1, _cinema.AvailableSeats[1].Count);
        }
    }
}
