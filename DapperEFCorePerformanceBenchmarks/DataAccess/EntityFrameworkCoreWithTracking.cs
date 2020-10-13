using DapperEFCorePerformanceBenchmarks.DataAccess;
using DapperEFCorePerformanceBenchmarks.Models;
using DapperEFCorePerformanceBenchmarks.TestData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

public class EntityFrameworkCoreWithTracking : ITestSignature
{
    public long GetPlayerByID(int id)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
        {
            var player = context.Players.Where(x=>x.Id == id).First();
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
        var teamRoster = context.Teams.Include(x => x.Players).Single(x => x.Id == teamId);
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
        var players = context.Teams.Include(x => x.Players).Where(x => x.SportId == sportId).ToList();
    }
    watch.Stop();
    return watch.ElapsedMilliseconds;
}
    public long Update(int Id)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                var entity = context.Players.Where(x => x.Id == 130999 * Id)
                    .SingleOrDefault();
                entity.FirstName = "Tessen";
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
        public long Delete(int Id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                var entity = context.Players.Where(x => x.Id == 130999 * Id)
                    .SingleOrDefault();
                context.Players.Remove(entity);
                context.SaveChanges();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long Create(int Id)
        {
            Stopwatch watch = new Stopwatch();
            Player player = new Player()
            {
                Id = 130999 * Id,
                FirstName = "The",
                LastName = "Tessen",
                DateOfBirth = DateTime.Now,
                TeamId = 1
            };
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
            {
                context.Players.Add(player);
                context.SaveChanges();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
}