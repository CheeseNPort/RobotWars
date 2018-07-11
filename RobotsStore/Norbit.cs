using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
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
            long possibleAttacks = 10;
            var newVictim = competitors.OrderBy(c => c.Health).FirstOrDefault();
            competitors.ForEach(c =>
            {
                if (c.Name == "TwoLukesAreBetterThanOne" && c.Health > Health)
                {
                    c.Attacks = possibleAttacks;
                    possibleAttacks = 0;
                }
                /*   if (c.Name == "CheatingRobot" && c.Health > Health)
                    {
                        !c.Attacks
                    }*/
                if (c.Health > Health && c.Name == newVictim.Name)
                {
                    newVictim.Attacks = possibleAttacks;
                    possibleAttacks = 0;
                }
            });

            var victim = competitors.OrderByDescending(c => c.Health).FirstOrDefault();
            victim.Attacks = possibleAttacks;
            return competitors;
        }

        public void UpdateHealth(long health)
        {
            Health = health;
        }
    }
}
