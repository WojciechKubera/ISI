using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Isi_Backend.Models
{
    public class Statistics
    {
		[Key]
		public int ID { get; set; }

		public string Country { get; set; }

		public string CountryCode { get; set; }

		public string Slug { get; set; }

		public int NewConfirmed { get; set; }

		public int TotalConfirmed { get; set; }

		public int NewDeaths { get; set; }

		public int TotalDeaths { get; set; }

		public int NewRecovered { get; set; }

		public int TotalRecovered { get; set; }

		public string Wojewodztwo { get; set; }

		public int Liczba_przypadkow { get; set; }
		public int zgony_w_wyniku_covid_bez_chorob_wspolistniejacych { get; set; }
		public int zgony_w_wyniku_covid_i_chorob_wspolistniejacych { get; set; }
		public int liczba_zlecen_poz { get; set; }
		public int liczba_osob_objetych_kwarantanna { get; set; }
		public int liczba_wykonanych_testow { get; set; }
		public int liczba_testow_z_wynikiem_pozytywnym { get; set; }
		public int liczba_testow_z_wynikiem_negatywnym { get; set; }
		public int liczba_pozostalych_testow { get; set; }
		public string teryt { get; set; }
		public int stan_rekordu_na { get; set; }
		[Required]
		public DateTime Date { get; set; }

		public Statistics()
		{
			Country = null;
			CountryCode = null;
			Slug = null;
			Date = new DateTime(1900, 01, 01, 01, 01, 01);
			NewConfirmed = 0;
			NewDeaths = 0;
			NewRecovered = 0;
			TotalConfirmed = 0;
			TotalDeaths = 0;
			TotalRecovered = 0;
			Wojewodztwo = null;
			Liczba_przypadkow = 0;
			zgony_w_wyniku_covid_bez_chorob_wspolistniejacych = 0;
			zgony_w_wyniku_covid_i_chorob_wspolistniejacych = 0;
			liczba_zlecen_poz = 0;
			liczba_osob_objetych_kwarantanna = 0;
			liczba_wykonanych_testow = 0;
			liczba_testow_z_wynikiem_pozytywnym = 0;
			liczba_testow_z_wynikiem_negatywnym = 0;
			liczba_pozostalych_testow = 0;
			teryt = null;
			stan_rekordu_na = 0;
		}
	}

	
}

