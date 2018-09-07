using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreLogging.Models
{
    public class LeakageStatistics
    {
        public int Occurences { get; set; }
        public string LeakageRange { get; set; }
        public int LookUpId { get; set; }
        public string LookUpKey { get; set; }
        public Decimal MTTR { get; set; }
    }
}
