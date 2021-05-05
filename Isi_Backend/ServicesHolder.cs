using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isi_Backend
{
    public class ServicesHolder
    {
        public static IServiceProvider serviceProvider { get; set; }
    }
}
