using RobotWars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotWarsConsole.Robots
{
    public class Michael : IRobot
    {
        public Int64 Health { get; set; }

        public string GetName()
        {
            return "Michael";
        }

        public List<RobotAction> MyTurn(List<RobotAction> competitors)
        {
            var pointsRemaining = 10;
            competitors = competitors.OrderByDescending(c => c.Health).ToList();
            while(pointsRemaining > 0)
            {
                competitors.ForEach(c =>
                {
                    if(pointsRemaining > 0)
                    {
                        c.Attacks++;
                        pointsRemaining--;
                    }
                });
            }
            return competitors;
        }

        public void UpdateHealth(long health)
        {
            Health = health;
        }
    }
}
