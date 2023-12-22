using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Models
{
    public class Grade : Common
    {
        public Student Student { get; set; }
        public Class Class { get; set; }
        public double Value { get; set; }
    }
}
