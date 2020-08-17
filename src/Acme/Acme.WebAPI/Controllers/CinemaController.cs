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
        /// Lấy các vùng ghế có thể đặt chỗ cho số lượng ghế yêu cầu
        /// </summary>
        /// <param name="seatsCount">Số lượng ghế cần đặt</param>
        /// <returns>Danh sách tọa độ các ghế của các vùng ghế có thể đặt</returns>
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
        /// Đặt ghế
        /// </summary>
        /// <param name="seats">Danh sách các ghế muốn đặt</param>
        [HttpPost]
        [Route("api/Cinema/Reserve")]
        public void Reserve(List<Seat> seats)
        {
            Workspace.GetInstance().Cinema.Reserve(seats);
        }

        /// <summary>
        /// Hiển thị các ghế trong rạp dưới dạng ma trận phục vụ test 
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