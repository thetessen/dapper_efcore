using DapperEFCorePerformanceBenchmarks.Models;
using DapperEFCorePerformanceBenchmarks.TestData;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace DapperEFCorePerformanceBenchmarks.DataAccess
{
    public class EntityFrameworkCore : ITestSignature
    {
        public long GetPlayerByID(int id )
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                var player = context.Players.First(x => x.Id == id);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetRosterByTeamID(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                var players = context.Teams.Include(x => x.Players).AsNoTracking().Single(x => x.Id == teamId);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetTeamRostersForSport(int sportId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                var players = context.Teams.Include(x => x.Players).Where(x => x.SportId == sportId).AsNoTracking().ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long Update()
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                var entity = context.Sports.Where(x => x.Id == 13).AsNoTracking()
                    .SingleOrDefault();
                entity.Name = "Điện tử";
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();

            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
        public long Delete()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                var entity = context.Sports.Where(x => x.Id == 13).AsNoTracking()
                    .SingleOrDefault();
                context.Sports.Remove(entity);
                context.SaveChanges();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long Create()
        {
            Stopwatch watch = new Stopwatch();
            Sport sport = new Sport()
            {
                Id = 13,
                Name = "Esport"
            };
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                context.Sports.Add(sport);
                context.SaveChanges();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
