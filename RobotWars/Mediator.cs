using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace RobotWars
{
    public class Mediator
    {
        public List<Robot> Robots { get; set; } = new List<Robot>();
        public Int64 TurnCounter { get; set; } = 0;

        public Mediator(List<IRobot> robots)
        {
            Robots = robots.Select(r => new Robot
            {
                Health = 100,
                RobotImplementation = r,
                Id = Guid.NewGuid(),
                LastTurn = DateTime.Now,
                Name = r.GetName()
            }).ToList();
            Robots.ForEach(r =>
            {
                if (Robots.Count(b => b.Name == r.Name) > 1)
                {
                    throw new Exception("Duplicate robots exist.");
                }
            });
            RandomizeStartOrder();
        }

        public void RandomizeStartOrder()
        {
            var tempRobots = Robots.Select(r => r).ToList();
            Robots.Clear();

            var random = new Random();
            while (tempRobots.Count > 0)
            {
                var min = 0;
                var max = tempRobots.Count;
                var index = random.Next(min, max);
                Robots.Add(tempRobots[index]);
                tempRobots.Remove(tempRobots[index]);
            }
        }

        public void NextTurn()
        {
            if (TurnCounter == Robots.Count)
            {
                Robots.ForEach(r => { r.LastTurn = new DateTime(1900, 1, 1); });
                RandomizeStartOrder();
            }
            var robot = Robots.Where(r => r.Health > 0).OrderBy(r => r.LastTurn).FirstOrDefault();
            var competitors = Robots.Where(r => r.Health > 0 && r.Id != robot.Id).Select(r => new RobotAction
            {
                Health = r.Health,
                Name = r.RobotImplementation.GetName(),
                Attacks = 0,
                Id = r.Id
            }).ToList();
            var attacks = new List<RobotAction>();
            try
            {
                attacks = robot.RobotImplementation.MyTurn(competitors);
                if (attacks.Sum(attack => attack.Attacks) > 10)
                {
                    // Tough luck, robot tried to cheat so forefits their turn
                    attacks.Clear();
                }
            }
            catch (Exception)
            {
                // Tough luck, there was an error so the robot forefits their turn.
            }
            attacks.ForEach(attack =>
            {
                var robotToAttack = Robots.SingleOrDefault(r => r.Id == attack.Id);
                if (robotToAttack != null)
                {
                    robotToAttack.Health = robotToAttack.Health - attack.Attacks;
                }
                else
                {
                    // Tough luck, robot tried to manipulate the id's, cheating so firefits this attack
                }
            });

            Robots.ForEach(r =>
            {
                r.RobotImplementation.UpdateHealth(r.Health);
            });

            robot.LastTurn = DateTime.Now;

            TurnCounter++;
        }
    }
}
