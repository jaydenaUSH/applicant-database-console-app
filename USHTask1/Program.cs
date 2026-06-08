using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using USHTask1.Models;
using USHTask1.Services;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
//DB configs
var options = new DbContextOptionsBuilder<InternDbContext>()
    .UseSqlServer(config.GetConnectionString("DefaultConnection"))
    .Options;

using var db = new InternDbContext(options);
var report = new Report(db);
await report.Generate();
Console.ReadLine();
