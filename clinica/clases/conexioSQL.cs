using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinica.clases
{
    public class conexioSQL
    {
        public static SqlConnection Clinica()
        {
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.clinica);

            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            else
            {
                cn.Open();
            }

            return cn;
        }
    }
}
