using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otopark.Model
{
    class Abonman
    {
        public string AbonmanTuru { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public double AbonmanUcret { get; set; }
        public int AracID { get; set; }
        private string aracID;
        public string Degisken;


        public string Plaka
        {
            get
            {
                string sorgu = $"  select AracPlaka from Arac_tbl where Arac_tbl.AracID = '{aracID}'";
                DataTable dt =  Sorgu.SqlSorguCalistir(sorgu);
                Degisken = dt.Rows[0].ItemArray[0].ToString();

                return Degisken;
            }
            set { aracID = value; }
        }

    }
}
