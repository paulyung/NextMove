using System;
using MySql.Data.Entity;
using System.Data.Entity;
using System.Data.Common;
using MyFirstWebApi.Model;


namespace MyFirstWebApi.ContextDBs
{
	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class WorldContext : DbContext
	{
		public WorldContext() : base(Startup.ConnectionString)
		{
		}

		// Constructor to use on a DbConnection that is already opened
		public WorldContext(DbConnection existingConnection, bool contextOwnsConnection)
          : base(existingConnection, contextOwnsConnection)
        {

        }

		public DbSet<City> MyCities { get; set; }
	}
}
