﻿using System;
using System.Collections.Generic;
using System.Linq;
using KTaNE_Helper.Helpers;
using static KTaNE_Helper.PortPlate;
using static KTaNE_Helper.SerialNumber;

namespace KTaNE_Helper
{
    public class TwoBits
    {
        private TwoBits()
        {
            for (var i = 0; i < 100; i++)
            {
                responses.Add(i);
                queries.Add($"{buttonLabels[i / 10]}{buttonLabels[i % 10]}");
            }
            var rand = new MonoRandom(0);
            responses.Shuffle(rand);

            for (var i = 0; i < 100; i++)
            {
                queryLookups.Add(responses[i], queries[i]);
            }
        }

        Dictionary<int, string> queryLookups = new Dictionary<int, string>();
        List<int> responses = new List<int>();
        List<string> queries = new List<string>();

        private static TwoBits _instance;
        public static TwoBits Instance => _instance ?? (_instance = new TwoBits());

        private KMBombInfo bombInfo = new KMBombInfo();

        public string TwoBitsLookup(int number)
        {
            return queryLookups.ContainsKey(number) ? queryLookups[number] : "";
        }

        protected static char[] buttonLabels = new char[] { 'b', 'c', 'd', 'e', 'g', 'k', 'p', 't', 'v', 'z' };

        public string CalculateInitialTwoBitsCode()
        {
            var batts = Batteries.TotalBatteries;
            if (!IsSerialValid)
            {
                return "";
            }
            var dict = new Dictionary<string, int>
            {
                {"0", 0},{"1", 0},{"2", 0},{"3", 0},{"4", 0},{"5", 0},
                {"6", 0},{"7", 0},{"8", 0},{"9", 0},{"A", 1},{"B", 2},
                {"C", 3},{"D", 4},{"E", 5},{"F", 6},{"G", 7},{"H", 8},
                {"I", 9},{"J", 10},{"K", 11},{"L", 12},{"M", 13},{"N", 14},
                {"O", 15},{"P", 16},{"Q", 17},{"R", 18},{"S", 19},{"T", 20},
                {"U", 21},{"V", 22},{"W", 23},{"X", 24},{"Y", 25},{"Z", 26},
            };

            var initial = 0;
            for (var i = 0; i < Serial.Length; i++)
            {
                if (dict[Serial.Substring(i, 1).ToUpper()] <= 0) continue;
                initial = dict[Serial.Substring(i, 1).ToUpper()];
                break;
            }
            initial += (batts * bombInfo.GetSerialNumberNumbers().Last());
            if (IsPortPresent(PortTypes.StereoRCA) && !IsPortPresent(PortTypes.RJ45))
                initial *= 2;
             return TwoBitsLookup(initial % 100) + @" / " + (initial % 100);

        }
    }
}

public static class Extensions
{
    public static void Shuffle<T>(this IList<T> list, MonoRandom random)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}