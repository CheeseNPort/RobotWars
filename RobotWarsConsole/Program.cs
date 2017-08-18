using RobotsStore;
using RobotsStore.Robots;
using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotWarsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var robotsOut = new List<Guid>();
            Console.ForegroundColor = ConsoleColor.White;
            var war = new Mediator(CompetitorsFactory.GetCompetitors());
            while (war.Robots.Count(robot => robot.Health > 0) > 1)
            {
                war.NextTurn();
                war.Robots.OrderBy(robot => robot.Name).ToList().ForEach(robot =>
                {
                    if (robot.Health <= 0)
                    {
                        if (!robotsOut.Contains(robot.Id))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{robot.Name} is out! :(");
                            Console.ForegroundColor = ConsoleColor.White;
                            robotsOut.Add(robot.Id);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{robot.Name} health is {robot.Health.ToString()}");
                    }
                });
                Console.WriteLine("");
                System.Threading.Thread.Sleep(400);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{war.Robots.SingleOrDefault(r => r.Health > 0).Name} Wins! :)");
            Console.ReadLine();
        }
    }
}