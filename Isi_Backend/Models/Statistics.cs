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
		public int iczba_wykonanych_testow { get; set; }
		public int liczba_testow_z_wynikiem_pozytywnym { get; set; }
		public int liczba_testow_z_wynikiem_negatywnym { get; set; }
		public int liczba_pozostalych_testow { get; set; }
		public int teryt { get; set; }
		public int stan_rekordu_na { get; set; }


		[Required]
		public DateTime Date { get; set; }
	}
}

