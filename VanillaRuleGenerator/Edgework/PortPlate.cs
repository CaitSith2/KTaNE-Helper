using System;
using System.Collections.Generic;

namespace VanillaRuleGenerator.Edgework
{
    public class PortPlate
    {
        private List<string> _presentPorts = new List<string>();
        public string[] PresentPorts => _presentPorts.ToArray();
        public PortPlate(string text)
        {
            var split = text.ToLowerInvariant().Trim().Split(new[] { ",", ":", " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var port in split)
            {
                switch (port)
                {
                    case "parallel":
                    case "pp":
                        _presentPorts.Add(PortTypes.Parallel.ToString());
                        break;
                    case "serial":
                    case "sp":
                        _presentPorts.Add(PortTypes.Serial.ToString());
                        break;
                    case "dvi":
                    case "dvid":
                    case "dvi-d":
                        _presentPorts.Add(PortTypes.DVI.ToString());
                        break;
                    case "ps2":
                    case "ps/2":
                        _presentPorts.Add(PortTypes.PS2.ToString());
                        break;
                    case "rj":
                    case "rj45":
                    case "rj-45":
                        _presentPorts.Add(PortTypes.RJ45.ToString());
                        break;
                    case "rca":
                    case "stereo":
                    case "stereo-rca":
                        _presentPorts.Add(PortTypes.StereoRCA.ToString());
                        break;

                    case "ac":
                    case "power":
                        _presentPorts.Add(PortTypes.AC.ToString());
                        break;
                    case "pcmcia":
                    case "pccard":
                        _presentPorts.Add(PortTypes.PCMCIA.ToString());
                        break;
                    case "hdmi":
                        _presentPorts.Add(PortTypes.HDMI.ToString());
                        break;
                    case "composite":
                    case "compositevideo":
                    case "rcavideo":
                        _presentPorts.Add(PortTypes.CompositeVideo.ToString());
                        break;
                    case "component":
                    case "componentvideo":
                        _presentPorts.Add(PortTypes.ComponentVideo.ToString());
                        break;
                    case "vga":
                        _presentPorts.Add(PortTypes.VGA.ToString());
                        break;
                    case "usb":
                        _presentPorts.Add(PortTypes.USB.ToString());
                        break;
                }
            }
        }

        public static List<PortPlate> portPlates = new List<PortPlate>();
        private static KMBombInfo _bombInfo = new KMBombInfo();

        public static void ParsePortPlates(string plates)
        {
            var split = plates.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            portPlates.Clear();
            foreach (var platetxt in split)
            {
                var plate = new PortPlate(platetxt);
                portPlates.Add(plate);
            }
        }

        public static bool IsPortPresent(PortTypes port)
        {
            return _bombInfo.IsPortPresent(port.ToString());
        }

        public static int PortCount(PortTypes port)
        {
            return port == PortTypes.ALL ? _bombInfo.GetPortCount() : _bombInfo.GetPortCount(port.ToString());
        }

        public static bool DuplicatePorts(PortTypes port = PortTypes.ALL)
        {
            return port != PortTypes.ALL ? _bombInfo.IsDuplicatePortPresent(port.ToString()) : _bombInfo.IsDuplicatePortPresent();
        }

        public static int CountUniquePorts()
        {
            return _bombInfo.CountUniquePorts();
        }

        public static int CountTotalPorts()
        {
            return _bombInfo.GetPortCount();
        }

    }

    [Flags]
    public enum PortTypes
    {
        None = 0,

        Serial = 1,
        Parallel = 2,

        DVI = 4,
        PS2 = 8,
        RJ45 = 16,
        StereoRCA = 32,

        HDMI = 64,
        USB = 128,
        ComponentVideo = 256,

        AC = 512,
        PCMCIA = 1024,
        VGA = 2048,
        CompositeVideo = 4096,

        ALL = Int32.MaxValue, 
    }
}