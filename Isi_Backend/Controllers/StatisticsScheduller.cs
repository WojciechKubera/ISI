using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isi_Backend.Controllers;
using Isi_Backend.Data;
using Isi_Backend.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Isi_Backend.Controllers
{
    public class StatisticsScheduller : Controller
    {
        private readonly Isi_BackendContext _context;

        public StatisticsScheduller()
        {
            var serviceProvider = (ServiceProvider)ServicesHolder.serviceProvider;
            _context = (Isi_BackendContext)serviceProvider.GetService(typeof(Isi_BackendContext));
        }

        private static int refreshTime = 3600000; // hour in miliseconds

        public void run ()
        {
            var statisticsController = new StatisticsController(_context);
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMilliseconds(refreshTime);

            var timer = new System.Threading.Timer((e) =>
            {
                _ = statisticsController.DownloadStatistics();
                _ = statisticsController.DownloadCsv();
            }, null, startTimeSpan, periodTimeSpan);
        }

       
    }
}
