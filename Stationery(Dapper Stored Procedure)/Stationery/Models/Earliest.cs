using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery.Models
{
    internal class Earliest
    {
        public int Id { get; set; }
        public string Stationery { get; set; }
        public string Firm { get; set; }
        public string Manager { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnitSold { get; set; }
        public DateTime DateOfSale { get; set; }
    }
}
