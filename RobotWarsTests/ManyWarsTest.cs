using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RobotWars;
using System.Linq;
using RobotWarsTests.Robots;

namespace RobotWarsTests
{
    [TestClass]
    public class ManyWarsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var results = new List<WarResults>()
            {
                new WarResults { Name = "Bully Robot", Wins = 0},
                new WarResults { Name = "Cheating Robot", Wins = 0},
                new WarResults { Name = "Compassionate Robot", Wins = 0},
                new WarResults { Name = "Lazy Robot", Wins = 0},
                new WarResults { Name = "Michael", Wins = 0},
                new WarResults { Name = "Stupid Robot", Wins = 0},
                new WarResults { Name = "Very Stupid Robot", Wins = 0}
            };

            for (int i = 0; i < 1000; i++)
            {
                var robotsOut = new List<Guid>();
                var war = new Mediator(new List<IRobot>()
                {
                    new BullyRobot(),
                    new CheatingRobot(),
                    new CompassionateRobot(),
                    new LazyRobot(),
                    new Michael(),
                    new StupidRobot(),
                    new VeryStupidRobot()
                });

                while (war.Robots.Count(robot => robot.Health > 0) > 1)
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

                results.SingleOrDefault(r => r.Name == war.Robots.SingleOrDefault(b => b.Health > 0).Name).Wins++;
            }

            var a = results;
        }
    }

    public class WarResults
    {
        public String Name { get; set; }
        public Int64 Wins { get; set; }
    }
}
