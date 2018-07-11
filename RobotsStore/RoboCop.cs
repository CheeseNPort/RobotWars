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
            var robotsToIgnore = new List<String>
            {
                "Cheating Robot", "Very Stupid Robot", "Stupid Robot"
            };
            var attacks = 10;
            var previousAttacks = attacks;
            while (attacks > 0)
            {
                if (previousAttacks == attacks)
                {
                    competitors.ForEach(c =>
                    {
                        c.Attacks++;
                        attacks--;
                    });
                }
                else
                {
                    competitors.ForEach(c =>
                    {
                        if (!robotsToIgnore.Contains(c.Name))
                        {
                            c.Attacks++;
                            attacks--;
                        }
                    });
                }
                previousAttacks = attacks;
            }
            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
