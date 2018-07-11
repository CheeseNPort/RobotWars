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
            RobotAction victim;
            var attacksLeft = 10;
            if (competitors.Where(c => c.Health < 50).FirstOrDefault() != null)
            {
                victim = competitors.Where(c => c.Health < 50).OrderByDescending(d => d.Health).FirstOrDefault();
                if (victim.Health < 10)
                {
                    victim.Attacks = victim.Health;
                }
                else
                {
                    victim.Attacks = attacksLeft;
                }
                attacksLeft -= (int)victim.Attacks;
            }
            if (competitors.OrderBy(c => c.Health).FirstOrDefault().Name == GetName())
            {
                victim = competitors.OrderBy(c => c.Health).Where(c => c.Name != GetName()).FirstOrDefault();
                if (victim.Health < 10)
                {
                    victim.Attacks = victim.Health;
                }
                else
                {
                    victim.Attacks = attacksLeft;
                }
                attacksLeft -= (int)victim.Attacks;
            }

            victim = competitors.OrderByDescending(c => c.Health).FirstOrDefault();
            victim.Attacks = attacksLeft;


            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
