using System.Collections.Generic;
using System.Linq;
using VanillaRuleGenerator.Edgework;
using VanillaRuleGenerator.Helpers;
using VanillaRuleGenerator.Extensions;

namespace KTaNE_Helper.Modules.Modded
{
    public class TwoBits
    {
        private TwoBits()
        {
            for (var i = 0; i < 100; i++)
            {
                _responses.Add(i);
                _queries.Add($"{ButtonLabels[i / 10]}{ButtonLabels[i % 10]}");
            }
            var rand = new MonoRandom(0);
            _responses.Shuffle(rand);

            for (var i = 0; i < 100; i++)
            {
                _queryLookups.Add(_responses[i], _queries[i]);
            }
        }

	    private readonly Dictionary<int, string> _queryLookups = new Dictionary<int, string>();
	    private readonly List<int> _responses = new List<int>();
	    private readonly List<string> _queries = new List<string>();

        private static TwoBits _instance;
        public static TwoBits Instance => _instance ?? (_instance = new TwoBits());

        private readonly KMBombInfo _bombInfo = new KMBombInfo();

        public string TwoBitsLookup(int number)
        {
            return _queryLookups.ContainsKey(number) ? _queryLookups[number] : "";
        }

        protected static char[] ButtonLabels = new char[] { 'b', 'c', 'd', 'e', 'g', 'k', 'p', 't', 'v', 'z' };

        public string CalculateInitialTwoBitsCode()
        {
            var batts = Batteries.TotalBatteries;
            if (!SerialNumber.IsSerialValid)
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
            for (var i = 0; i < SerialNumber.Serial.Length; i++)
            {
                if (dict[SerialNumber.Serial.Substring(i, 1).ToUpper()] <= 0) continue;
                initial = dict[SerialNumber.Serial.Substring(i, 1).ToUpper()];
                break;
            }
            initial += (batts * _bombInfo.GetSerialNumberNumbers().Last());
            if (PortPlate.IsPortPresent(PortTypes.StereoRCA) && !PortPlate.IsPortPresent(PortTypes.RJ45))
                initial *= 2;
            return TwoBitsLookup(initial % 100) + @" / " + (initial % 100);

        }
    }
}