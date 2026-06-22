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
var baseDir = AppDomain.CurrentDomain.BaseDirectory;
var logpath = Path.Combine(baseDir, "log");
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).WriteTo.File(
        path: Path.Combine(logpath, "log-.txt"),
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}"
    ).CreateLogger();

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


var solutionFolder = Path.Combine(Environment.CurrentDirectory, "..", "..");
var fsLog = Log.Logger.ForContext("SourceContext", "FileSystem");
startupLog.Information("Current dir {SolutionFolder}", baseDir);
var output = Path.Combine(logpath, "..", "output");
output = Path.GetFullPath(output);
Console.WriteLine(Path.GetFullPath(output));
var log= logpath;

if (Directory.Exists(output))
{
    fsLog.Information("The output directory exists");
    //RW perms
    if (!rwPerms(output))
    {
        fsLog.Error("No read/write access to output folder");
        validationFailed = true;
    } else { fsLog.Information("R/W access permitted for output folder"); }
    
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
    else { fsLog.Information("R/W access permitted for log folder"); }
    b
}
else
{
    fsLog.Error("The log directory does not exist");
    validationFailed = true;
        }

//

var report = new Report(db);

var reportLog =Log.Logger.ForContext("SourceContext", "Report");
if (validationFailed)
{
    Console.WriteLine("ERROR: Startup validation failed. Check logs for details.");
    return;
}

while (true)
{
    // Run this code every hour and once before then
    if(!validationFailed)
    {
        try {
            reportLog.Information("Report cyle beginning");
            var reportTxt = await report.Generate();
        //Save and output the report in CSV
        var date = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        var fileName = output+$"/applicantSummary{date}.csv";
            Console.WriteLine("File Name", fileName);
        File.AppendAllText(fileName, reportTxt.ToString());
            reportLog.Information(("Succesful report generation, cycle complete"));
            await Task.Delay(TimeSpan.FromHours(1));

        }
        catch (Exception e) {
            reportLog.Error(e, "Report generation failed");
            //prevent spam
            await Task.Delay(TimeSpan.FromSeconds(30));

        }
    } else
    {
        reportLog.Information("Validation failed report generation skipped");
        Log.CloseAndFlush();

    }

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


    