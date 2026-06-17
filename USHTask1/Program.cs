using System.IO.Enumeration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using USHTask1.Models;
using USHTask1.Services;
using System;
using System.IO;
using Serilog;

//Base app config + var init
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

Console.WriteLine("Application running");

var validationFailed = false;

//Serilog config
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
var startupLog = Log.Logger.ForContext("SourceContext", "Startup");
startupLog.Information("Application running");

//DB configs and setup
var options = new DbContextOptionsBuilder<InternDbContext>()
    .UseSqlServer(config.GetConnectionString("DefaultConnection"))
    .Options;
using var db = new InternDbContext(options);
var connectable = await db.Database.CanConnectAsync();
var dbLog = Log.Logger.ForContext("SourceContext", "Database");
if(connectable){
    dbLog.Information("Database connection available");
    } else {
    dbLog.Information("Database connection unavailable.");
        validationFailed = true; }

//Check for the required folders and make sure R-W access

var solutionFolder = Path.Combine(Environment.CurrentDirectory, "..", "..", "..");
solutionFolder = Path.GetFullPath(solutionFolder);
var fsLog = Log.Logger.ForContext("SourceContext", "FileSystem");
startupLog.Information("Current dir {SolutionFolder}", solutionFolder);
var output = solutionFolder+"/output";
var log= solutionFolder+"/log";

if (Directory.Exists(output))
{
    fsLog.Information("The output directory exists");
    //RW perms
    if (!rwPerms(output))
    {
        fsLog.Error("No read/write access to output folder");
        validationFailed = true;
    }
    
} else {
    fsLog.Information("The output directory does not exist");
    validationFailed = true;
    }

if (Directory.Exists(log))
{
    fsLog.Information("The log directory exists");
    //RW PERMS
    if(!rwPerms(log))
    {
        fsLog.Error("No read/write access to log folder");
        validationFailed = true;
    }

}
else
{
    fsLog.Error("The log directory does not exist");
    validationFailed = true;
        }

//

var report = new Report(db);

var reportLog =Log.Logger.ForContext("SourceContext", "Report");
while (true)
{
    // Run this code every hour and once before then
    if(!validationFailed)
    {
        try {
            var reportTxt = await report.Generate();

        //Save and output the report in CSV
        var date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        var fileName = solutionFolder+$"/output/applicantSummary{date}.csv";
        File.WriteAllText(fileName, reportTxt.ToString());
            Console.WriteLine("Number of records processesd: ");
        }
        catch (Exception e) {
            reportLog.Error(e, "Report generation failed");
        }
    } else
    {
        reportLog.Information("Validation failed report generation skipped");
    }
    await Task.Delay(TimeSpan.FromHours(1));

}

bool rwPerms(string folder) {
    try {
        //Make a tmp file and try to read and write to it
        var testFile = Path.Combine(folder, ".tmp");
        File.WriteAllText(testFile, "test");
        File.ReadAllText(testFile);
        return true;

    }
    catch(UnauthorizedAccessException) {
        //Invalid perm
        return false;
    }
    catch (IOException)
    {
        //File location issue
        return false;
    }


}


Console.ReadLine();
