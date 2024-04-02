using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_TicketMaster.Api.Model
{
    public class Call
    {
        public DateTime CallStart { get; set; }
        public DateTime CallPicked { get; set; }
        public DateTime CallEnd { get; set; }
    }
}
