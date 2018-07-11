using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore.Robots
{
    public class RoboCop : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "RoboCop";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            var random = new Random();
            var humanRobots = competitors.Where(c => (c.Name == "TwoLukesAreBetterThanOne Robot"
            || c.Name == "Norbit"
            || c.Name == "Scythe Robot") && c.Health > 0);
            if(humanRobots.Any())
            {
                var victim = humanRobots.ToList()[random.Next(0, humanRobots.Count() - 1)];
                var victimRobot = humanRobots.SingleOrDefault(r => r.Name == victim.Name);
                victimRobot.Attacks = 30;
            }
            else
            {
                var computerRobots = competitors.Where(c => (c.Name != "Cheating Robot" && c.Health > 0));
                if (computerRobots.Any())
                {
                    var victim = computerRobots.ToList()[random.Next(0, computerRobots.Count() - 1)];
                    var victimRobot = computerRobots.SingleOrDefault(r => r.Name == victim.Name);
                    victimRobot.Attacks = 30;
                }
                else
                {
                    var otherRobots = competitors.Where(c =>  c.Health > 0);
                    if (otherRobots.Any())
                    {
                        var victim = otherRobots.ToList()[random.Next(0, otherRobots.Count() - 1)];
                        var victimRobot = otherRobots.SingleOrDefault(r => r.Name == victim.Name);
                        victimRobot.Attacks = 30;
                    }
                }
            }
            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
