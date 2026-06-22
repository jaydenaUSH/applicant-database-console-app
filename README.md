# Applicant Database Console Project

A C# console application that connects to a SQL Server database and generates an organized applicant summary report (console output + CSV), on a recurring interval.

## Tools

- Visual Studio
- Git
- SSMS
- .NET SDK (for `dotnet publish` / running outside VS)

## Database

The project uses SQL Server for the database, managed through SSMS.

## Mockaroo

Mockaroo was used to generate sample data to populate the table, allowing testing of database functions as if the database were populated with real information.

## Entity Framework setup summary

Through PMC, the Entity Framework Core setup was scaffolded against the database, producing model/context classes similar to what you'd configure manually in `appsettings.json`.

---

## Deployment

### Deployment steps

1. Pull the latest code and confirm it builds locally:
   ```
   git pull
   dotnet build
   ```
2. Publish a standalone build (bundles the .NET runtime, so the target machine doesn't need .NET installed):
   ```
   dotnet publish -c Release -r win-x64 --self-contained true -o C:\Deploy\USHTask1
   ```
   If the deploy machine already has the matching .NET runtime installed, you can drop `--self-contained true` for a smaller framework-dependent publish instead.
3. Confirm `appsettings.json` made it into the output folder (it's marked `CopyToOutputDirectory: Always` in the `.csproj`, so it should copy automatically). Update it with the target environment's connection string before running.
4. Copy/move the published folder to its final deployment location if different from the publish output path.

### Runtime configuration notes

- All configuration lives in `appsettings.json`, loaded at startup via `ConfigurationBuilder`.
- The app reads `Directory.GetCurrentDirectory()` as its config base path, so always launch it **from inside** the deployed folder (e.g. `cd C:\Deploy\USHTask1` then `.\USHTask1.exe`), not by double-clicking from a shortcut elsewhere.
- The report currently regenerates every hour (`TimeSpan.FromHours(1)`, hardcoded in `Program.cs`). If the interval needs to be tunable per environment, add a value to `appsettings.json` (e.g. `"ReportIntervalMinutes": 60`) and read it via `config.GetValue<int>("ReportIntervalMinutes")` instead of hardcoding it.

### Connection string guidance

`appsettings.json` example:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=internDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

- The app uses Windows (`Trusted_Connection`) authentication by default, so no SQL login/password is stored in config. This requires the account *running the app* to have a corresponding SQL Server login.
- If deploying to a server where the app runs under a different account (a Windows service account, scheduled task identity, etc.), make sure that account — not your personal dev login — has a mapped SQL login with the necessary permissions (see **Required permissions** below).
- Never commit a connection string containing a real username/password to the repo; Windows auth avoids this entirely for local/dev use.

### Folder path setup

The app creates two folders directly alongside the executable at runtime:

```
<deploy folder>/
  USHTask1.exe
  appsettings.json
  log/        ← Serilog output
  output/     ← generated CSV reports
```

Both are computed off `AppDomain.CurrentDomain.BaseDirectory` and created automatically on startup if missing (`Directory.CreateDirectory`). No manual folder setup should be required — but if you're running an older build, double check it isn't computing these paths via relative `..\..\..` navigation, which only resolves correctly inside a Visual Studio `bin\Debug\netX\` build and breaks once deployed standalone (see **Troubleshooting**).

### Required permissions

- **Folder permissions**: the account running the app needs Read/Write access to the deployed folder (specifically `log\` and `output\`). The app self-checks this at startup and will fail validation if it can't write a test file.
- **SQL Server permissions**: the running account needs a SQL Server login mapped to the `internDB` database with at least `db_datareader` (and `db_datawriter` if the app ever writes back to the DB) permissions.

### Logging location

- Logs are written via Serilog to `log\log-<date>.txt` inside the deployed folder, rolling daily (`RollingInterval.Day`).
- Each entry includes a timestamp, level, source context (`Startup`, `Database`, `FileSystem`, `Report`), and message — check this file first when troubleshooting any failure, since the app logs validation results before doing anything else.

### How to run/verify the deployed application

1. Open a terminal in the deployed folder and run the executable directly — no Visual Studio required:
   ```
   cd C:\Deploy\USHTask1
   .\USHTask1.exe
   ```
2. Leave the console window open. Per current process, the app should be started manually and left running continuously until functionality is confirmed, then stopped manually (Ctrl+C or close the window).
3. Confirm in order:
   - Startup messages print (`Application running`, DB connectivity, folder checks) with no validation failure.
   - `log\log-<date>.txt` exists and contains the startup entries.
   - `output\applicantSummary<timestamp>.csv` appears after the first cycle.
   - Leave it running through a second interval and confirm a new CSV/log entry lands with an updated timestamp, proving the loop repeats correctly.
4. Once confirmed, stop the process manually.

### Troubleshooting notes

| Symptom | Likely cause | Fix |
|---|---|---|
| App prints "ERROR: Startup validation failed" and exits immediately | `log`/`output` folder checks failed | Check `log\log-*.txt` for which folder failed and why (missing, or no R/W access) |
| Works fine in Visual Studio but fails once deployed/published | Paths computed via `..\..\..` off `AppDomain.CurrentDomain.BaseDirectory` — only resolves correctly inside `bin\Debug\netX\`, not in a standalone deployed folder | Anchor `log`/`output` directly off `baseDir` with no relative navigation, and create them with `Directory.CreateDirectory` if missing |
| CSV file write throws `DirectoryNotFoundException` | An extra path segment (e.g. `output + "/output/..."`) duplicated the folder name, pointing at a subfolder that was never created | Build the file path with `Path.Combine(output, fileName)` directly, no extra segments |
| Build fails with missing `Serilog` types | `Serilog` (and `Serilog.Sinks.File`, `Serilog.Settings.Configuration`) not referenced in the `.csproj` | Add the NuGet packages and commit the updated `.csproj` |
| Database connection check fails | Wrong server/instance name, or the running account has no SQL login | Verify `DefaultConnection` in `appsettings.json` and confirm the account running the app has a mapped SQL login |
| App exits right after one cycle instead of repeating | Interval delay didn't execute due to an unhandled exception before `Task.Delay` | Check `log\log-*.txt` for a `Report` error entry from that cycle |

### Screenshots or examples

Example console/report output (see also `output\applicantSummary<timestamp>.csv` for the CSV version):

```
Applicant Summary Report
------------------------

Total Applicants: 1000

Applicants by State
:       825
01:     3
03:     10
...

Applicants by Household Size
Small(1-3): 275
Medium(4-6): 297
Large(7+): 428

Applicants with Children: 916

Applicants with Food insecuirty/assistance need indicators: 761

Mandy Daen      | mdaen0@livejournal.com        | pending
Raff Parradice  | rparradice1@1und1.de          | denied
Anderson Mont   | amont2@yellowpages.com        | pending
Zack Loddy      | zloddy3@vistaprint.com        | pending
Sheelah Lettuce | slettuce4@mit.edu             | pending
Joly Alliberton | jalliberton5@nydailynews.com  | denied
Worthy Grote    | wgrote6@icq.com               | approved
Sella Caustic   | scaustic7@harvard.edu         | approved
Orly Cuttle     | ocuttle8@technorati.com       | approved
Amara Kores     | akores9@opera.com             | pending
```

_Add a screenshot of the running console window and the deployed folder structure (`log/`, `output/`) here once captured from the deployment VM._
<img width="1512" height="982" alt="Screenshot 2026-06-22 at 3 55 53 PM" src="https://github.com/user-attachments/assets/c2895029-722a-4f65-b321-bc300bd75b25" />
<img width="1512" height="982" alt="Screenshot 2026-06-22 at 3 57 55 PM" src="https://github.com/user-attachments/assets/d160a54c-8eb6-4db4-8357-8acd8d4f70fe" />
<img width="1512" height="982" alt="Screenshot 2026-06-22 at 3 58 21 PM" src="https://github.com/user-attachments/assets/08f06fe7-b97d-4790-a097-c8230c8f0dc3" />

---

## Known issues or setup challenges

It was a challenge to incorporate EF into the project since it was a first-time use. There was a lot of troubleshooting involved and a lot of chats with AI trying to figure out how to fix the issues before getting into the actual coding. The deployment phase surfaced a related class of issues — paths and validation logic written assuming a Visual Studio dev environment didn't hold up once run from a standalone published folder; see **Troubleshooting notes** above for the specific fixes.
