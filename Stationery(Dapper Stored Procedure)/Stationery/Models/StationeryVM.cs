using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery.Models
{
    internal class StationeryVM : IHasId
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
    }
}
