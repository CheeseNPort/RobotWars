using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore
{
    public class Norbit : IRobot
    {
        private long attacks;

        public Int64 Health { get; set; }
        public string GetName()
        {
            return "Norbit";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            var random = new Random();
            var victim = competitors[random.Next(0, competitors.Count - 1)];
            victim.Attacks = 10;

            if (victim.Name != "Cheating Robot" || competitors.Count(c => c.Health > 0) == 1)
            {
                victim.Attacks = attacks;

            }
            return competitors;
        }
    


        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
