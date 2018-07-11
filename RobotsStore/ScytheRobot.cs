using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore.Robots
{
    public class ScytheRobot : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "Scythe Robot";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            if (competitors.Where(c => c.Health < 50).FirstOrDefault() != null)
            {
                var victim = competitors.Where(c => c.Health < 50).OrderByDescending(d => d.Health).FirstOrDefault();
                victim.Attacks = 10;
            }
            else
            {
                var victim = competitors.FirstOrDefault();
                victim.Attacks = 10;
            }

            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
