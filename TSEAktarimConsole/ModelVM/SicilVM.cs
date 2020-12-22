using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSEAktarimConsole.ModelVM
{
   public class SicilVM
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> Firma { get; set; }
        public Nullable<int> TerminalGrubu { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string PersonelNo { get; set; }
        public Nullable<System.DateTime> GirisTarih { get; set; }
        public Nullable<System.DateTime> CikisTarih { get; set; }
        public string SicilNo { get; set; }
        public Nullable<int> Pozisyon { get; set; }
        public Nullable<int> Bolum { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string CepTelefon { get; set; }
        public string Adres { get; set; }
        public string IL { get; set; }
        public string Ilce { get; set; }
        public Nullable<int> KanGrubu { get; set; }
        public byte[] FotoImage { get; set; }
        public string Bilgi { get; set; }
        public int MesaiPeriyodu { get; set; }
        public Nullable<System.DateTime> PeriyodBaslangici { get; set; }
        public bool SonDurum { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public bool FazlaMesai { get; set; }
        public bool EksikMesai { get; set; }
        public bool EksikMesai_FM { get; set; }
        public bool ErkenMesai { get; set; }
        public bool EksikGun { get; set; }
        public int MaasTipi { get; set; }
        public int Maas { get; set; }
        public float AylikCalismaSaati { get; set; }
        public int SonTasnifID { get; set; }
        public Nullable<byte> SicilKilit { get; set; }
        public Nullable<System.DateTime> DogumTarih { get; set; }
        public string OKod1 { get; set; }
        public string OKod2 { get; set; }
        public string OKod3 { get; set; }
        public string OKod4 { get; set; }
        public string OKod5 { get; set; }
        public string OKod6 { get; set; }
        public string OKod7 { get; set; }
        public string OKod8 { get; set; }
        public string OKod9 { get; set; }
        public string OKod10 { get; set; }
        public bool GeceZammi { get; set; }
        public bool FM_EM { get; set; }
        public Nullable<int> Gorev { get; set; }
        public Nullable<System.DateTime> bitistarih { get; set; }
        public Nullable<int> AltFirma { get; set; }
        public Nullable<int> Cinsiyet { get; set; }
        public string EMail { get; set; }
        public Nullable<int> Direktorluk { get; set; }
        public Nullable<int> Yaka { get; set; }
        public Nullable<int> Puantaj { get; set; }
        public Nullable<System.DateTime> KidemTarih { get; set; }
        public Nullable<int> BirimId { get; set; }
        public string PictureId { get; set; }
        public Nullable<bool> ZiyaretciKabulDurum { get; set; }
        public Nullable<int> GorevId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<int> AmirId { get; set; }
        public string AmirAdi { get; set; }
        public string GlobalSicilID { get; set; }
    }
}
