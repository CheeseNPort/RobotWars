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
            Int64 attacks = 10;
            attacks = Attack("TwoLukesAreBetterThanOne Robot", competitors, attacks);
            attacks = Attack("Scythe Robot", competitors, attacks);
            attacks = Attack("Norbit", competitors, attacks);
            attacks = Attack("Lazy Robot", competitors, attacks);
            attacks = Attack("Stupid Robot", competitors, attacks);
            attacks = Attack("Very Stupid Robot", competitors, attacks);
            attacks = Attack("Cheating Robot", competitors, attacks);
            return competitors;
        }

        private Int64 Attack(String name, List<RobotAction> robots, Int64 attacks)
        {
            if (attacks > 0)
            {
                var robot = robots.SingleOrDefault(r => r.Name == name);
                if (robot != null)
                {
                    if (robot.Health > attacks)
                    {
                        robot.Attacks = attacks;
                    }
                    else
                    {
                        robot.Attacks = robot.Health;
                    }
                    return attacks = robot.Attacks;
                }
            }
            return attacks;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
