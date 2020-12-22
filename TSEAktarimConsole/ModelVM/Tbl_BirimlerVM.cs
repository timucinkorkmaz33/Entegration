using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSEAktarimConsole.ModelVM
{
    public class Tbl_BirimlerVM
    {
        public int Id { get; set; }
        public Nullable<int> BirimKodu { get; set; }
        public string BirimAdi { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> Salon { get; set; }
        public Nullable<int> Mutfak { get; set; }
        public Nullable<int> Kasiyer { get; set; }
        public Nullable<int> Ekstra { get; set; }
        public Nullable<int> SalonId { get; set; }
        public Nullable<int> MutfakId { get; set; }
        public Nullable<int> KasiyerId { get; set; }
        public Nullable<int> EkstraId { get; set; }
        public string CihazID { get; set; }
    }
}
