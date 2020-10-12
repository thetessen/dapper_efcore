using DapperEFCorePerformanceBenchmarks.DataAccess;
using DapperEFCorePerformanceBenchmarks.Models;
using DapperEFCorePerformanceBenchmarks.TestData;
using Microsoft.EntityFrameworkCore;
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
            var player = context.Players.First(x=>x.Id == id);
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
    public long Create()
    {
        Stopwatch watch = new Stopwatch();
        Sport sport = new Sport() {
            Id = 11,
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
    public long Update()
    {
        Stopwatch watch = new Stopwatch();
        
        watch.Start();
        using (SportContextEfCore context = new SportContextEfCore(Database.GetOptions()))
        {
            var entity = context.Sports.Where(x => x.Id == 11)
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
            var entity = context.Sports.Where(x => x.Id == 11)
                .SingleOrDefault();
            context.Sports.Remove(entity);
            context.SaveChanges();
        }
        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
}