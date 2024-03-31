using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Stationery.Models
{
    internal class ManagerSales
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type {  get; set; }
        public double Cost { get; set; }
        public string Manager {  get; set; }
        public int Sold { get; set; }
    }
}
