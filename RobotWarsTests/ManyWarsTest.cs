using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RobotWars;
using System.Linq;
using RobotsStore.Robots;
using RobotsStore;

namespace RobotWarsTests
{
    [TestClass]
    public class ManyWarsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var robots = CompetitorsFactory.GetCompetitors();

            var results = robots.Select(r => new WarResults
            {
                Name = r.GetName(),
                Wins = 0
            }).ToList();

            for (int i = 0; i < 20000; i++)
            {
                var robotsOut = new List<Guid>();
                var war = new Mediator(robots);

                while (war.Robots.Count(robot => robot.Health > 0) > 1 && war.TurnCounter < 10000)
                {
                    war.NextTurn();
                    war.Robots.OrderBy(robot => robot.Name).ToList().ForEach(robot =>
                    {
                        if (robot.Health <= 0)
                        {
                            if (!robotsOut.Contains(robot.Id))
                            {
                                robotsOut.Add(robot.Id);
                            }
                        }
                    });
                }

                var winningRobot = war.Robots.OrderByDescending(b => b.Health).Where(b => b.Health > 0).FirstOrDefault();
                results.SingleOrDefault(r => r.Name == winningRobot.Name).Wins++;
            }

            results = results.OrderByDescending(r => r.Wins).ToList();
        }
    }

    public class WarResults
    {
        public String Name { get; set; }
        public Int64 Wins { get; set; }
    }
}
