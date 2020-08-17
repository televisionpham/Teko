using Acme.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.WebAPI
{
    public sealed class Workspace
    {
        private static Workspace _uniqueInstance;
        private static readonly object _lockObject = new object();

        private Workspace()
        {
            
        }

        public Cinema Cinema { get; private set; }

        public static Workspace GetInstance()
        {
            if (_uniqueInstance == null)
            {
                lock (_lockObject)
                {
                    if (_uniqueInstance == null)
                    {
                        _uniqueInstance = new Workspace();
                    }
                }
            }

            return _uniqueInstance;
        }

        /// <summary>
        /// Lấy tham số cho cinema từ file settings
        /// </summary>
        /// <param name="config"></param>
        public void LoadSettings(IConfiguration config)
        {
            var rowsCount = config.GetValue<int>("CinemaSettings:RowsCount");
            var colsCount = config.GetValue<int>("CinemaSettings:ColsCount");
            var minDistance = config.GetValue<int>("CinemaSettings:MinDistance");
            Cinema = new Cinema(rowsCount, colsCount, minDistance);
        }
    }
}
