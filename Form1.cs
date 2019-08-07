using Otopark.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otopark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string personelad, string personelsoyad)
        {
            InitializeComponent();
            this.Text = $"{personelad.Trim()} {personelsoyad.Trim()} Hoşgeldiniz.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_msterikyt_Click(object sender, EventArgs e)
        {
            if (txt_mustrad.Text == "" || txt_mstsoyad.Text == "" || maskedtxt_tel.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }

            else if (!Regex.Match(txt_mustrad.Text, @"[A-z^şŞıİçÇöÖüÜĞğ\s]*").Success)
            {
                MessageBox.Show("Geçersiz isim girdiniz.");
                return;
            }

            else if (!Regex.Match(txt_mstsoyad.Text, @"[A-z^şŞıİçÇöÖüÜĞğ\s]*").Success)
            {
                MessageBox.Show("Geçersiz soyisim girdiniz.");
                return;
            }

            else
            {
                Helper hp = new Helper();
                hp.AddPersonel(txt_mustrad.Text.Trim().ToUpper(), txt_mstsoyad.Text.Trim().ToUpper(), maskedtxt_tel.Text);
                MessageBox.Show("Yeni müşteri bilgisi başarıyla eklendi.");
                string sorguu = $"select * from Musteri_tbl where Musteri_tbl.MusteriTelefon = '{maskedtxt_tel.Text}'";
                DataTable dt = Sorgu.SqlSorguCalistir(sorguu);
                int MusteriIDm = 0;

                foreach (DataRow item in dt.Rows)
                {
                    MusteriIDm = Convert.ToInt32(item["MusteriID"]);

                }
                string sorgu2 = $"select AracPlaka from Arac_tbl where AracPlaka = '{maskedtx_plk.Text}'";
                DataTable dt2 = Sorgu.SqlSorguCalistir(sorgu2);
                if (dt2.Rows.Count == 0)
                {
                    Helper a = new Helper();
                    a.AracKayit(maskedtx_plk.Text.ToUpper().Trim(), Convert.ToInt32(maskedTextBox1.Text), textBox1.Text.ToUpper().Trim(), comboBox2.SelectedItem.ToString().ToUpper().Trim(), comboBox3.SelectedItem.ToString().ToUpper().ToString(),MusteriIDm);
                    MessageBox.Show("Araç kaydı Başarılı");
                }
                else
                {
                    MessageBox.Show("Araç Sisteme Kayıtlı", "Hata", MessageBoxButtons.OK);
                }
               
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            this.ActiveControl = txt_mustrad;
            txt_mustrad.Focus();
            
        }

        private void btn_otprkgor_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void btn_mguncelle_Click(object sender, EventArgs e)
        {
            if (txt_mustrad.Text == "" || txt_mstsoyad.Text == "" || maskedtxt_tel.Text == "")
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz.", "Hata", MessageBoxButtons.OK);
            }

            else if (!Regex.Match(txt_mustrad.Text, @"[A-z^şŞıİçÇöÖüÜĞğ\s]*").Success)
            {
                MessageBox.Show("Geçersiz isim girdiniz.", "Hata", MessageBoxButtons.OK);
                return;
            }

            else if (!Regex.Match(txt_mstsoyad.Text, @"[A-z^şŞıİçÇöÖüÜĞğ\s]*").Success)
            {
                MessageBox.Show("Geçersiz soyisim girdiniz.", "Hata", MessageBoxButtons.OK);
                return;
            }

            else if (dataGridView2.SelectedRows.Count == 1)
            {
                Helper asd = new Helper();
                int MusteriID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["MusteriID"].Value);
                bool result = asd.MusteriGuncelle(txt_mustrad.Text.ToUpper().Trim(), txt_mstsoyad.Text.ToUpper().Trim(), maskedtxt_tel.Text, MusteriID);
                Helper help = new Helper();
                int AracID = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["AracID"].Value);
                bool aa = help.AracGuncelle(maskedtx_plk.Text.ToUpper().Trim(), Convert.ToInt32(maskedTextBox1.Text), textBox1.Text.ToUpper().Trim(), comboBox2.Text.ToString().ToUpper().Trim(), comboBox3.Text.ToString().ToUpper().Trim(), AracID);
                if (result == true)
                {
                    MessageBox.Show("Güncelleme Başarılı");
                    string sorgu = "select  Musteri_tbl.MusteriID,MusteriAd,MusteriSoyad,MusteriTelefon,AracID,AracPlaka,AracModel,AracYil,AracRenk,AracTip from Musteri_tbl,Arac_tbl where Musteri_tbl.MusteriID=Arac_tbl.MusteriID";
                    var musteriarac = Sorgu.SqlSorguCalistir(sorgu);
                    dataGridView2.DataSource = musteriarac;
                }
                else MessageBox.Show("İşlemBaşarısız", "Hata", MessageBoxButtons.OK);
                txt_mustrad.Clear();
                txt_mstsoyad.Clear();
                maskedtxt_tel.Clear();
                maskedtx_plk.Clear();
                maskedTextBox1.Clear();
                textBox1.Clear();
            }
            else MessageBox.Show("Kullanıcı Seçiniz");
        }

        private void btn_mgor_Click(object sender, EventArgs e)
        {
            string sorgu = "select  Musteri_tbl.MusteriID,MusteriAd,MusteriSoyad,MusteriTelefon,AracID,AracPlaka,AracModel,AracYil,AracRenk,AracTip from Musteri_tbl,Arac_tbl where Musteri_tbl.MusteriID=Arac_tbl.MusteriID";
            var musteriarac = Sorgu.SqlSorguCalistir(sorgu);
            dataGridView2.DataSource = musteriarac;
        }

        private void txt_mustrad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_mustrad_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void btn_giris_Click(object sender, EventArgs e)
        {
            int AracID = 0, AbonmanID = 0, YerID = 0, durum =0;
            
            Fis fis = new Fis();
            string sroguAracID = $"select AracID from Arac_tbl where AracPlaka = '{comboBox5.SelectedItem.ToString()}'";
            DataTable dtAracID = Sorgu.SqlSorguCalistir(sroguAracID);
            foreach (DataRow item in dtAracID.Rows)
            {
                AracID = Convert.ToInt32(item["AracID"]);
            }
            string SorguAA = $"select FisDurumu from Fis_tbl where AracID = {AracID}";
            DataTable AA = Sorgu.SqlSorguCalistir(SorguAA);
            foreach (DataRow item in AA.Rows)
            {
                durum = Convert.ToInt32(item["FisDurumu"]);
            }
            if (AA.Rows.Count==0 && durum==0)
            {
                string sorguAbonmanID = $"select AbonmanID from Abonman_tbl where AracID= {AracID}";
                DataTable dtAbonmanID = Sorgu.SqlSorguCalistir(sorguAbonmanID);
                if (dtAbonmanID.Rows.Count == 1)
                {
                    foreach (DataRow item in dtAbonmanID.Rows)
                    {
                        AbonmanID = Convert.ToInt32(item["AbonmanID"]);
                    }
                }

                //else
                //{
                //    string sorguAbone = $"insert into Abonman_tbl(AbonelikTuru, AracID) values ('{Abone}',{AracID})";
                //    Sorgu.NonSorguCalistir(sorguAbone);
                //    string sorguAbonmanIDD = $"select AbonmanID from Abonman_tbl where AracID= {AracID}";
                //    DataTable dtAbonmanIDD = Sorgu.SqlSorguCalistir(sorguAbonmanIDD);
                //    foreach (DataRow item in dtAbonmanIDD.Rows)
                //    {
                //        AbonmanID = Convert.ToInt32(item["AbonmanID"]);
                //    }

                //}
                fis.FisDurumu = 1;
                string format = "yyyy-MM-dd HH:mm:ss";
                fis.Giris = DateTime.Now;
                //string Giris = fis.Giris.ToString();
                string sorgu2 = $"Insert into Fis_tbl(AracID,[Giris],FisDurumu) VALUES({AracID},'{fis.Giris.ToString(format)}',{fis.FisDurumu})";
                var sonuc1 = Sorgu.NonSorguCalistir(sorgu2);
                string YerLok = comboBox7.SelectedItem.ToString();
                string sorgu4 = $"select YerID from Yer_tbl where YerLokasyon= '{YerLok}' ";
                DataTable dtYer = Sorgu.SqlSorguCalistir(sorgu4);
                foreach (DataRow item in dtYer.Rows)
                {
                    YerID = Convert.ToInt32(item["YerID"]);
                }
                string sorgu5 = $"Update Arac_tbl set YerID='{YerID}' where AracID='{AracID}'";
                var sonuc5 = Sorgu.NonSorguCalistir(sorgu5);
                string sorgu = $"Update Yer_tbl set YerDurum='DOLU' where YerLokasyon='{comboBox7.SelectedItem.ToString()}'";
                var sonuc = Sorgu.NonSorguCalistir(sorgu);
                MessageBox.Show("Giriş Başarılı");
            }
            else
            {
                MessageBox.Show("Araç Giriş Yapmaz");
            }
           
        }

        private void btn_cikis_Click(object sender, EventArgs e)
        {
            string aracgiris = null;
            string Musteri =textBox2.Text;
            double ucret = 0;
            int YerID = 0;
            
            int cikis =Convert.ToInt32( DateTime.Now.Hour.ToString());
           
            string sorgu = $"Select Giris from Fis_tbl f,Arac_tbl a where f.FisDurumu=1 and a.AracPlaka='{comboBox5.SelectedItem.ToString()}' and  a.AracID=f.AracID";
            DataTable dt = Sorgu.SqlSorguCalistir(sorgu);
          

            if (dt.Rows.Count==1)
            {
                foreach (DataRow item in dt.Rows)
                {
                   

                    aracgiris = item["Giris"].ToString();
                    var x = Convert.ToDateTime(aracgiris).Hour;
                    int total = (cikis - Convert.ToInt32(x))+1;
                    if (total < 0)
                    {
                        ucret = (total + 24) * 8;
                    }
                    else
                    {
                        ucret = 8 * total;
                    }
                }
                string sorgu2 = $"update Fis_tbl set FisDurumu=0,Cikis='{DateTime.Now.ToString()}'";
                var sonuc = Sorgu.NonSorguCalistir(sorgu2);
                string sorgu3 = $"select YerID from Arac_tbl where AracPlaka='{comboBox5.SelectedItem.ToString()}' ";
                DataTable dt2 = Sorgu.SqlSorguCalistir(sorgu3);
                foreach (DataRow item in dt2.Rows)
                {
                    YerID = Convert.ToInt32(item["YerID"]);
                }
                string sorgu4 = $"Update Yer_tbl set YerDurum='BOŞ' where YerID={YerID}";
                var sonuc2 = Sorgu.NonSorguCalistir(sorgu4);
                MessageBox.Show("Çıkış Yapıldı");
            }

        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            if (msk_abnplaka.Text=="")
            {
                MessageBox.Show("Lütfen önce geçerli bir plaka giriniz.", "Hata", MessageBoxButtons.OK);
            }

            else if (cboxAbonmanTuru.Text == "")
            {
                MessageBox.Show("Lütfen abonelik türünü seçiniz.","Hata",MessageBoxButtons.OK);
            }

            else
            {
                string AracID = null;
                Helper help = new Helper();
                string abnbaslangictarihi = Convert.ToDateTime(dtAbonelikBaslangic.Value).ToString("yyyy-MM-dd");
                string abnbitistarihi = Convert.ToDateTime(dtAbonelikBitis.Value).ToString("yyyy-MM-dd");
                string srogu1 = $"select  Arac_tbl.AracID from Arac_tbl where  Arac_tbl.AracPlaka = '{msk_abnplaka.Text}'";
                DataTable dt = Sorgu.SqlSorguCalistir(srogu1);
                int a = dt.Rows.Count;
                foreach (DataRow row in dt.Rows)
                {
                    AracID = (row["AracID"]).ToString();
                }
                help.AbonmanKayit(cboxAbonmanTuru.Text, abnbaslangictarihi, abnbitistarihi, Convert.ToDouble(txtAbonmanUcret.Text), Convert.ToInt32(AracID));
                dataGridView3.DataSource = help.Abonmanlar();
            }
        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_rzvyap_Click(object sender, EventArgs e)
        {
            int AracID = 0;
            string YerID = null;
            string dd = Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyy-MM-dd");
            string sorgu = $"Select Arac_tbl.AracID,Yer_tbl.YerID from Arac_tbl,Yer_tbl where AracPlaka='{maskedTextBox3.Text}' and Yer_tbl.YerLokasyon='{comboBox1.SelectedItem.ToString()}'";
            DataTable dt = Sorgu.SqlSorguCalistir(sorgu);
            foreach (DataRow item in dt.Rows)
            {
                AracID = Convert.ToInt32(item["AracID"].ToString());
                YerID = item["YerID"].ToString();
            }
            
            string sorgu2 = $"Insert into Rezervasyon_tbl(AracID,YerID,RezervasyonTarih) VALUES({AracID},'{YerID}','{dd}')";
            var sonuc = Sorgu.NonSorguCalistir(sorgu2);
            string sorgu3 = $"Update Yer_tbl set YerDurum='DOLU' where YerLokasyon='{comboBox1.SelectedItem.ToString()}'";
            var xx = Sorgu.NonSorguCalistir(sorgu3);
            MessageBox.Show("Rezervasyon Başarılı");
        }

        private void cboxAbonmanTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            double ucret = 8.0;
            if (cboxAbonmanTuru.SelectedIndex==0)
            {
                dtAbonelikBitis.Value = dtAbonelikBaslangic.Value.AddDays(7);
                txtAbonmanUcret.Text = Convert.ToString(ucret * 7);
            }

            else if (cboxAbonmanTuru.SelectedIndex == 1)
            {
                dtAbonelikBitis.Value = dtAbonelikBaslangic.Value.AddDays(30);
                txtAbonmanUcret.Text = Convert.ToString(ucret * 30);
            }

            else if (cboxAbonmanTuru.SelectedIndex==2)
            {
                dtAbonelikBitis.Value = dtAbonelikBaslangic.Value.AddDays(365);
                txtAbonmanUcret.Text = Convert.ToString(ucret * 365);
            }
        }

        private void dtAbonelikBaslangic_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count==1)
            {
                string sorgu = $"delete from Abonman_tbl where Abonman_tbl.AracID='{dataGridView3.SelectedRows[0].Cells["AracID"].Value}'";
                Sorgu.SqlSorguCalistir(sorgu);
                Helper abonman = new Helper();
                dataGridView3.DataSource = abonman.Abonmanlar();
            }

            else
            {
                MessageBox.Show("Önce silmek istediğiniz aboneyi seçin.", "Hata", MessageBoxButtons.OK);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (msk_abnplaka.Text=="")
            {
                MessageBox.Show("Plaka girmediniz.", "Hata", MessageBoxButtons.OK);
            }

            else if (cboxAbonmanTuru.Text=="")
            {
                MessageBox.Show("Abonelik türü seçilmedi.", "Hata", MessageBoxButtons.OK);
            }

            else
            { 
            string AracID = null;
            Helper help = new Helper();
            string abnbaslangictarihi = Convert.ToDateTime(dtAbonelikBaslangic.Value).ToString("yyyy-MM-dd");
            string abnbitistarihi = Convert.ToDateTime(dtAbonelikBitis.Value).ToString("yyyy-MM-dd");
            string srogu1 = $"select  Arac_tbl.AracID from Arac_tbl where  Arac_tbl.AracPlaka = '{msk_abnplaka.Text}'";
            DataTable dt = Sorgu.SqlSorguCalistir(srogu1);
            int a = dt.Rows.Count;
            foreach (DataRow row in dt.Rows)
            {
                AracID = (row["AracID"]).ToString();
            }
            help.AbonmanGuncelle(cboxAbonmanTuru.Text.Trim().ToUpper(), abnbaslangictarihi, abnbitistarihi, Convert.ToDouble(txtAbonmanUcret.Text), Convert.ToInt32(AracID));
            MessageBox.Show("Abonman bilgisi başarılı şekilde güncellendi.");
            dataGridView3.DataSource = help.Abonmanlar();
            }
        }

        private void msk_abnplaka_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btn_arackaydet_Click(object sender, EventArgs e)
        {
           
        }

        private void maskedtx_plk_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Helper abonman = new Helper();
            dataGridView3.DataSource = abonman.Abonmanlar();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txt_mustrad.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            txt_mstsoyad.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            maskedtxt_tel.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            maskedtx_plk.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
            maskedTextBox1.Text = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
            comboBox2.Text = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();
            comboBox3.Text = dataGridView2.Rows[e.RowIndex].Cells[9].Value.ToString();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView4_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            //string sorguu = $"select * from Musteri_tbl where Musteri_tbl.MusteriTelefon = '{maskedTextBox2.Text}'";
            //DataTable dt = Sorgu.SqlSorguCalistir(sorguu);
            //int MusteriID = 0;
            
            //foreach (DataRow item in dt.Rows)
            //{
            //    MusteriID = Convert.ToInt32(item["MusteriID"]);

            //}
            //string sorgu2 = $"select AracPlaka,AracModel,AracYil,AracRenk,AracTip from Arac_tbl where Arac_tbl.MusteriID='{MusteriID}'";
            //DataTable dt2 = Sorgu.SqlSorguCalistir(sorgu2);
            //Arac araba = new Arac();
            //foreach (DataRow item in dt2.Rows)
            //{
            //    araba.Plaka = item["AracPlaka"].ToString();
            //    araba.Model = item["AracModel"].ToString();
            //    araba.Yıl = Convert.ToInt32(item["AracYil"]);
            //    araba.Renk = item["AracRenk"].ToString();
            //    araba.AracTip = item["AracTip"].ToString();


            //}
            //maskedtx_plk.Text = araba.Plaka;
            //maskedTextBox1.Text = araba.Yıl.ToString();
            //textBox1.Text = araba.Model;
            //comboBox2.Text = araba.Renk;
            //comboBox3.Text = araba.AracTip;

          



            //if (dt.Rows.Count == 0)
            //{
            //    Helper musteriIDgetir = new Helper();
            //   musteriIDgetir.AraclariGetir
            //    MessageBox.Show("Araç kaydı Başarılı");
            //}
            
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }
       
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int MusteriID = 0, AracID=0;
            string AboneTur = null;

            string sorgu1 = $"Select MusteriID from Arac_tbl where AracPlaka = '{comboBox5.SelectedItem.ToString()}'";
            DataTable dt = Sorgu.SqlSorguCalistir(sorgu1);
            foreach (DataRow item in dt.Rows)
            {
                MusteriID = Convert.ToInt32(item["MusteriID"]);

            }
            string sorgu2 = $"Select MusteriAd, MusteriSoyad from Musteri_tbl where MusteriID ='{MusteriID }'";
            DataTable dt2 = Sorgu.SqlSorguCalistir(sorgu2);
            Musteri musteri = new Musteri();
            foreach (DataRow item in dt2.Rows)
            {
                musteri.MusteriAd = item["MusteriAd"].ToString();
                musteri.MusteriSoyad = item["MusteriSoyad"].ToString();
            }
            textBox2.Text = musteri.MusteriAd.Trim() + " "+musteri.MusteriSoyad.Trim();
            string sorguArac = $"select AracID from Arac_tbl where AracPlaka= '{comboBox5.SelectedItem.ToString()}'";
            DataTable dtARac = Sorgu.SqlSorguCalistir(sorguArac);
            foreach (DataRow item in dtARac.Rows)
            {
                AracID = Convert.ToInt32(item["AracID"]);
            }
            string sorgu3 = $"select AbonelikTuru from Abonman_tbl where AracID = {AracID}";
            DataTable dt3 = Sorgu.SqlSorguCalistir(sorgu3);
            if (dt3.Rows.Count==1)
            {
                foreach (DataRow item in dt3.Rows)
                {
                    AboneTur = item["AbonelikTuru"].ToString();
                }
                comboBox6.Text = AboneTur;
            }
            else
            {
                comboBox6.Text = "Yok";
            }
           
        }

        private void comboBox5_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox5.Items.Clear();
            string sorgu3 = $"Select AracPlaka from Arac_tbl";
            DataTable dt3 = Sorgu.SqlSorguCalistir(sorgu3);
            foreach (DataRow item in dt3.Rows)
            {
                comboBox5.Items.Add(item["AracPlaka"].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();
            string[] Bosyerler;

            string sorgu = $"Select YerLokasyon from Yer_tbl where YerDurum='BOŞ'";
            DataTable x = Sorgu.SqlSorguCalistir(sorgu);
            Bosyerler = new string[x.Rows.Count];
            int sayac = 0;
            foreach (DataRow item in x.Rows)
            {
                Bosyerler[sayac] = item["YerLokasyon"].ToString();
                sayac++;
            }
            comboBox1.Items.AddRange(Bosyerler);
        }

        private void btn_rzvgor_Click(object sender, EventArgs e)
        {
            string sorgu = $"select MusteriAd,MusteriSoyad,RezervasyonTarih from Arac_tbl a,Musteri_tbl m,Rezervasyon_tbl r where a.AracID=r.AracID and a.MusteriID=m.MusteriID";
            DataTable dt = Sorgu.SqlSorguCalistir(sorgu);
            dataGridView1.DataSource = dt;
        }

        private void comboBox7_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox7.Items.Clear();
            string[] Bosyerler;

            string sorgu = $"Select YerLokasyon from Yer_tbl where YerDurum='BOŞ'";
            DataTable x = Sorgu.SqlSorguCalistir(sorgu);
            Bosyerler = new string[x.Rows.Count];
            int sayac = 0;
            foreach (DataRow item in x.Rows)
            {
                Bosyerler[sayac] = item["YerLokasyon"].ToString();
                sayac++;
            }
            comboBox7.Items.AddRange(Bosyerler);
        }

        private void comboBox5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            msk_abnplaka.Text=dataGridView3.Rows[e.RowIndex].Cells[5].Value.ToString();
            cboxAbonmanTuru.Text = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }
}
