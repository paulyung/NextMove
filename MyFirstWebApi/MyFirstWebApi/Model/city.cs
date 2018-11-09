using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebApi.Model
{
    [Table("city")]
    public class City
    {
        public City()
        {
        }

        [Column("city_id")]
		public int id { get; set; }

		[Column("city")]
		public string name { get; set; }
    }
}
