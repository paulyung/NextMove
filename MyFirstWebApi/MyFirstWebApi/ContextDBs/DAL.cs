using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
namespace MyFirstWebApi.ContextDBs
{
    public class DAL
    {
        //private const String SERVER = "127.0.0.1";
        //private const String DATABASE = "sakila";
        //private const String UID = "root";
        //private const String PASSWORD = "fsdfsafsfdfsad";
        //private const String SSLMODE = "none";
        private static MySqlConnection dbConn;

        private readonly IConfiguration configuration;

        static string hash = "GoingToTheCloud";

        private static string Decrypt(string ds)
        {

            byte[] data = Convert.FromBase64String(ds);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = triDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(results);
                }

            }
        }

        public DAL(IConfiguration config)
        {
            //MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            //builder.Server = SERVER;
            //builder.UserID = UID;
            //builder.Password = PASSWORD;
            //builder.Database = DATABASE;
            ////builder.SSLMODE = SSLMODE;

            //string connString = builder.ToString();
            //builder = null;
            //Console.WriteLine(connString);

            this.configuration = config;
            var connString = configuration.GetConnectionString("default");

            // Decrypt the encrypted password
            var p = connString.Substring(connString.IndexOf("password=") + 9, connString.Length - (connString.IndexOf("password=") + (9 + 1)));
            connString = connString.Replace(p, Decrypt(p));

            dbConn = new MySqlConnection(connString + ";SslMode=none");
        }

        public  List<string> GetAll()
        {

            try
            {
                String query = "SELECT * FROM city";
                MySqlCommand cmd = new MySqlCommand(query, dbConn);
                dbConn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                string name = String.Empty;
                List<string> cityList = new List<string>();
                while (reader.Read())
                {
                    name = (string)reader["city"];
                    cityList.Add(name);
                    // Console.WriteLine(name);
                }

                dbConn.Close();
                return cityList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }
    }
}
