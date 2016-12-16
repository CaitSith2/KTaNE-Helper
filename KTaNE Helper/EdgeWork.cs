using System.Collections.Generic;
using System.Net.Mime;

namespace KTaNE_Helper
{
    public static class EdgeWork
    {
        public static List<EdgeWorkItem> ModuleEdgeWorkRequired = new List<EdgeWorkItem>
        {
            //About
            new EdgeWorkItem {ModuleName = "About", Nothing = true },

            //Vanilla Modules
            new EdgeWorkItem {ModuleName = "Complicated Wires", SerialNumber = true, Batteries = true, Ports = true},
            new EdgeWorkItem {ModuleName = "Keypads", Nothing = true },
            new EdgeWorkItem {ModuleName = "Mazes", Nothing = true },
            new EdgeWorkItem {ModuleName = "Memory", Nothing = true },
            new EdgeWorkItem {ModuleName = "Morse Code", Nothing = true },
            new EdgeWorkItem {ModuleName = "Passwords", Nothing = true },
            new EdgeWorkItem {ModuleName = "The Button", Batteries = true, LitIndicators = true},
            new EdgeWorkItem {ModuleName = "Simon Says", SerialNumber = true, Strikes = true},
            new EdgeWorkItem {ModuleName = "Simple Wires", SerialNumber = true},
            new EdgeWorkItem {ModuleName = "Who's on First", Nothing = true },
            new EdgeWorkItem {ModuleName = "Wire Sequences", Nothing = true },

            //Mod bomb Modules
            new EdgeWorkItem {ModuleName = "3D Maze", LitIndicators = true, UnlitIndicators = true, SerialNumber = true},
            new EdgeWorkItem {ModuleName = "Adventure Game", Everything = true },
            new EdgeWorkItem {ModuleName = "Alphabet", Nothing = true },
            new EdgeWorkItem {ModuleName = "Anagram", Nothing = true },
            new EdgeWorkItem {ModuleName = "Astrology", SerialNumber = true },
            new EdgeWorkItem {ModuleName = "Blind Alley", Everything = true },
            new EdgeWorkItem {ModuleName = "The Bulb",  LitIndicators = true, UnlitIndicators = true},
            new EdgeWorkItem {ModuleName = "Caesar Cipher", Everything = true },
            new EdgeWorkItem {ModuleName = "Chess", SerialNumber = true},
            new EdgeWorkItem {ModuleName = "Combination Lock", TwoFactor = true, Batteries = true, UnsolvedModules = true, SolvedModules = true },
            new EdgeWorkItem {ModuleName = "Connection Check", SerialNumber = true },
            new EdgeWorkItem {ModuleName = "Cryptography", Nothing = true },
            new EdgeWorkItem {ModuleName = "Emoji Math", Nothing = true },
            new EdgeWorkItem {ModuleName = "Flashing Colors", Nothing = true },
            new EdgeWorkItem {ModuleName = "Forget Me Not", Everything = true },
            new EdgeWorkItem {ModuleName = "Murder", Everything = true },
            new EdgeWorkItem {ModuleName = "Perspective Pegs", SerialNumber = true, Batteries = true},
            new EdgeWorkItem {ModuleName = "Plumbing", Everything = true },
            new EdgeWorkItem {ModuleName = "Resistors",SerialNumber = true,Batteries = true,Ports = true,BatteryHolders = true},
            new EdgeWorkItem {ModuleName = "Safety Safe", SerialNumber = true, LitIndicators = true, UnlitIndicators = true },
            new EdgeWorkItem {ModuleName = "Word Scramble", Nothing = true },
            new EdgeWorkItem {ModuleName = "Switches", Nothing = true },
            new EdgeWorkItem {ModuleName = "Two Bits", Batteries = true, SerialNumber = true},
        };

        public static string GetRequiredEdgeWork(string name)
        {
            var text = "";
            foreach (var item in EdgeWork.ModuleEdgeWorkRequired)
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

    public class EdgeWorkItem
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