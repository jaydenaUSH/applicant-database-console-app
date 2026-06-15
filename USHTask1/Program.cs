using System.IO.Enumeration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using USHTask1.Models;
using USHTask1.Services;
using System;
using System.IO;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

Console.WriteLine("Application running");

var validationFailed = false;
//DB configs and setup
var options = new DbContextOptionsBuilder<InternDbContext>()
    .UseSqlServer(config.GetConnectionString("DefaultConnection"))
    .Options;
using var db = new InternDbContext(options);
var connectable = await db.Database.CanConnectAsync();
if(connectable){
    Console.WriteLine("Database connection available");
    } else {
    Console.WriteLine("Database connection unavailable.");
        validationFailed = true; }

//Check for the required folders and make sure R-W access
var solutionFolder = Path.Combine(Environment.CurrentDirectory, "..", "..", "..");
solutionFolder = Path.GetFullPath(solutionFolder);
Console.WriteLine("Current dir "+ solutionFolder);
var output = solutionFolder+"/output";
var log= solutionFolder+"/log";
UnixFileMode rw = UnixFileMode.UserRead | UnixFileMode.UserWrite;
if (Directory.Exists(output))
{
    Console.WriteLine("The output directory exists");
     
    File.SetUnixFileMode(output, rw);
} else {
    Console.WriteLine("The output directory does not exist");
    validationFailed = true;
    }

if (Directory.Exists(log))
{
    Console.WriteLine("The log directory exists");
    File.SetUnixFileMode(log, rw);

}
else
{
    Console.WriteLine("The log directory does not exist");
    validationFailed = true;
        }

//

var report = new Report(db);

//await report.Generate();

while (true)
{
    // Run this code every hour and once before then
    await Task.Delay(TimeSpan.FromHours(1));
    if(!validationFailed)
    {
        await report.Generate();

        //Save and output the report in CSV
        var fileName = "reportStuffs";
        Console.WriteLine("Number of records processesd: "   );
    }
}


Console.ReadLine();
