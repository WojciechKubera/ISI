﻿using System;
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

        // GET: Statistics
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
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

            var statistics = await _context.Statistics
                .FirstOrDefaultAsync(m => m.Country == country || m.CountryCode == country);
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
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
               
            WebClient webClient = new WebClient();
          
            webClient.DownloadFile("https://www.arcgis.com/sharing/rest/content/items/153a138859bb4c418156642b5b74925b/data", startupPath);

            return Ok(); ;
        }
    }
      
}
