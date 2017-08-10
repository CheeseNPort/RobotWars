using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RobotWars
{
    public class Mediator
    {
        public List<Robot> Robots { get; set; } = new List<Robot>();

        public Mediator()
        {
            var robots = Assembly.GetEntryAssembly().DefinedTypes.Where(type => type.ImplementedInterfaces.Contains(typeof(IRobot))).ToList();
            robots.ForEach(robot =>
            {
                var iRobot = (IRobot)Activator.CreateInstance(robot.AsType());
                Robots.Add(new Robot
                {
                    Health = 100,
                    RobotImplementation = iRobot,
                    Id = Guid.NewGuid(),
                    Name = iRobot.GetName(),
                    LastTurn = DateTime.Now
                });
            });
        }

        public void NextTurn()
        {
            var robot = Robots.OrderBy(r => r.LastTurn).FirstOrDefault();
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
                r.RobotImplementation.UpdateHealth(r.Health);
            });

            robot.LastTurn = DateTime.Now;
        }
    }
}
