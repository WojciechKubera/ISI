using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isi_Backend.Controllers
{
    public class StatisticsDeserializecs
    {
        public string ID { get; set; }
        public string Message { get; set; }
        public  Global global { get; set; }
        public List<Countries> countries { get; set; }
        public DateTime Date { get; set; }

    }
    public class Global
    {
        public string ID { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public DateTime Date { get; set; }
    }

    public class Countries
    {
        public string ID { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public DateTime Date { get; set; }
    }
}
