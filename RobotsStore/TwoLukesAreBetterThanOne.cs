using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsStore.Robots
{
    public class TwoLukesAreBetterThanOne : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "TwoLukesAreBetterThanOne";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            long possibleAttacks = 10;
            var newVictim = competitors.OrderBy(c => c.Health).FirstOrDefault();
            competitors.ForEach(c =>
            {
                if (c.Name == "Compassionate Robot" && c.Health > Health)
                {
                    c.Attacks = possibleAttacks;
                    possibleAttacks = 0;
                }
                if(c.Health > Health && c.Name == newVictim.Name)
                {
                    newVictim.Attacks = possibleAttacks;
                    possibleAttacks = 0;
                }
            });
            var weakVictim = competitors.OrderBy(c => c.Health).FirstOrDefault();
            var victim = competitors.OrderByDescending(c => c.Health).FirstOrDefault();
            if(victim.Name != "Cheating Robot")
            {
                victim.Attacks = possibleAttacks;
                return competitors;
            }
            else
            {
                weakVictim.Attacks = possibleAttacks;
                return competitors;
            }
           
            
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}