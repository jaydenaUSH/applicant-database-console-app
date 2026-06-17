
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using USHTask1.Models;

namespace USHTask1.Services
{
    public class Report
    {
        private readonly InternDbContext _db;

        public Report(InternDbContext db)
        {
            _db = db;
        }

        public async Task<StringBuilder> Generate() {
            var header = new StringBuilder();
            /* EDGE CASES
             If the return is empty
             
             */
            // Gather the necessary info from SQL
            var total = _db.MockData1s.Count();
            var parents = _db.MockData1s.Count(a =>a.ChildrenInHousehold>0);
            var inNeed = _db.MockData1s.Count(a => a.EnoughFood ==false|| a.JobLossOrReducedHours==false);
            var states = _db.MockData1s.GroupBy(a =>a.State).Select(group=> new { state = group.Key, Count = group.Count()}).ToList();
            var household = _db.MockData1s.GroupBy(a=>a.HouseholdSize<4?"Small(1-3)":a.HouseholdSize<7?"Medium(4-6)":"Large(7+)").Select(group => new {Size = group.Key, count = group.Count()}).ToList();
            var top10 = _db.MockData1s.AsNoTracking().Select(a=> new{a.FirstName, a.LastName, a.Email,a.ApplicantStatus}).Take(10).ToList();
            //Create the visual for the report
            Console.WriteLine("Applicant Summary Report");
            Console.WriteLine("------------------------");
            Console.WriteLine($"\nTotal Applicants: {total}\n");
            Console.WriteLine("Applicants by State");
            foreach (var state in states)
            {
               
                Console.WriteLine($"{state.state}:\t{state.Count}");
            }
            Console.WriteLine("\nApplicants by Household Size");
            foreach(var bin in household)
            {
                Console.WriteLine($"{bin.Size}: {bin.count}");
            }
            Console.WriteLine($"\nApplicants with Children: {parents}");
            Console.WriteLine($"\nApplicants with Food insecuirty/assistance need indicators: {inNeed}");
            foreach (var row in top10) {
                Console.WriteLine($"\n{row.FirstName} {row.LastName}\t| {row.Email.PadRight(25)}\t| {row.ApplicantStatus}");
            }






            
            return header;
        }
    }
}
