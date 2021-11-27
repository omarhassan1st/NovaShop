using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;

namespace Elections.Classes
{
    class Querys
    {
        private static string connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=Electrics;User ID=sa;Password=123456";
        public static void Excuter(string Qury)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(Qury, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
                return;
            }
        }
        public static string Reader_SingleValue(string ReaderQury)
        {
            try
            {
                DataTable table2 = new DataTable();
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(ReaderQury, conn))
                    {
                        conn.Open();
                        object Value = cmd.ExecuteScalar();
                        conn.Close();

                        if (Value == null)
                            return string.Empty;
                        else
                        return Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
                return string.Empty;
            }
        }
        public static void Reader_Datagrid(string ReaderQury, ref GunaDataGridView TableName)
        {
            try
            {
                DataTable table2 = new DataTable();
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(ReaderQury, conn))
                    {
                        conn.Open();
                        table2.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }
                TableName.DataSource = table2;
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
                return;
            }
        }
        public static void Reader_Table(string ReaderQury, ref DataTable TableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(ReaderQury, conn))
                    {
                        conn.Open();
                        TableName.Load(cmd.ExecuteReader());
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
                return;
            }
        }
        public static void CBFillers(GunaComboBox CB, string qury)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(qury, conn))
                    {
                        CB.Items.Clear();
                        conn.Open();
                        SqlDataReader DR = cmd.ExecuteReader();
                        while (DR.Read())
                        {
                            CB.Items.Add(DR[0]);
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                COMMANDS.Error(ex.Message);
            }
        }
    }
}
