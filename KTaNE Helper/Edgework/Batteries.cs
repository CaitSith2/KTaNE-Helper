using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace KTaNE_Helper
{
    public class Batteries
    {
        private static readonly List<int> BatteryHolders = new List<int>();
        public static int[] Holders => BatteryHolders.ToArray();

        public static int TotalBatteries => Holders.Sum();

        public static int DBatteries => Holders.Count(battery => battery == 1);
        public static int AABatteries => Holders.Count(battery => battery == 2) * 2;

        public static void SetBatteries(string batteries)
        {
            BatteryHolders.Clear();
            var split = batteries.Split(new[] {" ", ",", ":", Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var battery in split)
            {
                int batteryInt;
                if(int.TryParse(battery,out batteryInt))
                    BatteryHolders.Add(batteryInt);
            }
        }
    }
}