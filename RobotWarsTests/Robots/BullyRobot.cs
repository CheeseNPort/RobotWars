using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotWarsTests.Robots
{
    public class BullyRobot : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "Bully Robot";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            var victim = competitors.OrderBy(c => c.Health).FirstOrDefault();
            victim.Attacks = 10;
            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
