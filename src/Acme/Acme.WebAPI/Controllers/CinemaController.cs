using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Acme.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acme.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {        
        /// <summary>
        /// Get available seats region for a given number
        /// </summary>
        /// <param name="seatsCount"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Cinema/GetAvailableSeats")]
        public List<List<Seat>> GetAvailableSeats(int seatsCount)
        {
            var sets = new List<List<Seat>>();
            foreach (var region in Workspace.GetInstance().Cinema.AvailableSeats)
            {
                if (region.Count >= seatsCount)
                {
                    sets.Add(region);
                }
            }
            return sets;
        }

        /// <summary>
        /// Reserve seats
        /// </summary>
        /// <param name="seats"></param>
        [HttpPost]
        [Route("api/Cinema/Reserve")]
        public void Reserve(List<Seat> seats)
        {
            Workspace.GetInstance().Cinema.Reserve(seats);
        }

        /// <summary>
        /// Clear all seats
        /// </summary>
        [HttpPost]
        [Route("api/Cinema/Clear")]
        public void Clear()
        {
            Workspace.GetInstance().Cinema.Clear();
        }

        /// <summary>
        /// Cancel reserved seats
        /// </summary>
        /// <param name="seats"></param>
        [HttpPost]
        [Route("api/Cinema/FreeSeats")]
        public void FreeSeats(List<Seat> seats)
        {
            Workspace.GetInstance().Cinema.FreeSeats(seats);
        }

        /// <summary>
        /// Display seats as a 2D matrix (for testing purpose)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Cinema/PrintCinema")]
        public string PrintCinema()
        {
            return Util.PrintMatrix(Workspace.GetInstance().Cinema.Seats);            
        }
    }
}