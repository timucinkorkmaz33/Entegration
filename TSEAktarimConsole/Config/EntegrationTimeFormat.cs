using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSEAktarimConsole.Config
{
    public class EntegrationTimeFormat
    {
        public EntegrationTimeFormat(string val)
        {
            string[] valsplit = val.Split(':');
            this.WorkingHour = int.Parse(valsplit[0]);
            this.WorkingMinute = int.Parse(valsplit[1]);
        }
        public int WorkingHour { get; set; }
        public int WorkingMinute { get; set; }
    }
}
