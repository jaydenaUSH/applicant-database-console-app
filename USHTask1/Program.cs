using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connString = config.GetConnectionString("DefaultConnection");

using SqlConnection conn = new SqlConnection(connString);
conn.Open();

SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM [MOCK_DATA (1)]", conn);
SqlDataReader reader = cmd.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine(reader[0]); // just prints first column to confirm data is coming back
}