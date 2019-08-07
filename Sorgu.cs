using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Otopark
{
    class Sorgu
    {
        public static string cs = ConfigurationManager.ConnectionStrings["Onder"].ConnectionString;
        public static DataTable SqlSorguCalistir(string Sql)
        {
            SqlConnection con = null;
            DataTable dt = new DataTable();
            try
            {
                con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand(Sql, con);
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception hata)
            {

                throw new Exception("sorgu çalıştırılmadı: " + hata.Message);

            }
            finally
            {
                con.Close();
            }
            return dt;
        }
        public static int NonSorguCalistir(string Cumle)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand komut = new SqlCommand(Cumle, con);
            con.Open();
            var a = komut.ExecuteNonQuery();
            return a;
        }
      
    }
}
