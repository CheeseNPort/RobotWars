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
            const int attacks = 30;
            var attacksLeft = attacks;
            var allCompetitors = competitors;
            competitors = competitors.Where(c => c.Name == "Lazy Robot" || c.Name == "Norbit" || c.Name == "TwoLukesAreBetterThanOne Robot" || c.Name == "RoboCop").ToList();
            if (competitors.Where(c => c.Health < 50).FirstOrDefault() != null)
            {
                victim = competitors.Where(c => c.Health <= attacks).OrderBy(d => d.Health).FirstOrDefault();
                if (victim != null)
                {
                    if (competitors.Where(c => c.Health >= victim.Health && c.Health < Health).OrderBy(d => d.Health).FirstOrDefault() != null)
                    {
                        if (victim.Health < attacks)
                        {
                            victim.Attacks = victim.Health;
                            attacksLeft -= (int)victim.Health;
                        }
                        else
                        {
                            victim.Attacks = attacksLeft;
                            attacksLeft = 0;
                        }
                    }
                }
                else
                {
                    victim = competitors.Where(c => c.Health > attacks && c.Health <= attacks * 2).OrderBy(d => d.Health).FirstOrDefault();
                    if (victim != null)
                    {

                        victim.Attacks = victim.Health - attacks;
                        attacksLeft -= (int)victim.Attacks;
                    }
                }
            }

            victim = competitors.OrderByDescending(c => c.Health).FirstOrDefault();

            if (victim == null)
            {
                victim = allCompetitors.OrderByDescending(c => c.Health).FirstOrDefault();
            }

            victim.Attacks = attacksLeft;

            return competitors;
        }

        public void UpdateHealth(Int64 health)
        {
            Health = health;
        }
    }
}
