using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Isi_Backend.Models;

namespace Isi_Backend.Data
{
    public class Isi_BackendContext : DbContext
    {

        public Isi_BackendContext()
        {
        }
        public Isi_BackendContext (DbContextOptions<Isi_BackendContext> options)
            : base(options)
        {
        }

        public DbSet<Isi_Backend.Models.Statistics> Statistics { get; set; }
    }
}
