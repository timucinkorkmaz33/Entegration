using appCS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSEAktarimConsole.Model;
using TSEAktarimConsole.ModelVM;
using TSEAktarimConsole.RefreshTime;
using TSEAktarimConsole.TSEService;


namespace TSEAktarimConsole
{
    public class Program
    {

        public static TSEEntities ent = new TSEEntities();
        public static ikbsSoapClient br = new ikbsSoapClient();
        static ConfigValues rfr;
        static void Main(string[] args)
        {

            while (true)
            {
                rfr = new ConfigValues();
                DateTime t = DateTime.Now;
                if (ConfigValues.ReCalculate() == "1" || ((t.Hour == rfr.BirimRefreshTime.WorkingHour && t.Minute == rfr.BirimRefreshTime.WorkingMinute) && rfr.BirimRefreshEnabled))
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Birim Güncelleme Baþlatýldý...");
                    BirimListesi();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Birim Güncelleme Tamamlandý...");

                }
                if (ConfigValues.ReCalculate() == "1" || (t.Hour == rfr.SicilRefreshTime.WorkingHour && t.Minute == rfr.SicilRefreshTime.WorkingMinute) && rfr.SicilRefreshEnabled)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Personel Güncelleme Baþlatýldý...");
                    PersonelListesi();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Personel Güncelleme Tamamlandý...");
                }
                if (ConfigValues.ReCalculate() == "1" || (t.Hour == rfr.SicilIzinRefreshTime.WorkingHour && t.Minute == rfr.SicilIzinRefreshTime.WorkingMinute) && rfr.SicilIzinRefreshEnabled)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Izin Güncelleme Baþlatýldý...");
                    PersonelIzinListesi();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Izin Güncelleme Tamamlandý...");
                }
                if (ConfigValues.ReCalculate() == "1")
                {
                    ConfigValues.SetReCalculateStatusAsCompeleted();
                }
            }

        }

        public static string BirimListesi()
        {
            int kalan = 0;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            var liste = br.BirimListesi();
            //var anabirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == "TSE").FirstOrDefault();
            //if (anabirim == null)
            //{
            //    Tbl_Birimler tbrmler = new Tbl_Birimler();
            //    tbrmler.BirimAdi = "TSE";
            //    tbrmler.BirimKodu = NextBirimID();
            //    tbrmler.ParentId = 0;
            //    tbrmler.CihazID = "anabirim";
            //    ent.Tbl_Birimler.Add(tbrmler);
            //    ent.SaveChanges();
            //}83dfb282-2841-4226-8888-5e0bd256d8aa

            var sorgula = ent.Tbl_Birimler.Select(u => u).Count();
            if (sorgula == 0)
            {
                foreach (System.Data.DataRow brm in liste.Tables[0].Rows)
                {
                    Console.WriteLine("Kalan Birim Sayýsý=" + (liste.Tables[0].Rows.Count - kalan) + "  -  " + brm[3].ToString());
                    kalan++;
                    var birimadi = brm[3].ToString();
                    var cihazid = brm[0].ToString();
                    var cihazustid = brm[1].ToString();

                    Tbl_Birimler tbrm = new Tbl_Birimler();
                    tbrm.BirimAdi = birimadi;
                    tbrm.BirimKodu = NextBirimID();
                    tbrm.ParentId = 0;
                    tbrm.CihazID = cihazid;
                    tbrm.CihazUstBirimId = cihazustid;
                    ent.Tbl_Birimler.Add(tbrm);
                    ent.SaveChanges();
                }
            }

            foreach (System.Data.DataRow brm in liste.Tables[0].Rows)
            {

                Console.WriteLine("Kalan Birim Sayýsý=" + (liste.Tables[0].Rows.Count - kalan) + "  -  " + brm[3].ToString());
                kalan++;
                var birimadi = brm[3].ToString();
                var cihazid = brm[0].ToString();
                var cihazustid = brm[1].ToString();
                var birim = ent.Tbl_Birimler.Where(u => u.BirimAdi == birimadi && u.CihazID == cihazid).FirstOrDefault();//birimin olup olmadýðý kontrol ediliyor
                if (birim == null)
                {
                    Tbl_Birimler tbrm = new Tbl_Birimler();
                    tbrm.BirimAdi = birimadi;
                    tbrm.BirimKodu = NextBirimID();
                    tbrm.ParentId = 0;
                    tbrm.CihazID = cihazid;
                    tbrm.CihazUstBirimId = cihazustid;
                    ent.Tbl_Birimler.Add(tbrm);
                    ent.SaveChanges();

                    ///update yapýlmalý
                    var tseparentid = brm[1];

                    var ustbirim = liste.Tables[0].Select("BirimUN=" + "'" + tseparentid + "'").FirstOrDefault();
                    if (ustbirim != null && tseparentid.ToString() != "")
                    {

                        var ustbirimad = ustbirim[3].ToString();
                        var ustbirimcihazid = ustbirim[0].ToString();
                        var ustbirimcihazustid = ustbirim[1].ToString();

                        var birimvarmi = ent.Tbl_Birimler.Where(u => u.BirimAdi == ustbirimad && u.CihazID == ustbirimcihazid).FirstOrDefault();

                        if (birimvarmi == null)
                        {
                            var ustbirimadi = ustbirim[3].ToString();
                            Tbl_Birimler tbrm1 = new Tbl_Birimler();
                            tbrm1.BirimAdi = ustbirim[3].ToString();
                            tbrm1.BirimKodu = ent.Tbl_Birimler.Select(x => x.Id).Max() + 1;
                            tbrm1.ParentId = 0;
                            tbrm1.CihazID = ustbirimcihazid;
                            tbrm1.CihazUstBirimId = ustbirimcihazustid;
                            ent.Tbl_Birimler.Add(tbrm1);
                            ent.SaveChanges();
                            var eklenenustbirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == ustbirimadi).FirstOrDefault();
                            var eklenenbirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == birimadi).FirstOrDefault();

                            eklenenbirim.ParentId = eklenenustbirim.BirimKodu;
                            ent.SaveChanges();
                        }
                        else
                        {
                            var bulunanustbirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == birimvarmi.BirimAdi && u.CihazID == birimvarmi.CihazID).FirstOrDefault();
                            var eklenenbirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == birimadi && u.CihazID == cihazid).FirstOrDefault();

                            eklenenbirim.ParentId = bulunanustbirim.BirimKodu;
                            ent.SaveChanges();
                        }

                    }
                    else
                    {
                        if (tseparentid.ToString() != "")
                        {
                            var ustbirimad = ustbirim[3].ToString();
                            var eklenenbirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == birimadi).FirstOrDefault();
                            var bulunanustbirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == ustbirimad).FirstOrDefault();
                            eklenenbirim.ParentId = bulunanustbirim.BirimKodu;
                            ent.SaveChanges();
                        }
                    }

                }
                else
                {
                    Tbl_Birimler tbrm = ent.Tbl_Birimler.Where(u => u.Id == birim.Id).FirstOrDefault();
                    tbrm.BirimAdi = birim.BirimAdi;
                    tbrm.BirimKodu = birim.BirimKodu;
                    ent.SaveChanges();

                    ///Parent deðiþtimi
                    var ustbirim = ent.Tbl_Birimler.Where(u => u.CihazID == tbrm.CihazUstBirimId).FirstOrDefault();
                    //if (ustbirim == null)
                    //{
                    //    var ustbirimadi = ustbirim[3].ToString();
                    //    Tbl_Birimler tbrm1 = new Tbl_Birimler();
                    //    tbrm1.BirimAdi = ustbirim[3].ToString();
                    //    tbrm1.BirimKodu = ent.Tbl_Birimler.Select(x => x.Id).Max() + 1;
                    //    tbrm1.ParentId = 0;
                    //    ent.Tbl_Birimler.Add(tbrm1);
                    //    ent.SaveChanges();
                    //    var eklenenustbirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == ustbirimadi).FirstOrDefault();
                    //    tbrm.ParentId = eklenenustbirim.BirimKodu;
                    //    ent.SaveChanges();
                    //}

                    {
                        //var ustbirimad = ustbirim[3].ToString();
                        // var cihaz = ustbirim[0].ToString();
                        //   var bulunanustbirim = ent.Tbl_Birimler.Where(u => u.BirimAdi == ustbirimad && u.CihazID==cihaz).FirstOrDefault();
                        if (ustbirim == null) { tbrm.ParentId = 0; }
                        else
                        {
                            tbrm.ParentId = ustbirim.BirimKodu;
                        }
                        ent.SaveChanges();

                    }


                }

            }

            //usr.CardID=a.Tables[0].Rows


            return "1";

        }

        public static int NextBirimID()
        {
            if (ent.Tbl_Birimler.Select(x => x.Id).FirstOrDefault() != 0)
            {
                var birimid = ent.Tbl_Birimler.Select(x => x.Id).Max();
                if (birimid == 0)
                    return 1;
                else
                {
                    return Convert.ToInt32(birimid.ToString()) + 1;
                }
            }
            else { return 1; }
        }

        public static string PersonelListesi()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            var kalan = 0;
            var pList = br.PersonelListesiPDKS();
            UserListVM usr = new UserListVM();
            foreach (System.Data.DataRow per in pList.Tables[0].Rows)
            {
                var tc = per[1].ToString();
                var isim = per[2].ToString();
                var soyisim = per[3].ToString();
                var sicil = per[5].ToString();
                var birimid = per[38].ToString();
                var PerSicilId = per[0].ToString();
                Console.WriteLine("Kalan Personel Sayýsý=" + (pList.Tables[0].Rows.Count - kalan) + "  -  " + isim + " " + soyisim);
                kalan++;
                var percontrol = ent.Sicil.Where(u => u.SicilNo == sicil).FirstOrDefault();
                if (percontrol == null)
                {
                    UserList ul = new UserList();
                    ul.ID = NextUserlistID();
                    ul.UserID = CharacterFixer.dynamicfix(ul.ID.ToString(), 8);
                    ul.CardType = 0;
                    ul.CardAttribute = 5;
                    ul.FacilityCode = "00000000";
                    ul.UserDef = 1;
                    ul.Master = 0;
                    ul.BypassCard = 1;
                    ul.IsTimezone = 0;
                    ent.UserList.Add(ul);
                    ent.SaveChanges();

                    var sicilbirimid = ent.Tbl_Birimler.Where(u => u.CihazID == birimid).FirstOrDefault().Id;

                    Sicil scl = new Sicil();
                    scl.ID = NextSicilID();
                    scl.UserID = ul.UserID;
                    scl.Ad = isim;
                    scl.Soyad = soyisim;
                    scl.MesaiPeriyodu = 1;
                    scl.SonDurum = false;
                    scl.FazlaMesai = false;
                    scl.EksikMesai = false;
                    scl.EksikMesai_FM = false;
                    scl.ErkenMesai = false;
                    scl.EksikGun = false;
                    scl.Maas = 0;
                    scl.MaasTipi = 1;
                    scl.AylikCalismaSaati = 255;
                    scl.SonTasnifID = 0;
                    scl.GeceZammi = false;
                    scl.FM_EM = false;
                    scl.BirimId = sicilbirimid;
                    scl.Firma = 1;
                    scl.PersonelNo = tc;
                    scl.SicilNo = sicil;
                    scl.GlobalSicilID = PerSicilId;
                    ent.Sicil.Add(scl);
                    ent.SaveChanges();

                }
                else
                {
                    var birimkontrol = ent.Tbl_Birimler.Where(u => u.CihazID == birimid).FirstOrDefault();
                    percontrol.Soyad = soyisim;
                    percontrol.BirimId = birimkontrol.Id;
                    percontrol.GlobalSicilID = PerSicilId;
                    ent.SaveChanges();
                }

                //userid için karakter fixer eklenecek

            }
            return "1";
        }

        public static int NextUserlistID()
        {
            if (ent.UserList.Select(x => x.ID).FirstOrDefault() != 0)
            {
                var Userid = ent.UserList.Select(x => x.ID).Max();
                if (Userid == 0)
                    return 1;
                else
                {
                    return Convert.ToInt32(Userid.ToString()) + 1;
                }
            }
            else { return 1; }

        }
        public static int NextSicilID()
        {
            if (ent.Sicil.Select(x => x.ID).FirstOrDefault() != 0)
            {
                var sicilid = ent.Sicil.Select(x => x.ID).Max();
                if (sicilid == 0)
                    return 1;
                else
                {
                    return Convert.ToInt32(sicilid.ToString()) + 1;
                }
            }
            else { return 1; }
        }

        public static string PersonelIzinListesi()
        {
            var izinList = br.PersonelIzinListesiPDKS();
            var kalan = 0;
            foreach (DataRow izn in izinList.Tables[0].Rows)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Kalan Izin Sayýsý=" + (izinList.Tables[0].Rows.Count - kalan));
                kalan++;

                var izinid = izn[0].ToString();
                var personelid = izn[1].ToString();
                var izbas = izn[2].ToString().Split(' ');
                var izinbaslama = izbas[0] + " " + izn[4].ToString();
                var izbit = izn[3].ToString().Split(' ');
                var izinbitis = izbit[0] + " " + izn[5].ToString();
                var izinturid = izn[6].ToString();
                var aciklama = izn[8].ToString();
                var taleptarih = izn[15].ToString();
                var personel = ent.Sicil.Where(u => u.GlobalSicilID == personelid).FirstOrDefault();
                if (personel != null)
                {
                    try
                    {
                        Model.Izinler iz = new Model.Izinler();
                        iz.Aciklama = aciklama;
                        iz.BasTarih = Convert.ToDateTime(izinbaslama);
                        iz.BitTarih = Convert.ToDateTime(izinbitis);
                        iz.SicilID = personel.ID;
                        if (iz.Tarih != DateTime.MinValue)
                        {
                            iz.Tarih = Convert.ToDateTime(taleptarih);
                        }
                        else
                        {
                            iz.Tarih = Convert.ToDateTime(izinbaslama).AddDays(-1);
                        }

                        iz.TipID = Convert.ToInt32(izinturid);
                        ent.Izinler.Add(iz);
                        ent.SaveChanges();
                        DataSet dts = new DataSet();
                        DataTable dt = new DataTable("table");
                        dt.Columns.Add(new DataColumn("PersonelIzinUN", typeof(Guid)));
                        DataRow dr = dt.NewRow();
                        dr["PersonelIzinUN"] = izinid;
                        dt.Rows.Add(dr);
                        dts.Tables.Add(dt);
                        br.PersonelIzinGorulduYapPDKS(dts); //bu alanda bir eksýk var
                    }
                    catch(Exception e)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Aktarýlamayan ID=" + personelid+" "+ e);
                    }
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Aktarýlamayan ID=" + personelid);
                }
            }

            return "1";
        }
    }
}
