using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Company.Model
{
    public class JobPosition
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Staff> Staff {  get; set; }
    }
}
