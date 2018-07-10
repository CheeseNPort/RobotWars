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

            // Initialize the robots with 100 health each
            Robots = robots.Select(r => new Robot
            {
                Health = 100,
                RobotImplementation = r,
                Id = Guid.NewGuid(),
                LastTurn = DateTime.Now,
                Name = r.GetName()
            }).ToList();

            // Ensure there are no duplicate robots
            if(Robots.Any(r => Robots.Count(b => b.Name == r.Name) > 1))
            {
                throw new Exception("Duplicate robots exist.");
            }

            // Ensures that the starting order of the robots is randomised each time.
            RandomizeStartOrder();
        }

        // Randomises the start order of the robots
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
            // If it is the first turn in a round, randomize the start order
            if(TurnCounter % Robots.Count == 0)
            {
                Robots.ForEach(r => { r.LastTurn = new DateTime(1900, 1, 1); });
                RandomizeStartOrder();
            }

            // Get the first robot whose health is greater than zero and it has been the longest since their last turn
            var robot = Robots.Where(r => r.Health > 0).OrderBy(r => r.LastTurn).FirstOrDefault();

            // Get the robots competitors, these are those whose health is greater than zero and not itself
            var competitors = Robots.Where(r => r.Health > 0 && r.Id != robot.Id).Select(r => new RobotAction
            {
                Health = r.Health,
                Name = r.RobotImplementation.GetName(),
                Attacks = 0,
                Id = r.Id
            }).ToList();

            // Generate a new list if attacks, and make attacks based on the bobots own implementation
            var attacks = new List<RobotAction>();
            try
            {
                attacks = robot.RobotImplementation.MyTurn(competitors);
                if(attacks.Sum(attack => attack.Attacks) > 10)
                {
                    // Tough luck, robot tried to cheat so forefits their turn
                    attacks.Clear();
                }
            }
            catch (Exception)
            {
                // Tough luck, there was an error so the robot forefits their turn.
            }

            // Process each attack
            attacks.ForEach(attack =>
            {
                var robotToAttack = Robots.SingleOrDefault(r => r.Id == attack.Id);
                if(robotToAttack != null)
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
                // Push the new health value to the implementation
                r.RobotImplementation.UpdateHealth(r.Health);
            });

            robot.LastTurn = DateTime.Now;

            TurnCounter++;
        }
    }
}
