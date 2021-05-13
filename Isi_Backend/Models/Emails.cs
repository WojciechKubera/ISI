using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Isi_Backend.Models
{
    public class Emails
    {
        [Key]
        public string email { get; set; }
        public int condition { get; set; }
        public string countryCondition { get; set; }


    }
}
