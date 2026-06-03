using Microsoft.Extensions.Configuration;
using USHTask1.Models;
using USHTask1.Services;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


using var db = new InternDbContext();
var report = new Report(db);


