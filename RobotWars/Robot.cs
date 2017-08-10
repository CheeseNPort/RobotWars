using System;
using System.Collections.Generic;
using System.Text;

namespace RobotWars
{
    public class Robot
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public IRobot RobotImplementation { get; set; }
        public Int64 Health { get; set; }
        public DateTime LastTurn { get; set; }
    }
}
