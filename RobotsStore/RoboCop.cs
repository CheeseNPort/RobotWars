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
                    robotsToIgnore = new List<String>();
                }

                var rnd = new Random();
                if (competitors.Count > 2)
                {
                    var index = rnd.Next(competitors.Count - 3);
                    competitors[index + 1].Attacks = attacks;
                    attacks = 0;
                }
                else
                {
                    var index = rnd.Next(competitors.Count - 1);
                    competitors[index].Attacks = attacks;
                    attacks = 0;
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
