using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSEAktarimConsole.ModelVM
{
   public class Izinler
    {
        public int ID { get; set; }
        public int SicilID { get; set; }
        public int TipID { get; set; }
        public System.DateTime Tarih { get; set; }
        public bool Saatlikizin { get; set; }
        public string Aciklama { get; set; }
        public int Sure { get; set; }
        public int Baslangic { get; set; }
        public int Bitis { get; set; }
        public bool Ucretli { get; set; }
        public int SaatlikUcret { get; set; }
        public int MailSended { get; set; }
        public Nullable<System.DateTime> BasTarih { get; set; }
        public Nullable<System.DateTime> BitTarih { get; set; }
        public string KarsiAktarimIdTcSicil { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> IseBaslamaTarih { get; set; }
    }
}
