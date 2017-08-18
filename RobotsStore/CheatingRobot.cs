using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore.Robots
{
    public class CheatingRobot : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "Cheating Robot";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            competitors.ForEach(c =>
            {
                c.Attacks = 1000000;
            });
            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
