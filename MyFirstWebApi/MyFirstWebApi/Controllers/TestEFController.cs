using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyFirstWebApi.Model;
using MyFirstWebApi.ContextDBs;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyFirstWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestEFController : Controller
    {

		private WorldContext db = new WorldContext();
		private readonly AppSettings _appSettings;

		public string x = String.Empty;

		public TestEFController(IOptions<AppSettings> appsetting)
		{
			_appSettings = appsetting.Value;
            
			 x = Startup.ConnectionString;
		}
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
			try
			{

                using (MySqlConnection connection = new MySqlConnection(x))
				{
                    connection.Open();
                    //return new string[] { "hello", "value2" };
                    using (WorldContext context = new WorldContext(connection, false))
                    {
                    //	var c = context.MyCities.TakeLast(1).FirstOrDefault();
                    //	var env = _appSettings.Environment;
                    //                   return new string[] { c.name, "value2" };
                    }
                    return new string[] { "hello", "value2" };
                }
				//	var c = db.MyCities.TakeLast(1).FirstOrDefault();
				//var env = _appSettings.Environment;
				//return new string[] { c.name , "value2" };
			}
            catch(Exception ex)
			{
				throw ex;
			}
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

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
