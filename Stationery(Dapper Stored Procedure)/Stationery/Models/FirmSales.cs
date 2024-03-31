using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery.Models
{
    internal class FirmSales
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double Cost { get; set; }
        public string Firm{ get; set; }
        public int Sold { get; set; }
    }
}
