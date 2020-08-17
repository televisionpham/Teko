using Acme.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Acme.UnitTests
{
    [TestClass]
    public class CinemaTests
    {
        [TestMethod]
        public void Can_find_regions()
        {
            var rowsCount = 5;
            var colsCount = 5;
            var cinema = new Cinema(rowsCount, colsCount, 3);
            using (var sr = new StreamReader("seats.txt"))
            {
                var line = sr.ReadLine();
                var row = 0;
                while (line != null)
                {
                    var parts = line.Split(' ');
                    for (int i = 0; i < parts.Length; i++)
                    {
                        cinema.Seats[row, i] = int.Parse(parts[i]);
                    }
                    line = sr.ReadLine();
                    row += 1;
                }
            }
            Util.PrintMatrix(cinema.Seats);
            var regions = cinema.FindRegions();
            Assert.AreEqual(2, regions.Count);
            Assert.AreEqual(6, regions[0].Count);
            Assert.AreEqual(11, regions[1].Count);
        }
    }
}
