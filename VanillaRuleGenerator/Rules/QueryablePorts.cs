using System.Collections.Generic;
using System.Linq;
using VanillaRuleGenerator.Edgework;
using VanillaRuleGenerator.Modules;

namespace VanillaRuleGenerator.Rules
{
    public class QueryablePorts
    {
        public static QueryableProperty HasParallelPort = new QueryableProperty
        {
            Name = "portXPresent",
            Text = "there is a parallel port present on the bomb",
            CompoundQueryOnly = true,
            QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => PortPlate.IsPortPresent(PortTypes.Parallel))
        };

        public static QueryableProperty HasSerialPort = new QueryableProperty
        {
            Name = "portXPresent",
            Text = "there is a serial port present on the bomb",
            CompoundQueryOnly = true,
            QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => PortPlate.IsPortPresent(PortTypes.Serial))
        };

        public static QueryableProperty HasRJ45Port = new QueryableProperty
        {
            Name = "portXPresent",
            Text = "there is a RJ-45 port present on the bomb",
            CompoundQueryOnly = true,
            QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => PortPlate.IsPortPresent(PortTypes.RJ45))
        };

        public static QueryableProperty HasPS2Port = new QueryableProperty
        {
            Name = "portXPresent",
            Text = "there is a PS2 port present on the bomb",
            CompoundQueryOnly = true,
            QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => PortPlate.IsPortPresent(PortTypes.PS2))
        };

        public static QueryableProperty HasDVIPort = new QueryableProperty
        {
            Name = "portXPresent",
            Text = "there is a DVI-D port present on the bomb",
            CompoundQueryOnly = true,
            QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => PortPlate.IsPortPresent(PortTypes.DVI))
        };

        public static QueryableProperty HasRCAPort = new QueryableProperty
        {
            Name = "portXPresent",
            Text = "there is a Stereo RCA port present on the bomb",
            CompoundQueryOnly = true,
            QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => PortPlate.IsPortPresent(PortTypes.StereoRCA))
        };

        public static QueryableProperty EmptyPortPlate = new QueryableProperty
        {
            Name = "emptyPlatePresent",
            Text = "there is an empty port plate present on the bomb",
            CompoundQueryOnly = true,
            QueryFunc = ((BombComponent comp, Dictionary<string, object> args) => IsEmptyPlatePresent())
        };

        public static QuerySet GetPortQueries()
        {
            return new QuerySet
            {
                QueryableProperties = new List<QueryableProperty>
                {
                    HasDVIPort,HasPS2Port,HasRJ45Port,HasRCAPort,HasParallelPort,HasSerialPort,EmptyPortPlate
                }
            };
        }

        public static bool IsEmptyPlatePresent()
        {
            return PortPlate.portPlates.Any(x => x.PresentPorts.Length == 0);
        }

        public static List<QueryableProperty> PortList = new List<QueryableProperty>
        {
            HasDVIPort,HasPS2Port,HasRJ45Port,HasRCAPort,HasParallelPort,HasSerialPort
        };
    }
}