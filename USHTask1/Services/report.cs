
using System.Threading.Tasks.Dataflow;
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

        public Task Generate() {
            // Gather the necessary info from SQL
            var total = _db.MockData1s.Count();
            var parents = _db.MockData1s.Count(a =>a.ChildrenInHousehold>0);
            var inNeed = _db.MockData1s.Count(a => a.EnoughFood ==false|| a.JobLossOrReducedHours==false);
            var states = _db.MockData1s.GroupBy(a =>a.State).Select(group=> new { state = group.Key, Count = group.Count()}).ToList();
            var household = _db.MockData1s.GroupBy(a=>a.HouseholdSize<4?"small":a.HouseholdSize<7?"medium":"large").Select(group => new {Size = group.Key, count = group.Count()}).ToList();
            var top10 = _db.MockData1s.Take(10).ToList();
            //Create the visual for the report
            Console.WriteLine("Applicant Summary Report");
            Console.WriteLine("------------------------");
            Console.WriteLine("Total Applicants", "\t\t", total);
            Console.WriteLine(" Applicants by State");
            foreach (var state in states)
            {
                Console.WriteLine($"{state.state} : {state.Count}");
            }
            Console.WriteLine(" Applicants by Household size");
            foreach(var bin in household)
            {
                Console.WriteLine($"{bin.Size} : {bin.count}");
            }
            Console.WriteLine(" Applicants with children", "\t\t", parents);
            Console.WriteLine("Applicants with Food insecuirty/assistance need indicators", "\t\t", inNeed);
            foreach (var row in top10) {
                Console.WriteLine(row);
            }






            Console.WriteLine(total);
            
            return Task.CompletedTask;
        }
    }
}
