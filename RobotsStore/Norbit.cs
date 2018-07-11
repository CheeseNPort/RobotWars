using RobotWars;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsStore
{
    public class Norbit : IRobot
    {
        public Int64 Health { get; set; }
        public string GetName()
        {
            return "Norbit";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            competitors.ForEach(c =>
            {
                c.Attacks = 3;
            });
            return competitors;
        }

        public void UpdateHealth(long health)
        {
            Health = health;
        }
    }
}
