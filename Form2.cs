using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Otopark
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string kullaniciad { get; set; }
        string kullanicisoyad { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciadi = txtKullaniciAdi.Text;
            string sifre = txtSifre.Text;
            string sorgux= $"select  PersonelAd,PersonelSoyad from Personel_tbl,Login_tbl where Personel_tbl.PersonelID = Login_tbl.PersonelID and Login_tbl.KullaniciAdi = '{kullaniciadi}' and Login_tbl.Sifre = '{sifre}'";
               DataTable dt = Sorgu.SqlSorguCalistir(sorgux);
            foreach (DataRow row in dt.Rows)
            {
                kullaniciad = row["PersonelAd"].ToString();
                kullanicisoyad = row["PersonelSoyad"].ToString();
            }
        
            if (dt.Rows.Count == 1)
            {
                Form1 fr = new Form1(kullaniciad,kullanicisoyad);
                MessageBox.Show($"{kullaniciad.Trim()} {kullanicisoyad.Trim()} hoşgeldiniz.");
                fr.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Lütfen Şifre ve Kullanıcı Adınızı Kontrol Ediniz.");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
    
        }
    }
}
