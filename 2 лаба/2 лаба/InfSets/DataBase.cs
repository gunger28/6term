using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;

using Npgsql;
using System.Data;

namespace InfSets
{
    class DataBase
    {
        public string host;
        public string port;
        public string user;
        public string dataBaseName;
        public string password;

        public string connectionString;

        public bool isOpened;

        NpgsqlConnection con;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;

        public DataBase() => Console.WriteLine("Класс базы данных создан!");

        public void initializeConnection(string host = "localhost", 
                                         string port = "5432", 
                                         string user = "egor", 
                                         string dataBaseName = "library", 
                                         string password = "123"
                                        ) {
            this.host = host;
            this.port = port;
            this.dataBaseName = dataBaseName;
            this.user = user;
            this.password = password;

            connectionString = "Server=" + host + ";port=" + port +
                ";Database=" + dataBaseName + ";User Id=" + user + ";Password=" + password + ";";
            con = new NpgsqlConnection(connectionString);
            comm = con.CreateCommand();
            isOpened = false;
        }

        public bool openConnection() {
            try
            {
                con.Open();

                if (Convert.ToBoolean(con.State) == true)
                {
                    isOpened = true;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool closeConnection()
        {
            try
            {
                con.Close();

                if (Convert.ToBoolean(con.State) == false)
                {
                    isOpened = false;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool executeQuery(string sql_query)
        {
            string queryType = sql_query.Split()[0].ToLower();

            if (queryType.Equals("select"))
            {
                return selectQuery(sql_query);
            } else
            {
                return dataQuery(sql_query);
            };

        }

        public NpgsqlDataReader GetDataReader()
        {
            return reader;
        }

        private bool selectQuery(string sql_query)
        {
            
            comm.CommandText = sql_query;

            try
            {
                using (NpgsqlDataReader localReader = comm.ExecuteReader())
                {
                    reader = localReader;

                    DataSet ds = new DataSet("DataBaseData");
                    DataTable author = new DataTable("Row");
                    author.Load(localReader);
                    ds.Tables.Add(author);

                    ds.WriteXml("books.xml");
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        private bool dataQuery(string sql_query)
        {
            comm.CommandText = sql_query;
            try
            {
                if (comm.ExecuteNonQuery() > 0) return true;
            } catch
            {
                return false;
            }

            return false;
        }
    }
}
