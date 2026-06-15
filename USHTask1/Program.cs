using System.IO.Enumeration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using USHTask1.Models;
using USHTask1.Services;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

Console.log("Applcation running")

var validationFailed = false;
//DB configs and setup
var options = new DbContextOptionsBuilder<InternDbContext>()
    .UseSqlServer(config.GetConnectionString("DefaultConnection"))
    .Options;
using var db = new InternDbContext(options);
var connectable = await db.Database.CanConnect(());
if(connectable){
    console.log("Database connection available")
        validationFailed = true;
    } else { 
    Console.log("Database connection unavailable.")
        validationFailed = true; }

//Check for the required folders and make sure R-W access
var output;
var log;

//

var report = new Report(db);

await report.Generate();

// Run this code every hour and once before then
Task.Delay(TimeSpan.FromHours(1));
{
    await report.Generate();

//Save and output the report in CSV
fileName = "reportStuffs" 
        Console.log("Number of records processesd: " +  )
}
Console.ReadLine();
