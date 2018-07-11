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
            return "TwoLukesAreBetterThanOne Robot";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            var robotsToIgnore = new List<String>
            {
                "Cheating Robot", "Very Stupid Robot", "Stupid Robot"
            };


            long possibleAttacks = 10;
            var newVictim = competitors.OrderBy(c => c.Health).FirstOrDefault();
            competitors.ForEach(c =>
            {
             
                if (c.Health > Health && c.Name == newVictim.Name && Math.Abs(c.Health - Health) >= 29)
                {
                    newVictim.Attacks = possibleAttacks;
                    possibleAttacks = 0;
                }
            });
            var weakVictim = competitors.OrderBy(c => c.Health).FirstOrDefault();
            var victim = competitors.OrderByDescending(c => c.Health).FirstOrDefault();


            if (weakVictim.Health <= 30)
            {
                long tempAttack = weakVictim.Health;
                
                weakVictim.Attacks = possibleAttacks;
                possibleAttacks = possibleAttacks - tempAttack;
            }

            var i = 0;
            competitors.ForEach(c =>
            {
                i++;
            });
            if(!robotsToIgnore.Contains(victim.Name) && i != 3)
            {
                victim.Attacks = possibleAttacks;
                return competitors;
            }
            else
            {
                victim.Attacks = possibleAttacks;
                return competitors;
            }
           
            
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}