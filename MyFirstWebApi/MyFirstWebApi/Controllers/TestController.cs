using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MyFirstWebApi.ContextDBs;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
		//private const String SERVER = "127.0.0.1";
        //private const String DATABASE = "sakila";
        //private const String UID = "root";
        //private const String PASSWORD = "";
        //private const String SSLMODE = "none";
        //private static MySqlConnection dbConn;


        //public IConfiguration Configuration { get; }
        private readonly IConfiguration configuration;

        public TestController(IConfiguration config)
        {
            this.configuration = config;
        }

        public static void InitializeDB()
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
            //dbConn = new MySqlConnection(connString + ";SslMode=none");
        }

        // Choose either one method
        [HttpGet("GetAll")]   // Cobmine Get and Action
        //[HttpGet]
        //[Route("GetAll")]
        public  string GetAll()
        {

            try
            {
                //return "I am fine.";
                if (configuration.GetConnectionString("default") == null)
                {
                    return "It is null";
                }
                else
                {
                    //return configuration.GetConnectionString("default");
                    return "successfull read the connection string";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
                // Console.WriteLine(ex.Message);
				//throw ex;
            }

        }

        // GET: api/values
        [HttpGet]
        public List<string> Get()
        {

            //InitializeDB();
            //  string city = GetAll();
            // return new string[] { city, "value2" };
            DAL dl = new DAL(configuration);
            return dl.GetAll();

        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
