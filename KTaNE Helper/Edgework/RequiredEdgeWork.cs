using System.Collections.Generic;

namespace KTaNE_Helper.Edgework
{
    public static class RequiredEdgeWork
    {
        public static List<RequiredEdgeWorkItem> ModuleEdgeWorkRequired = new List<RequiredEdgeWorkItem>
        {
            //About
            new RequiredEdgeWorkItem {ModuleName = "About", Nothing = true },

            //Vanilla Modules
            new RequiredEdgeWorkItem {ModuleName = "Complicated Wires", SerialNumber = true, Batteries = true, Ports = true},
            new RequiredEdgeWorkItem {ModuleName = "Keypads", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Mazes", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Memory", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Morse Code", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Passwords", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "The Button", Batteries = true, LitIndicators = true},
            new RequiredEdgeWorkItem {ModuleName = "Simon Says", SerialNumber = true, Strikes = true},
            new RequiredEdgeWorkItem {ModuleName = "Simple Wires", SerialNumber = true},
            new RequiredEdgeWorkItem {ModuleName = "Who's on First", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Wire Sequences", Nothing = true },

            //Mod bomb Modules
            new RequiredEdgeWorkItem {ModuleName = "3D Maze", LitIndicators = true, UnlitIndicators = true, SerialNumber = true},
            new RequiredEdgeWorkItem {ModuleName = "Adventure Game", Everything = true },
            new RequiredEdgeWorkItem {ModuleName = "Alphabet", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Anagram", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Astrology", SerialNumber = true },
            new RequiredEdgeWorkItem {ModuleName = "Blind Alley", Everything = true },
            new RequiredEdgeWorkItem {ModuleName = "The Bulb",  LitIndicators = true, UnlitIndicators = true},
            new RequiredEdgeWorkItem {ModuleName = "Caesar Cipher", Everything = true },
            new RequiredEdgeWorkItem {ModuleName = "Chess", SerialNumber = true},
            new RequiredEdgeWorkItem {ModuleName = "Combination Lock", TwoFactor = true, Batteries = true, UnsolvedModules = true, SolvedModules = true },
            new RequiredEdgeWorkItem {ModuleName = "Connection Check", SerialNumber = true },
            new RequiredEdgeWorkItem {ModuleName = "Cryptography", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Emoji Math", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Flashing Colors", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Forget Me Not", Everything = true },
            new RequiredEdgeWorkItem {ModuleName = "Murder", Everything = true },
            new RequiredEdgeWorkItem {ModuleName = "Perspective Pegs", SerialNumber = true, Batteries = true},
            new RequiredEdgeWorkItem {ModuleName = "Plumbing", Everything = true },
            new RequiredEdgeWorkItem {ModuleName = "Resistors",SerialNumber = true,Batteries = true,Ports = true,BatteryHolders = true},
            new RequiredEdgeWorkItem {ModuleName = "Safety Safe", SerialNumber = true, LitIndicators = true, UnlitIndicators = true },
            new RequiredEdgeWorkItem {ModuleName = "Word Scramble", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Switches", Nothing = true },
            new RequiredEdgeWorkItem {ModuleName = "Two Bits", Batteries = true, SerialNumber = true},
        };

        public static string GetRequiredEdgeWork(string name)
        {
            var text = "";
            foreach (var item in RequiredEdgeWork.ModuleEdgeWorkRequired)
            {
                if (item.ModuleName != name.Trim()) continue;
                if (item.Nothing)
                {
                    text += " - No Edgeword required for \"" + item.ModuleName + "\"";
                    break;
                }
                text += " - Edgework required for \"" + item.ModuleName + "\"";
                if (item.Everything) text += " - ALL Edgework Requried";
                else
                {
                    if (item.TwoFactor) text += ", Two Factor";
                    if (item.SerialNumber) text += ", Serial Number";
                    if (item.Batteries)
                    {
                        if (item.BatteryHolders) text += ", Batteries in Holders";
                        else text += ", Batteries";
                    }
                    if (item.LitIndicators && item.UnlitIndicators)
                    {
                        text += ", All Indicators";
                    }
                    else
                    {
                        if (item.LitIndicators) text += ", Lit Indicators";
                        if (item.UnlitIndicators) text += ", Unlit Indicators";
                    }

                    if (item.Ports && item.EmptyPortPlates)
                    {
                        text += ", All Ports including Empty Port Plates";
                    }
                    else
                    {
                        if (item.Ports) text += ", Ports";
                        if (item.EmptyPortPlates) text += ", Empty Port Plates";
                    }

                    if (item.SolvedModules)
                        text += ", Solved Modules";
                    if (item.UnsolvedModules)
                        text += ", Unsolved Modules";
                }
                if (item.Strikes) text += ", Strikes";
                break;
            }
            return text == "" 
                ? " - ASSUMING ALL EDGEWORK IS REQUIRED FOR \"" + name + "\""
                : text;
        }
    }

    public class RequiredEdgeWorkItem
    {
        public string ModuleName;

        public bool Everything;
        public bool Nothing;

        public bool Strikes;

        public bool LitIndicators;
        public bool UnlitIndicators;
        public bool Ports;
        public bool EmptyPortPlates;

        public bool Batteries;
        public bool BatteryHolders;

        public bool UnsolvedModules;
        public bool SolvedModules;

        public bool TwoFactor;

        public bool SerialNumber;
    }
}