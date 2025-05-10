using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Proiect_Paoo
{

    public static class DatabaseHelper
    {
        private static readonly string connectionString = "Server=localhost;Database=agentie_turism;User ID=root;Password=root;Port=3306;";
        private static MySqlConnection connection;

        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }
            return connection;
        }

        public static MySqlConnection OpenConnection()
        {
            var conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public static void CloseConnection()
        {
            if (connection != null)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
                connection = null;
            }
        }
    }
}
