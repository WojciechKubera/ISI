using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Isi_Backend.Data;
using Isi_Backend.Models;
using System.Net;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Newtonsoft.Json.Linq;
using CsvHelper;
using System.Globalization;
using System.Data;
using CsvHelper.Configuration;

namespace Isi_Backend.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private readonly Isi_BackendContext _context;

        public StatisticsController(Isi_BackendContext context)
        {
            _context = context;
        }
        public static readonly string[] POLISH_LOCALIZATIONS = {
    "caly-kraj", "dolnoslaskie", "kujawsko-pomorskie",
    "lubelskie", "lubuskie", "lodzkie", "malopolskie",
    "mazowieckie", "opolskie", "podkarpackie", "podlaskie",
    "pomorskie", "slaskie", "swietokrzyskie",
    "warminsko-mazurskie", "wielkopolskie", "zachodniopomorskie" };
        // GET: Statistics
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            var statistics =  _context.Statistics;
            //return View(await _context.Statistics.ToListAsync());
            return Ok(JsonConvert.SerializeObject(statistics));
        }

        // GET: Statistics/Details/5
        [HttpGet("{country}")]
        [Route("Details/{country}")]
        public async Task<IActionResult> Details(string? country)
        {
            if (country == null)
            {
                return NotFound();
            }

            var statistics = _context.Statistics.Where(m => m.Country == country || m.CountryCode == country).ToList();
                //.FirstOrDefaultAsync(m => m.Country == country || m.CountryCode == country);
            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(statistics));
        }

        // GET: Statistics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Statistics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Country,NewConfirmed,TotalConfirmed,NewDeaths,TotalDeaths,NewRecovered,TotalRecovered,Wojewodztwo,Liczba_przypadkow,zgony_w_wyniku_covid_bez_chorob_wspolistniejacych,zgony_w_wyniku_covid_i_chorob_wspolistniejacych,liczba_zlecen_poz,liczba_osob_objetych_kwarantanna,iczba_wykonanych_testow,liczba_testow_z_wynikiem_pozytywnym,liczba_testow_z_wynikiem_negatywnym,liczba_pozostalych_testow,teryt,stan_rekordu_na,Date")] Statistics statistics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statistics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statistics);
        }

        // GET: Statistics/Edit/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics.FindAsync(id);
            if (statistics == null)
            {
                return NotFound();
            }
            return View(statistics);
        }

        // POST: Statistics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Country,NewConfirmed,TotalConfirmed,NewDeaths,TotalDeaths,NewRecovered,TotalRecovered,Wojewodztwo,Liczba_przypadkow,zgony_w_wyniku_covid_bez_chorob_wspolistniejacych,zgony_w_wyniku_covid_i_chorob_wspolistniejacych,liczba_zlecen_poz,liczba_osob_objetych_kwarantanna,iczba_wykonanych_testow,liczba_testow_z_wynikiem_pozytywnym,liczba_testow_z_wynikiem_negatywnym,liczba_pozostalych_testow,teryt,stan_rekordu_na,Date")] Statistics statistics)
        {
          
            if (id != statistics.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatisticsExists(statistics.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(statistics);
        }

        // GET: Statistics/Delete/5
           [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics
                .FirstOrDefaultAsync(m => m.ID == id);
            if (statistics == null)
            {
                return NotFound();
            }

            return View(statistics);
        }

        // POST: Statistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statistics = await _context.Statistics.FindAsync(id);
            _context.Statistics.Remove(statistics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatisticsExists(int id)
        {
            return _context.Statistics.Any(e => e.ID == id);
        }

        [HttpGet]
        [Route("DownloadStatistics")]
        public async Task<IActionResult> DownloadStatistics()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://api.covid19api.com/summary"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

           
            
            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())   //modified from your code since the using statement disposes the stream automatically when done
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            var json = JObject.Parse(jsonString);
            StatisticsDeserializecs statisticsDeserializecs = JsonConvert.DeserializeObject<StatisticsDeserializecs>(jsonString);
            Statistics stat = new Statistics();

            stat.Country = "World";
            stat.Date = statisticsDeserializecs.global.Date;
            stat.NewConfirmed = statisticsDeserializecs.global.NewConfirmed;
            stat.NewDeaths = statisticsDeserializecs.global.NewDeaths;
            stat.NewRecovered = statisticsDeserializecs.global.NewRecovered;
            stat.TotalConfirmed = statisticsDeserializecs.global.TotalConfirmed;
            stat.TotalDeaths = statisticsDeserializecs.global.TotalDeaths;
            stat.TotalRecovered = statisticsDeserializecs.global.TotalRecovered;
          
            if (ModelState.IsValid)
            {
                _context.Add(stat);
                _context.SaveChanges();

            }

            foreach (var item in statisticsDeserializecs.countries)
            {
                var t = new Statistics();

                t.Country = item.Country;
                t.Slug = item.Slug;
                t.CountryCode = item.CountryCode;
                t.Date = item.Date;
                t.NewConfirmed = item.NewConfirmed;
                t.NewDeaths = item.NewDeaths;
                t.NewRecovered = item.NewRecovered;
                t.TotalConfirmed = item.TotalConfirmed;
                t.TotalDeaths = item.TotalDeaths;
                t.TotalRecovered = item.TotalRecovered;
               

                if (ModelState.IsValid)
                {
                    _context.Add(t);
                    _context.SaveChanges();

                }


            }
        

            return Ok(); ;
        }

        [HttpGet]
        [Route("DownloadCsv")]
        public async Task<IActionResult> DownloadCsv()
        {
            


        string startupPath = Path.Combine(Environment.CurrentDirectory,"csv", "akutalne_dane_wojewodztwa.csv");


            WebClient webClient = new WebClient();

            webClient.DownloadFile("https://www.arcgis.com/sharing/rest/content/items/153a138859bb4c418156642b5b74925b/data", startupPath);
            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            using (var reader = new StreamReader(startupPath))
            using (var csv = new CsvReader(reader,config))
            {
               
                // Do any configuration to `CsvReader` before creating CsvDataReader.
                using (var dr = new CsvDataReader(csv))
                {
                    var dt = new DataTable();
                    dt.Load(dr);
                    var test = dt.Rows[1].ItemArray[14].ToString();
                    //var date = DateTime.Parse(test);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var t = new Statistics();

                        t.Country = "Poland";
                        t.CountryCode = "PL";
                        t.Wojewodztwo = POLISH_LOCALIZATIONS[i];
                        t.Liczba_przypadkow = int.Parse(dt.Rows[i].ItemArray[1].ToString());
                        t.NewDeaths = int.Parse(dt.Rows[i].ItemArray[3].ToString());
                        t.zgony_w_wyniku_covid_bez_chorob_wspolistniejacych = int.Parse(dt.Rows[i].ItemArray[4].ToString());
                        t.zgony_w_wyniku_covid_i_chorob_wspolistniejacych = int.Parse(dt.Rows[i].ItemArray[5].ToString());
                        t.liczba_zlecen_poz = int.Parse(dt.Rows[i].ItemArray[6].ToString());
                        t.NewRecovered = t.liczba_zlecen_poz = int.Parse(dt.Rows[i].ItemArray[7].ToString());
                        t.liczba_osob_objetych_kwarantanna = int.Parse(dt.Rows[i].ItemArray[8].ToString());
                        t.liczba_wykonanych_testow = int.Parse(dt.Rows[i].ItemArray[9].ToString());
                        t.liczba_testow_z_wynikiem_pozytywnym = int.Parse(dt.Rows[i].ItemArray[10].ToString());
                        t.liczba_testow_z_wynikiem_negatywnym = int.Parse(dt.Rows[i].ItemArray[11].ToString());
                        t.liczba_pozostalych_testow = int.Parse(dt.Rows[i].ItemArray[12].ToString());
                        t.teryt = dt.Rows[i].ItemArray[13].ToString();
                        t.Date = DateTime.Parse(dt.Rows[i].ItemArray[14].ToString());

                        if (ModelState.IsValid)
                        {
                            _context.Add(t);
                            _context.SaveChanges();

                        }


                    }
                }
            }


            return Ok(); ;
        }
    }
      
}
