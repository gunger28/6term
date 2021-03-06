﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

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
            NpgsqlCommand qert = con.CreateCommand();
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
    }
}
