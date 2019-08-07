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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            SqlConnection baglanti = new SqlConnection("Data source=PC\\SQLEXPRESS; Initial catalog = Otopark; user = sa; password=123456;");
            
            SqlCommand komut = new SqlCommand("select * from Yer_tbl",baglanti);
            baglanti.Open();
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text==read["YerLokasyon"].ToString() && read["YerDurum"].ToString()=="DOLU")
                        {
                            item.BackColor = Color.Red;
                            
                        }
                    }
                }
            }
            baglanti.Close();
        }


        public void VeriGetir(int musteriID,string aracPlaka,DateTime girissaati,DateTime cikissaati,int uyeliktipi,string ucret)
        {
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
