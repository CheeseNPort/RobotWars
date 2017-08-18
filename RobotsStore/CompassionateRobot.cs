using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore.Robots
{
    public class CompassionateRobot : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "Compassionate Robot";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            var victim = competitors.OrderByDescending(c => c.Health).FirstOrDefault();
            victim.Attacks = 10;
            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
