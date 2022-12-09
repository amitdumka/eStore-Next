using eStore.SetUp.Import;
using Microsoft.Data.SqlClient;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStore.SetUp.Export
{
    public class SQLJson
    {
        static string BasePath = @"D:\EStore\DatabaseBackup";
        static string connectionString = "Data Source=tcp:aprajitaretails.database.windows.net,1433;Initial Catalog=AprajitaRetails_db;User Id=AmitKumar@aprajitaretails;Password=Dumka@@2654";
        static SqlConnection sqlConn;
        static List<string> TableNames;
        string connString = "Server=COMEAU-WIN7;Database=AdventureWorks2012;Trusted_Connection=True;";
        int Count = 0;
       
        public static DataTable Test()
        {
            try
            {


                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();

                SqlCommand sqlCommand = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", sqlConn);
                SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCommand);//, sqlConn);

                DataTable dtEmployees = new DataTable();
                sqlDA.Fill(dtEmployees);
                return dtEmployees;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task< bool> BackupDatabase()
        {

            if (Connect() && FetchTables())
            {
                bool flag = true;
                Directory.CreateDirectory(BasePath);
                Count = 0;
                foreach (var tableName in TableNames)
                {
                    if (await TableToJSon(tableName)==false) flag = false;
                }
                if (flag) return ZipJson(); else return false;
            }
            return false;
        }

        private bool Connect()
        {
            if (sqlConn == null)
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
            }
            if (sqlConn.State != ConnectionState.Open) return false; else return true;
        }
        private bool FetchTables()
        {
            Connect();

            try
            {
                using (SqlCommand com = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", sqlConn))
                {
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        TableNames = new List<string>();
                        while (reader.Read())
                        {
                            TableNames.Add((string)reader["TABLE_NAME"]);
                        }
                    }
                }
                return TableNames.Count > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private async Task<bool> TableToJSon(string tableName)
        {

            try
            {
                if (sqlConn == null)
                    sqlConn = new SqlConnection(connString);
                SqlCommand sqlCommand = new SqlCommand($"Select * from {tableName}", sqlConn);
                SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDA.Fill(dataTable);
                var flag = await ImportData.DataTableToJSONFile(dataTable, Path.Combine(BasePath, "Tables\\" + tableName));
                Count++;
                return flag;
            }
            catch (Exception e)
            {
                if (tableName.Contains("rule")) return true;

                return false;
            }
        }

        private bool ZipJson()
        {
          return  ImportBasic.BackupJSon(Path.Combine(BasePath, $@"Database_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.zip"), Path.Combine(BasePath, "Tables"));
           // return false;
        }
    }

    //class Dump   {
    //       public void ConnectSQLServer()
    //       {
    //           if (sqlConn == null)
    //               sqlConn = new SqlConnection(connString);
    //           SqlDataAdapter sqlDA = new SqlDataAdapter("spCurrentEmployees", sqlConn);
    //           DataTable dtEmployees = new DataTable();
    //           sqlDA.Fill(dtEmployees);
    //           // dgEmployees.DataSource = dtEmployees;
    //       }
    //       public IList<string> ListTables()
    //       {
    //           List<string> tables = new List<string>();
    //           DataTable dt = _connection.GetSchema("Tables");
    //           foreach (DataRow row in dt.Rows)
    //           {
    //               string tablename = (string)row[2];
    //               tables.Add(tablename);
    //           }
    //           return tables;
    //       }
    //       public static List<string> GetTableNames(this SqlConnection connection)
    //       {
    //           using (SqlConnection conn = connection)
    //           {
    //               if (conn.State == ConnectionState.Open)
    //               {
    //                   return conn.GetSchema("Tables").AsEnumerable().Select(s => s[2].ToString()).ToList();
    //               }
    //           }
    //           //Add some error-handling instead !
    //           return new List<string>();
    //       }

    //       public void tablenames()
    //       {
    //           using (SqlConnection con = new SqlConnection(strConnect))
    //           {
    //               con.Open();
    //               using (SqlCommand com = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", con))
    //               {
    //                   using (SqlDataReader reader = com.ExecuteReader())
    //                   {
    //                       myComboBox.Items.Clear();
    //                       while (reader.Read())
    //                       {
    //                           myComboBox.Items.Add((string)reader["TABLE_NAME"]);
    //                       }
    //                   }
    //               }
    //           }
    //       }

    //   }


}
