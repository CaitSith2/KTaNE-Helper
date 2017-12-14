using System;
using System.Collections.Generic;
using System.Linq;
using VanillaRuleGenerator.Edgework;

namespace KTaNE_Helper.Modules.Modded
{
    public class AdventureGame
    {
        private readonly bool _useMoonstone;
        private readonly bool _useSunstone;
        private readonly bool _useHardDrive;
        private readonly bool _useBattery;

        public AdventureGame()
        {
            _useHardDrive = PortPlate.DuplicatePorts();
            _useMoonstone = Indicators.UnlitIndicators.Length >= 2;
            _useSunstone = Indicators.LitIndicators.Length >= 2;
            _useBattery = Batteries.TotalBatteries < 2;
        }

        private static string ItemName(Items i) => i.ToString().Replace("_", " ");
        private static string WeaponName(Weapons w) => w.ToString().Replace("_", " ");

        private int[] _stats;
        private Monsters _monster;

        private int GetSTR() => _stats[0];
        private int GetDEX() => _stats[1];
        private int GetINT() => _stats[2];
        private int GetHeight() => _stats[3];
        private int GetTemp() => _stats[4];
        private int GetGrav() => _stats[5];
        private int GetATM() => _stats[6];

        private static int GetWeapon(Weapons w) => (int) w;

        private bool UseThisItem(Items item)
        {
            switch (item)
            {
                case Items.Balloon:
                    return (GetGrav() < 93 || GetATM() > 110) && _monster != Monsters.Eagle;

                case Items.Battery:
                    return _useBattery && _monster != Monsters.Golem && _monster != Monsters.Wizard;

                case Items.Bellows:
                    return ((_monster == Monsters.Dragon || _monster == Monsters.Eagle) && GetATM() > 105) ||
                           ((_monster != Monsters.Dragon && _monster != Monsters.Eagle) && GetATM() < 95);

                case Items.Cheat_Code:
                    return false;   //Cheaters never prosper

                case Items.Crystal_Ball:
                    return GetINT() > SerialNumber.LastSerialDigit && _monster != Monsters.Wizard;

                case Items.Feather:
                    return GetDEX() > GetINT() || GetDEX() > GetSTR();

                case Items.Hard_Drive:
                    return _useHardDrive;

                case Items.Lamp:
                    return GetTemp() < 12 && _monster != Monsters.Lizard;

                case Items.Moonstone:
                    return _useMoonstone;

                case Items.Potion:
                    return true;

                case Items.Small_Dog:
                    return _monster != Monsters.Demon && _monster != Monsters.Dragon && _monster != Monsters.Troll;

                case Items.Step_Ladder:
                    return GetHeight() < 48 && _monster != Monsters.Goblin && _monster != Monsters.Lizard;

                case Items.Sunstone:
                    return _useSunstone;

                case Items.Symbol:
                    return _monster == Monsters.Demon || _monster == Monsters.Golem || GetTemp() > 31;

                case Items.Ticket:
                    return GetHeight() >= 54 && GetGrav() >= 92 && GetGrav() <= 104;

                case Items.Trophy:
                    return GetSTR() > SerialNumber.FirstSerialDigit || _monster == Monsters.Troll;

                case Items.NotSelected:
                    return false;

                default:
                    return false;
            }
        }

        private readonly int[] _monsterStats = {
            50, 50, 50, //Demon
            10, 11, 13, //Dragon
            4, 7, 3,    //Eagle
            3, 6, 5,    //Goblin
            9, 4, 7,    //Golem
            8, 5, 4,    //Troll
            4, 6, 3,    //Lizard
            4, 3, 8     //Wizard
        };

        private int CalculateWeaponScore(Weapons w)
        {
            var i = GetWeapon(w)/2;
            var num2 = _stats[i] + ((GetWeapon(w)%2)*2);
            var num3 = _monsterStats[((int)_monster*3) + i];
            return num2 - num3;
        }

        public string GetAdventrueGameResults(int[] stats, Monsters monster, Weapons[] weapons, Items[] items)
        {
            _stats = stats;
            _monster = monster;
            var result = "";
            for(var i = 0; i < 3; i++)
                for (var j = i + 1; j < 3; j++)
                {
                    if (weapons[i] != weapons[j]) continue;
                    return "Please Select 3 different Weapons";
                }

            for (var i = 0; i < 5; i++)
                for (var j = i + 1; j < 5; j++)
                {
                    if (items[i] != items[j]) continue;
                    if (items[i] == Items.NotSelected) continue;
                    return "Please Select 5 different items";
                }

            var itemsToUse = items.Where(UseThisItem).ToList();
           
            if (itemsToUse.Count > 0)
            {
                result += "Use these Items: ";
                result = itemsToUse.Aggregate(result, (current, t) => current + (ItemName(t) + ", "));
            }
            var weaponscore = -999;
            var correctweapon = new bool[3];
            for (var i = 0; i < 3; i++)
            {
                var ws = CalculateWeaponScore(weapons[i]);
                if (ws == weaponscore)
                {
                    correctweapon[i] = true;
                    weaponscore = ws;
                }
                else if (ws > weaponscore)
                {
                    for (var j = 0; j < i; j++)
                        correctweapon[j] = false;
                    correctweapon[i] = true;
                    weaponscore = ws;
                }
            }

            if (itemsToUse.Contains(Items.Potion))
            {
                var str = GetSTR();
                var Int = GetINT();
                var dex = GetDEX();
                var potionaffectscorrectweapon = false;
                for(_stats[0] = str - 1; _stats[0] < (str+4) && !potionaffectscorrectweapon; _stats[0]++)
                    for(_stats[1] = dex - 1; _stats[1] < (dex+4) && !potionaffectscorrectweapon; _stats[1]++)
                        for (_stats[2] = Int - 1; _stats[2] < (Int + 4) && !potionaffectscorrectweapon; _stats[2]++)
                        {
                            var potionweaponscore = -999;
                            var potioncorrectweapon = new bool[3];
                            for (var i = 0; i < 3; i++)
                            {
                                var pws = CalculateWeaponScore(weapons[i]);
                                if (pws == potionweaponscore)
                                {
                                    potioncorrectweapon[i] = true;
                                    potionweaponscore = pws;
                                }
                                else if (pws > potionweaponscore)
                                {
                                    for (var j = 0; j < i; j++)
                                        potioncorrectweapon[j] = false;
                                    potioncorrectweapon[i] = true;
                                    potionweaponscore = pws;
                                }
                            }
                            for ( var i = 0; i < 3; i++)
                                potionaffectscorrectweapon |= correctweapon[i] && !potioncorrectweapon[i];
                        }
                if (potionaffectscorrectweapon)
                    result += "Ask Defuser for STR,DEX,INT again. ";
            }

            result += "Use weapons: ";
            var correctWeaponList = new List<string>();
            for(var i = 0; i < 3; i++)
                if (correctweapon[i])
                    correctWeaponList.Add(WeaponName(weapons[i]));
            result += string.Join(", ", correctWeaponList.ToArray());

            return result;
        }

        public int ParseHeight(string height)
        {
            int i;
            var h = 0;
            if (height.Contains("'"))
            {
                var feetInches = height.Split(new[] {"'"}, StringSplitOptions.RemoveEmptyEntries);
                if (feetInches.Length > 0 && int.TryParse(feetInches[0], out i))
                    h = i*12;
                if (feetInches.Length > 1 && int.TryParse(feetInches[1], out i))
                    h += i;
            }
            else if (height.Length > 0 && int.TryParse(height, out i))
                h = i;
            return h;
        }

        public int ParseGravity(string gravity)
        {
            int i;
            var h = 0;
            if (gravity.Contains("."))
            {
                var feetInches = gravity.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                if (feetInches.Length > 0 && int.TryParse(feetInches[0], out i))
                    h = i * 10;
                if (feetInches.Length > 1 && int.TryParse(feetInches[1], out i))
                    h += i;
            }
            else if (gravity.Length > 0 && int.TryParse(gravity, out i))
                h = i;
            return h;
        }
    }

    public enum Monsters
    {
        Demon,
        Dragon,
        Eagle,
        Goblin,
        Golem,
        Troll,
        Lizard,
        Wizard
    }

    // ReSharper disable InconsistentNaming
    public enum Weapons
    {
        Broadsword = 0,
        Caber,
        Nasty_Knife,
        Longbow,
        Magic_Orb,
        Grimoire
    }

    public enum Items
    {
        NotSelected = -1,
        Balloon,
        Battery,
        Bellows,
        Cheat_Code,
        Crystal_Ball,
        Feather,
        Hard_Drive,
        Lamp,
        Moonstone,
        Potion,
        Small_Dog,
        Step_Ladder,
        Sunstone,
        Symbol,
        Ticket,
        Trophy
    }
    // ReSharper restore InconsistentNaming
}