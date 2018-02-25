using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace VanillaRuleGenerator.Edgework
{
    public class KMBombInfo
    {
        public static string QUERYKEY_GET_SERIAL_NUMBER = "serial";
        public static string QUERYKEY_GET_BATTERIES = "batteries";
        public static string QUERYKEY_GET_INDICATOR = "indicator";
        public static string QUERYKEY_GET_PORTS = "ports";

        private class IndicatorJSON
        {
            public string label = null;
            public string on = null;

            public bool IsOn()
            {
				bool.TryParse(on, out bool isOn);
				return isOn;
            }
        }

        private class ColorIndicatorJSON
        {
            public string label = null;
            public string color = null;
        }

        private class BatteryJSON
        {
            public int numbatteries = 0;
        }

        private class PortsJSON
        {
            public string[] presentPorts = null;
        }

        private class SerialNumberJSON
        {
            public string serial = null;
        }

        public List<string> QueryWidgets(string queryKey, string queryInfo)
        {
            var list = new List<string>();

            switch (queryKey)
            {
                case "batteries":
                    //list.AddRange(Batteries.Holders.Select(battery => JsonConvert.SerializeObject(new BatteryJSON {numbatteries = battery})));
                    foreach (var battery in Batteries.Holders)
                    {
                        list.Add(JsonConvert.SerializeObject(new BatteryJSON { numbatteries = battery }));
                    }
                    break;

                case "indicator":
                    list.AddRange(Indicators.LitIndicators.Select(indicator => JsonConvert.SerializeObject(new IndicatorJSON() {@on = string.Empty + true, label = indicator})));
                    list.AddRange(Indicators.UnlitIndicators.Select(indicator => JsonConvert.SerializeObject(new IndicatorJSON() {@on = string.Empty + false, label = indicator })));
                    break;

                case "indicatorColor":
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Black  ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Black.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.White  ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.White.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Red    ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Red.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Orange ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Orange.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Yellow ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Yellow.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Green  ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Green.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Blue   ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Blue.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Purple ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Purple.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Magenta).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Magenta.ToString(), label = indicator })));
                    list.AddRange(Indicators.GetColoredIndicator((int)KMBombInfoExtensions.KnownIndicatorColors.Gray   ).Select(indicator => JsonConvert.SerializeObject(new ColorIndicatorJSON() { color = KMBombInfoExtensions.KnownIndicatorColors.Gray.ToString(), label = indicator })));
                    break;

                case "ports":
                    list.AddRange(PortPlate.portPlates.Select(plate => JsonConvert.SerializeObject(new PortsJSON {presentPorts = plate.PresentPorts})));
                    break;

                case "serial":
                    list.Add(JsonConvert.SerializeObject(new SerialNumberJSON {serial = SerialNumber.Serial}));
                    break;
            }

            return list;
        }

        private static int _strikes;

        public static void SetStrikes(int strikes)
        {
            _strikes = strikes;
        }

        public int GetStrikes()
        {
            return _strikes;
        }
    }
}