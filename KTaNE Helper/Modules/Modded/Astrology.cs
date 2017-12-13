using System.Collections.Generic;
using System.Linq;
using KTaNE_Helper.Edgework;

namespace KTaNE_Helper.Modules.Modded
{
    public class Astrology
    {
        private readonly List<string> _elements = new List<string> {"fire", "water", "earth", "air"};
        private readonly List<string> _planets = new List<string>
        {
            "sun","moon","mercury","venus","mars",
            "jupiter","saturn","uranus","neptune","pluto"
        };
        private readonly List<string> _signs = new List<string>
        {
            "aries","leo","sagittarius",
            "taurus","virgo","capricorn",
            "gemini","libra","aquarius",
            "cancer","scorpio","pisces"
        };

        private readonly int[][] _elementPlanets = new int[4][] {
            new int[10] {0,0,1,-1,0,1,-2,2,0,-1},
            new int[10] {-2,0,-1,0,2,0,-2,2,0,1},
            new int[10] {-1,-1,0,-1,1,2,0,2,1,-2},
            new int[10] {-1,2,-1,0,-2,-1,0,2,-2,2}
        };

        private readonly int[][] _elementSigns = new int[4][]
        {
            new int[12] {1,0,-1,0,0,2,2,0,1,0,1,0},
            new int[12] {2,2,-1,2,-1,-1,-2,1,2,0,0,2},
            new int[12] {-2,-1,0,0,1,0,1,2,-1,-2,1,1},
            new int[12] {1,1,-2,-2,2,0,-1,1,0,0,-1,-1}
        };

        private readonly int[][] _planetSigns = new int[10][]
        {
            new int[12] {-1,-1,2,0,-1,0,-1,1,0,0,-2,-2},
            new int[12] {-2,0,1,0,2,0,-1,1,2,0,1,0},
            new int[12] {-2,-2,-1,-1,1,-1,0,-2,0,0,-1,1},
            new int[12] {-2,2,-2,0,0,1,-1,0,2,-2,-1,1},
            new int[12] {-2,0,-1,-2,-2,-2,-1,1,1,1,0,-1},
            new int[12] {-1,-2,1,-1,0,0,0,1,0,-1,2,0},
            new int[12] {-1,-1,0,0,1,1,0,0,0,0,-1,-1},
            new int[12] {-1,2,0,0,1,-2,1,0,2,-1,1,0},
            new int[12] {1,0,2,1,-1,1,1,1,0,-2,2,0},
            new int[12] {-1,0,0,-1,-2,1,2,1,1,0,0,-1}
        };

        public string GetOmen(int element, int planet, int sign)
        {
            var serial = SerialNumber.Serial.ToLowerInvariant();
            var omen = 0;
            omen += _elementPlanets[element][planet];
            omen += _elementSigns[element][sign];
            omen += _planetSigns[planet][sign];
            omen += _elements[element].ToCharArray().Aggregate(false, (current, x) => current | serial.Contains(x.ToString())) ? 1 : -1;
            omen += _planets[planet].ToCharArray().Aggregate(false, (current, x) => current | serial.Contains(x.ToString())) ? 1 : -1;
            omen += _signs[sign].ToCharArray().Aggregate(false, (current, x) => current | serial.Contains(x.ToString())) ? 1 : -1;

            if (omen == 0) return "No Omen";
            if (omen > 0) return "Good Omen on " + omen;
            return "Bad Omen on " + (omen*-1);

        }

    }
}