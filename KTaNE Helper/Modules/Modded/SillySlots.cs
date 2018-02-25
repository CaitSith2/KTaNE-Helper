using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KTaNE_Helper.Modules.Modded
{
    public class SillySlots
    {
        private readonly SlotData[] _slotRounds = new SlotData[6];

        private const string Silly = "Silly";
        private const string Sassy = "Sassy";
        private const string Soggy = "Soggy";

        private const string Sally = "Sally";
        private const string Simon = "Simon";
        private const string Sausage = "Sausage";
        private const string Steven = "Steven";


        public string GetSlotColorName(string keyword, SlotColor color)
        {
            return Slots.SlotColors[keyword][Sassy] == color ? Sassy
                 : Slots.SlotColors[keyword][Silly] == color ? Silly
                 : Soggy;
        }

        public string GetSlotShapeName(string keyword, SlotShape shape)
        {
            return Slots.SlotShapes[keyword][Sally] == shape ? Sally
                 : Slots.SlotShapes[keyword][Simon] == shape ? Simon
                 : Slots.SlotShapes[keyword][Sausage] == shape ? Sausage
                 : Steven;
        }

        public void DumpStateToClipboard()
        {
            var slotColorColors = new[] {"Red", "Green", "Blue"};
            var slotShapeShapes = new[] {"Bomb", "Grape", "Cherry", "Coin"};
            

            var clipboard = "";
            if (_slotRounds[0] == null) return;
            foreach (var xx in _slotRounds)
            {
                if (xx == null) break;

                clipboard += xx.Keyword + "\t";
                for (var i = 0; i < 3; i++)
                {
                    clipboard += slotColorColors[(int) xx.Slots[i].Color];
                    clipboard += " ";
                    clipboard += slotShapeShapes[(int) xx.Slots[i].Shape];
                    if (i < 2)
                        clipboard += ", ";
                }
                //clipboard += xx.Result == 0 ? "\t//Spin" : "\t//Keep";
                clipboard += Environment.NewLine;
            }
            clipboard += Environment.NewLine;
            for(var j = 0; j < 4; j++)
            {
                var xx = _slotRounds[j];
                if (xx == null) break;

                for (var i = 0; i < 3; i++)
                {
                    clipboard += GetSlotColorName(xx.Keyword, xx.Slots[i].Color);
                    clipboard += " ";
                    clipboard += GetSlotShapeName(xx.Keyword, xx.Slots[i].Shape);
                    if (i < 2)
                        clipboard += ", ";
                }
                clipboard += xx.Result == 0 ? "\t//Spin\t" : "\t//Keep\t";
				var s = new Slot(SlotShape.Bomb, SlotColor.Blue);
				CheckSlots(0, s, s, s, out string reason, j);
                clipboard += reason + Environment.NewLine;
            }


            Clipboard.SetText(clipboard);
        }

        private int _currentRound = -1;

        private static int CountSlots(SlotData x, string identifier, SlotBool type)
        {
            var count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (type == SlotBool.Color && x.Slots[i].Color == Slots.SlotColors[x.Keyword][identifier])
                    count++;
                else if (type == SlotBool.Shape && x.Slots[i].Shape == Slots.SlotShapes[x.Keyword][identifier])
                    count++;
            }
            return count;
        }

        private static int CountSlots(SlotData x, string identifier, SlotBool type, string currentKeyword)
        {
            var count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (type == SlotBool.Color && x.Slots[i].Color == Slots.SlotColors[currentKeyword][identifier])
                    count++;
                else if (type == SlotBool.Shape && x.Slots[i].Shape == Slots.SlotShapes[currentKeyword][identifier])
                    count++;
            }
            return count;
        }

        private static int CountSlots(SlotData x, string color, string shape)
        {
            var count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (x.Slots[i].Color == Slots.SlotColors[x.Keyword][color] &&
                    x.Slots[i].Shape == Slots.SlotShapes[x.Keyword][shape])
                    count++;
            }
            return count;
        }

        private static int CountSlots(SlotData x, string color, string shape, string currentKeyword)
        {
            var count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (x.Slots[i].Color == Slots.SlotColors[currentKeyword][color] &&
                    x.Slots[i].Shape == Slots.SlotShapes[currentKeyword][shape])
                    count++;
            }
            return count;
        }



        private static int CountSlots(SlotData x, string color, string shape, out int position)
        {
            position = -1;
            var count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (x.Slots[i].Color == Slots.SlotColors[x.Keyword][color] &&
                    x.Slots[i].Shape == Slots.SlotShapes[x.Keyword][shape])
                    count++;
                position = (count == 1) ? i : -1;
            }
            return count;
        }


        public bool CheckSlots(int keyWord, Slot slot1, Slot slot2, Slot slot3)
        {
	        // ReSharper disable once UnusedVariable
			return CheckSlots(keyWord, slot1, slot2, slot3, out string reason);
		}


        public bool CheckSlots(int keyWord, Slot slot1, Slot slot2, Slot slot3, out string reason, int debug = -1)
        {
            var x = new SlotData();
            reason = "";

            if (debug < 0)
            {
                if (_currentRound == 3)
                {
                    reason = "Module already Solved";
                    return true;
                }
                _currentRound++;

                x.Keyword = Slots.Keywords[keyWord];
                x.Slots[0] = slot1;
                x.Slots[1] = slot2;
                x.Slots[2] = slot3;

                _slotRounds[_currentRound] = x;
            }
            else
            {
                _currentRound = debug;
                x = _slotRounds[debug];
            }

            if (CountSlots(x, Silly, Sausage) == 1)
            {
                reason += "There is a Single Silly Sausage";
                return false; //There is a Single Silly Sausage
            }

			if (CountSlots(x, Sassy, Sally, out int position) == 1)
			{   //There is a single Sasy Sally
				reason += "There is a single Sassy Sally";
				if ((_currentRound - 2) < 0) return false;
				var xx = _slotRounds[_currentRound - 2];
				if (xx.Slots[position].Color != Slots.SlotColors[x.Keyword][Soggy]) return false;
				reason += ", however, That slot 2 stages ago was Soggy. ";
			}   //Unless the slot in the same position 2 Stages ago was Soggy

			if (CountSlots(x, Soggy, Steven) > 1)
            {
                reason += "There are 2 or more Soggy Stevens";
                return false; //There are 2 or more Soggy Stevens
            }

            if (CountSlots(x, Simon, SlotBool.Shape) == 3)
            {
                reason += "There are 3 Simons";
                if(CountSlots(x, Sassy, SlotBool.Color) == 0)
                    return false; //There are 3 Simons, unless any of them are Sassy
                reason += ", However, at least one of those Simons was Sassy. ";
            }

            var sss = new[] {0, 1, 1, 2, 2, 1, 1, 0};
            var flag1 = false;
            var flag2 = false;
            for (var i = 0; i < 8 && !flag1; i += 2)
            {
                flag1 = (x.Slots[sss[i]].Shape == Slots.SlotShapes[x.Keyword][Sausage] &&
                          x.Slots[sss[i + 1]].Shape == Slots.SlotShapes[x.Keyword][Sally]);
                flag2 = x.Slots[sss[i + 1]].Color != Slots.SlotColors[x.Keyword][Soggy];
            }   //There is a Sausage adjacent to Sally, unless Sally is Soggy
            if (flag1)
            {
                reason += "There is a Sausage adjacent to Sally";
                if (flag2) return false;
                reason += ", However, Sally was Soggy. ";
            }


            if (CountSlots(x, Silly, SlotBool.Color) == 2)
            {
                reason += "There are 2 Silly slots";
                if(CountSlots(x, Silly, Steven) != 2)
                    return false; //There are exactly 2 Silly Slots, unless they are both Steven
                reason += ", However, both of these were Silly Stevens. ";
            }



            if (CountSlots(x,Soggy,SlotBool.Color) == 1) //There is a single Soggy slot
            {
                reason += "There is a single Soggy slot";
                if (_currentRound == 0) return false; //Not possible to be true on first round.
                if (CountSlots(_slotRounds[_currentRound - 1], Sausage, SlotBool.Shape, x.Keyword) == 0)
                    return false;   //Unless the previous stage had any number of Sausage slots
                reason += ", However, there was at least one Sausage the previous round. ";
            }

            if (x.Slots[0].Color == x.Slots[1].Color && x.Slots[1].Color == x.Slots[2].Color)
            {
                reason += "All 3 slots are the same color";
                var flag3 = false;
                if (x.Slots[0].Shape == x.Slots[1].Shape && x.Slots[1].Shape == x.Slots[2].Shape)
                {
                    //All 3 slots are the same symbol and color
                    reason += " and symbol";
                    for (var i = _currentRound - 1; i >= 0; i--)
                    {
                        if (CountSlots(_slotRounds[i], Soggy, Sausage, x.Keyword) > 0) break;
                        if (i == 0) return false;
                    }
                    reason += ", However, there was a Soggy Sausage present in the past. ";
                    flag3 = true;
                } //Unless there has been a Soggy Sausage in any previous Stage
                else
                {
	                reason += ", but with different symbols. ";
                }

	            if (flag3)
                    reason += "All 3 slots are the same color";
                //All 3 slots are the same Color
                if (CountSlots(x, Sally, SlotBool.Shape) == 0)
                {
                    if (_currentRound == 0) return false; //Not possible to be true on first round.
                    if (CountSlots(_slotRounds[_currentRound - 1], Silly, Steven, x.Keyword) == 0)
                        return false;
                    if (flag3)
                        reason += ", ";
                    reason += "However, there was a Silly Steven the previous stage. ";
                } //Unless any of them are Sally or there was a Silly Steven in the last stage
                else
                {
                    if (flag3)
                        reason += ", ";
                    reason += "However, one of these slots were Sally. ";
                }
            }

            if (CountSlots(x, Silly, Simon) > 0)    //There are any number of Silly Simons
            {
                reason += "There is at least one Silly Simon";
                if (_currentRound == 0) return false;
                for (var i = _currentRound - 1; i >= 0; i--)
                {
                    if (CountSlots(_slotRounds[i], Sassy, Sausage, x.Keyword) > 0) break;
                }
                reason += ", However, there was a Sassy Sausage in the past. ";
            }   //Unless there has been a Sassy Sausage in any previous stage.

            reason += "No Illegal states Detected.";
            x.Result = 1;
            return true; //Keep.
        }

    }

    public struct Slot
    {
        public SlotShape Shape;
        public SlotColor Color;
        //public int position;
        public Slot(SlotShape shape, SlotColor color/*, int position*/)
        {
            Shape = shape;
            Color = color;
            //this.position = position;
        }
    }

    public class SlotData
    {
        public string Keyword = "";
        public Slot[] Slots = new Slot[3];
        public int Result;
    }

    public enum SlotColor
    {
        Red,
        Green,
        Blue
    }

    public enum SlotShape
    {
        Bomb,
        Grape,
        Cherry,
        Coin
    }

    public enum SlotBool
    {
        Color,
        Shape
    }

    public static class Slots
    {
        // Fields
        public static Dictionary<string, Dictionary<string, SlotColor>> SlotColors;
        public static Dictionary<string, Dictionary<string, SlotShape>> SlotShapes;

        public static List<string> Keywords = new List<string>
        {
            "Sassy","Silly","Soggy",
            "Sally","Simon","Sausage","Steven"
        };

        // ReSharper disable once InconsistentNaming
        public static Slot[] slots =
        {
            new Slot(SlotShape.Grape, SlotColor.Red),
            new Slot(SlotShape.Cherry, SlotColor.Red),
            new Slot(SlotShape.Bomb, SlotColor.Red),
            new Slot(SlotShape.Coin, SlotColor.Red),
            new Slot(SlotShape.Grape, SlotColor.Blue),
            new Slot(SlotShape.Cherry, SlotColor.Blue),
            new Slot(SlotShape.Bomb, SlotColor.Blue),
            new Slot(SlotShape.Coin, SlotColor.Blue),
            new Slot(SlotShape.Grape, SlotColor.Green),
            new Slot(SlotShape.Cherry, SlotColor.Green),
            new Slot(SlotShape.Bomb, SlotColor.Green),
            new Slot(SlotShape.Coin, SlotColor.Green)
        };
        

        // Methods
        public static void PopulateSubstitionTable()
        {
            if ((SlotColors != null) && (SlotShapes != null)) return;

            SlotColors = new Dictionary<string, Dictionary<string, SlotColor>>();
            var dictionary = new Dictionary<string, SlotColor> {
                {"Sassy",SlotColor.Blue},
                {"Silly",SlotColor.Red},
                {"Soggy",SlotColor.Green}
            };
            var dictionary2 = new Dictionary<string, SlotColor> {
                {"Sassy",SlotColor.Blue},
                {"Silly",SlotColor.Green},
                {"Soggy",SlotColor.Red}
            };
            var dictionary3 = new Dictionary<string, SlotColor> {
                {"Sassy",SlotColor.Green},
                {"Silly",SlotColor.Blue},
                {"Soggy",SlotColor.Red}
            };
            var dictionary4 = new Dictionary<string, SlotColor> {
                {"Sassy",SlotColor.Red},
                {"Silly",SlotColor.Blue},
                {"Soggy",SlotColor.Green}
            };
            var dictionary5 = new Dictionary<string, SlotColor> {
                {"Sassy",SlotColor.Red},
                {"Silly",SlotColor.Green},
                {"Soggy",SlotColor.Blue}
            };
            var dictionary6 = new Dictionary<string, SlotColor> {
                {"Sassy",SlotColor.Red},
                {"Silly",SlotColor.Blue},
                {"Soggy",SlotColor.Green}
            };
            var dictionary7 = new Dictionary<string, SlotColor> {
                {"Sassy",SlotColor.Green},
                {"Silly",SlotColor.Red},
                {"Soggy",SlotColor.Blue}
            };
            SlotColors.Add("Sassy", dictionary);
            SlotColors.Add("Silly", dictionary2);
            SlotColors.Add("Soggy", dictionary3);
            SlotColors.Add("Sally", dictionary4);
            SlotColors.Add("Simon", dictionary5);
            SlotColors.Add("Sausage", dictionary6);
            SlotColors.Add("Steven", dictionary7);

            SlotShapes = new Dictionary<string, Dictionary<string, SlotShape>>();
            var dictionary8 = new Dictionary<string, SlotShape> {
                {"Sally",SlotShape.Cherry},
                {"Simon",SlotShape.Grape},
                {"Sausage",SlotShape.Bomb},
                {"Steven",SlotShape.Coin}
            };
            var dictionary9 = new Dictionary<string, SlotShape> {
                {"Sally",SlotShape.Coin},
                {"Simon",SlotShape.Bomb},
                {"Sausage",SlotShape.Grape},
                {"Steven",SlotShape.Cherry}
            };
            var dictionary10 = new Dictionary<string, SlotShape> {
                {"Sally",SlotShape.Coin},
                {"Simon",SlotShape.Cherry},
                {"Sausage",SlotShape.Bomb},
                {"Steven",SlotShape.Grape}
            };
            var dictionary11 = new Dictionary<string, SlotShape> {
                {"Sally",SlotShape.Grape},
                {"Simon",SlotShape.Cherry},
                {"Sausage",SlotShape.Bomb},
                {"Steven",SlotShape.Coin}
            };
            var dictionary12 = new Dictionary<string, SlotShape> {
                {"Sally",SlotShape.Bomb},
                {"Simon",SlotShape.Grape},
                {"Sausage",SlotShape.Cherry},
                {"Steven",SlotShape.Coin}
            };
            var dictionary13 = new Dictionary<string, SlotShape> {
                {"Sally",SlotShape.Grape},
                {"Simon",SlotShape.Bomb},
                {"Sausage",SlotShape.Coin},
                {"Steven",SlotShape.Cherry}
            };
            var dictionary14 = new Dictionary<string, SlotShape> {
                {"Sally",SlotShape.Cherry},
                {"Simon",SlotShape.Bomb},
                {"Sausage",SlotShape.Coin},
                {"Steven",SlotShape.Grape}
            };
            SlotShapes.Add("Sassy", dictionary8);
            SlotShapes.Add("Silly", dictionary9);
            SlotShapes.Add("Soggy", dictionary10);
            SlotShapes.Add("Sally", dictionary11);
            SlotShapes.Add("Simon", dictionary12);
            SlotShapes.Add("Sausage", dictionary13);
            SlotShapes.Add("Steven", dictionary14);
        }
}





}