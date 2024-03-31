using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery.Models
{
    internal class TypeVM : IHasId
    {
        public int Id {  get; set; }
        public string Title { get; set; }
    }
}
