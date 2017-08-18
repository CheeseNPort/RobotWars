using RobotsStore.Robots;
using RobotWars;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsStore
{
    public class CompetitorsFactory
    {
        public static List<IRobot> GetCompetitors()
        {
            return new List<IRobot>()
            {
                    new BullyRobot(),
                    new CheatingRobot(),
                    new CompassionateRobot(),
                    new LazyRobot(),
                    new StupidRobot(),
                    new VeryStupidRobot()
            };
        }
    }
}
