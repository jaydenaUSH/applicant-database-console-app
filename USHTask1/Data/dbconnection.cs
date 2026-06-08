using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
namespace USHungerDB
{

    public class Dbconnection
    {
        private readonly string _connection;
        public Dbconnection() {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            _connection = config.GetConnectionString("DefaultConnection");
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connection);
        }

    }

}

