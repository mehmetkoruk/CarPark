using Otopark.Model;
using System;
using System.Collections.Generic;
using System.Data;



namespace Otopark
{
    class Helper
    {
        //Select PersonelAd,PersonelSoyad from Personel_tbl join Login_tbl on Personel_tbl.PersonelID=Login_tbl.PersonelID
        public bool AddPersonel(string Ad, string Soyad, string telefon)
        {
            string sorgu = $"insert into Musteri_tbl(MusteriAd,MusteriSoyad,MusteriTelefon) " +
                $"values('{Ad}','{Soyad}','{telefon}')";
            var sonuc = Sorgu.NonSorguCalistir(sorgu);
            return sonuc > 0 ? true : false;
        }
        public List<Musteri> GetListAll()
        {
            DataTable dt = Sorgu.SqlSorguCalistir("Select*From Musteri_tbl");
            return MusteriListele(dt);
        }
        public List<Musteri> MusteriListele(DataTable dt)
        {
            List<Musteri> listpersonel = new List<Musteri>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Musteri a = new Musteri();
                a.MusteriID = Convert.ToInt32(dt.Rows[i]["MusteriID"]);
                a.MusteriAd = dt.Rows[i]["MusteriAd"].ToString();
                a.MusteriSoyad = dt.Rows[i]["MusteriSoyad"].ToString();
                a.MusteriTel = (dt.Rows[i]["MusteriTelefon"]).ToString();
                listpersonel.Add(a);

            }
            return listpersonel;
        }
        public bool MusteriGuncelle(string Ad, string Soyad, string Tel,int MusteriID)
        {
            string sorgu = $"update Musteri_tbl set MusteriAd='{Ad}',MusteriSoyad='{Soyad}', MusteriTelefon='{Tel}' where MusteriID={MusteriID}";
            var sonuc = Sorgu.NonSorguCalistir(sorgu);
            return sonuc > 0 ? true : false;
        }
       public bool AracKayit(string plaka,int aracyil,string model,string renk,string tip, int MusteriID)
        {
            string sorgu = $"insert into Arac_tbl(AracPlaka, AracRenk, AracModel, AracTip, AracYil, MusteriID) values('{plaka}', '{renk}', '{model}', '{tip}', {aracyil},{MusteriID})";
            var sonuc = Sorgu.NonSorguCalistir(sorgu);

            return sonuc > 0 ? true : false;
        }
        public bool AracKayit(string plaka, int musteriID)
        {
            string sorgu = $"insert into Arac_tbl(AracPlaka, MusteriID) values('{plaka}','{musteriID}' )";
            var sonuc = Sorgu.NonSorguCalistir(sorgu);

            return sonuc > 0 ? true : false;
        }
        public bool AbonmanKayit(string abonetur, string baslangic, string bitis, double ucret,int AracID)
        {
            string sorgu = $"insert into Abonman_tbl(AbonelikTuru, AbonelikBaslangic, AbonelikBitis, AbonmanUcret,AracID) values('{abonetur}', '{baslangic}', '{bitis}', '{ucret}','{AracID}')";
            var sonuc = Sorgu.NonSorguCalistir(sorgu);

            return sonuc > 0 ? true : false;
        }
        public bool AracGuncelle(string Plaka, int Yil, string Model, string Renk, string Tip, int AracID)
        {
            string sorgu = $"update Arac_tbl set AracModel='{Model}',AracYil='{Yil}', AracRenk='{Renk}', AracTip='{Tip}' where AracID={AracID}";
            var sonuc = Sorgu.NonSorguCalistir(sorgu);
            return sonuc > 0 ? true : false;
        }
        public List<Arac> AracListele(DataTable dt)
        {
            List<Arac> araclistesi = new List<Arac>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Arac a = new Arac();
                a.AracID = Convert.ToInt32(dt.Rows[i]["AracID"]);
                a.AracTip = dt.Rows[i]["AracTip"].ToString();
                a.Renk = dt.Rows[i]["AracRenk"].ToString();
                a.Model = (dt.Rows[i]["AracModel"]).ToString();
                a.Plaka = (dt.Rows[i]["AracPlaka"]).ToString();
                a.Yıl= Convert.ToInt32(dt.Rows[i]["AracYil"]);
                araclistesi.Add(a);

            }
            return araclistesi;
        }
        public List<Abonman> AbonmanlarıListele(DataTable dt)
        {
            List<Abonman> abonmanListesi = new List<Abonman>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Abonman abonman = new Abonman();
                abonman.AracID = Convert.ToInt32(dt.Rows[i]["AracID"]);
                abonman.AbonmanTuru = dt.Rows[i]["AbonelikTuru"].ToString();
                abonman.AbonmanUcret = Convert.ToInt32(dt.Rows[i]["AbonmanUcret"]);
                abonman.BaslangicTarihi = Convert.ToDateTime(dt.Rows[i]["AbonelikBaslangic"]);
                abonman.BitisTarihi = Convert.ToDateTime(dt.Rows[i]["AbonelikBitis"]);
                abonman.Plaka =dt.Rows[i]["AracID"].ToString();
                abonmanListesi.Add(abonman);

            }
            return abonmanListesi;
        }

        public List<Arac> AraclariGetir()
        {
            DataTable dt = Sorgu.SqlSorguCalistir("Select*From Arac_tbl");
            return AracListele(dt);
        }
        public List<Abonman> Abonmanlar()
        {
            DataTable dt = Sorgu.SqlSorguCalistir("Select * from Abonman_tbl");
            return AbonmanlarıListele(dt);
        }
        public bool AbonmanGuncelle(string abonetur, string baslangic, string bitis, double ucret, int AracID)
        {
            string sorgu= $"update Abonman_tbl set AbonelikTuru='{abonetur}',AbonelikBaslangic='{baslangic}', AbonelikBitis='{bitis}', AbonmanUcret='{ucret}' where AracID={AracID}";
            var sonuc = Sorgu.NonSorguCalistir(sorgu);
            return sonuc > 0 ? true : false;

        }
        //public bool RezervasyonYap(string Yerlokasyon, string Plaka)
        //{
        //    string sorgu = $"Update  Yer_tbl set YerDurum='DOLU' where YerLokasyon='{Yerlokasyon}'";
        //    string sorgu2=$"insert into "
               
        //    var sonuc = Sorgu.NonSorguCalistir(sorgu);
        //    return sonuc > 0 ? true : false;
        //}
    }
     
}
