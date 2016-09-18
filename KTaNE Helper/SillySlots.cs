
using System;
using System.Windows.Forms;

namespace KTaNE_Helper
{
    public class SillySlots
    {
        private const int Sassy = 0;
        private const int Silly = 1;
        private const int Soggy = 2;
        private const int Sally = 0;
        private const int Simon = 1;
        private const int Sausage = 2;
        private const int Steven = 3;

        private readonly int[][] _keyWordLookup = {
            new[] {Silly,Sassy,Soggy,Simon,Sally,Sausage,Steven},
            new[] {Soggy,Sassy,Silly,Sausage,Steven,Simon,Sally},
            new[] {Soggy,Silly,Sassy,Steven,Simon,Sausage,Sally},
            new[] {Sassy,Silly,Soggy,Sally,Simon,Sausage,Steven},
            new[] {Sassy,Soggy,Silly,Simon,Sausage,Sally,Steven},
            new[] {Sassy,Silly,Soggy,Sally,Steven,Simon,Sausage},
            new[] {Silly,Soggy,Sassy,Steven,Sally,Simon,Sausage}      
        };

        private readonly int[][] _slotRounds = new int[6][];

        public void DumpStateToClipboard()
        {
            var keywords = new[]
            {
                    "Sassy", "Silly", "Soggy", "Sally", "Simon", "Sausage", "Steven"
                };
            var colorsymboles = new[]
            {
                    "Red", "Blue", "Green", " Grape", " Cherry", " Bomb", " Coin"
                };

            var clipboard = "";
            foreach (var xx in _slotRounds)
            {
                if (xx != null)
                {
                    clipboard += keywords[xx[0]] + "\t";
                    for (var i = 0; i < 6; i += 2)
                    {
                        clipboard += colorsymboles[xx[i + 1]] + colorsymboles[xx[i + 2]];
                        if (i < 4)
                            clipboard += ", ";
                    }
                    clipboard += xx[7] == 0 ? "\t\t//Spin" : "\t\t//Keep";
                    clipboard += Environment.NewLine;
                }
                else
                {
                    Clipboard.SetText(clipboard);
                    return;
                }
            }
        }

        private int _currentRound = -1;

        public bool CheckSlots(int keyWord, int slot1, int slot2, int slot3, bool debug = false)
        {
            var x = new int[8];

            if (_currentRound == 5)
                return true;
            _currentRound++;

            x[0] = keyWord;
            x[1] = slot1/4;
            x[2] = (slot1%4) + 3;
            x[3] = slot2/4;
            x[4] = (slot2%4) + 3;
            x[5] = slot3/4;
            x[6] = (slot3%4) + 3;
            _slotRounds[_currentRound] = x;


            var count = 0;

            var y = _keyWordLookup[x[0]];
            for (var i = 0; i < 3; i++)
            {
                if (y[x[(i*2) + 1]] == Silly && y[x[(i*2) + 2]] == Sausage)
                    count++;
            }
            if (count == 1) return false; //There is a Single Silly Sausage

            count = 0;
            var position = 0;
            for (var i = 0; i < 3; i++)
            {
                if (y[x[(i*2) + 1]] == Sassy && y[x[(i*2) + 2]] == Sally)
                    count++;
                if (count == 1) position = i;
            }
            if (count == 1) //There is a single Sasy Sally
            {
                if ((_currentRound - 2) < 0) return false; //Not possible to be true on rounds 1 & 2.
                var xx = _slotRounds[_currentRound - 2];
                var yy = _keyWordLookup[xx[0]];
                if (yy[xx[(position*2) + 1]] != Soggy) return false;
            } //Unless the slot in the same position 2 Stages ago was Soggy

            count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (y[x[(i*2) + 1]] == Soggy && y[x[(i*2) + 2]] == Steven)
                    count++;
            }
            if (count > 1) return false; //There are 2 or more Soggy Stevens

            count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (y[x[(i*2) + 1]] != Sassy && y[x[(i*2) + 2]] == Simon)
                    count++;
            }
            if (count == 3) return false; //There are 3 Simons, unless any of them are Sassy

            if (y[x[2]] == Sausage && y[x[3]] != Soggy && y[x[4]] == Sally) return false;
            if (y[x[4]] == Sausage && y[x[1]] != Soggy && y[x[2]] == Sally) return false;
            if (y[x[4]] == Sausage && y[x[5]] != Soggy && y[x[6]] == Sally) return false;
            if (y[x[6]] == Sausage && y[x[3]] != Soggy && y[x[4]] == Sally) return false;
            //There is a Sausage adjacent to Sally, unless Sally is Soggy

            count = 0;
            position = 0;
            for (var i = 0; i < 3; i++)
            {
                if (y[x[(i*2) + 1]] == Silly)
                    count++;
                if (y[x[(i*2) + 1]] == Silly && y[x[(i*2) + 2]] == Steven)
                    position++;
            }
            if (count == 2 && position < 2) return false; //There are exactly 2 Silly Slots, unless they are both Steven

            count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (y[x[(i*2) + 1]] == Soggy)
                    count++;
            }
            if (count == 1) //There is a single Soggy slot
            {
                if (_currentRound == 0) return false; //Not possible to be true on first round.
                var xx = _slotRounds[_currentRound - 1];
                var yy = _keyWordLookup[xx[0]];
                count = 0;
                for (var i = 0; i < 3; i++)
                {
                    if (yy[xx[(i*2) + 2]] == Sausage)
                        count++;
                }
                if (count == 0) return false;
            } //Unless the previous stage had any number of Sausage slots

            if (slot1 == slot2 && slot2 == slot3) //All 3 slots are the same symbol and color
            {
                if (_currentRound == 0) return false;
                count = 0;
                for (var i = _currentRound - 1; i >= 0 && count == 0; i--)
                {
                    var xx = _slotRounds[i];
                    var yy = _keyWordLookup[xx[0]];
                    for (var j = 0; j < 2 && count == 0; j++)
                    {
                        if (yy[xx[(i*2) + 1]] == Soggy && yy[xx[(i*2) + 2]] == Sausage)
                            count++;
                    }
                }
                if (count == 0) return false;
            } //Unless there has been a Soggy Sausage in any previous Stage

            if (x[1] == x[3] && x[3] == x[5]) //All 3 slots are the same Color
            {
                if (y[x[2]] != Sally && y[x[4]] != Sally && y[x[6]] != Sally)
                {
                    if (_currentRound == 0) return false;
                    var xx = _slotRounds[_currentRound - 1];
                    var yy = _keyWordLookup[xx[0]];
                    count = 0;
                    for (var i = 0; i < 3; i++)
                    {
                        if (yy[xx[(i*2) + 1]] == Silly && yy[xx[(i*2) + 2]] == Steven)
                            count++;
                    }
                    if (count == 0) return false;
                }
            } //Unless any of them are Sally or there was a Silly Steven in the last stage

            count = 0;
            for (var i = 0; i < 3; i++)
            {
                if (y[x[(i*2) + 1]] == Silly && y[x[(i*2) + 2]] == Simon)
                    count++;
            }
            if (count > 0) //There are any number of Silly Simons
            {
                if (_currentRound == 0) return false;
                count = 0;
                for (var i = _currentRound - 1; i >= 0 && count == 0; i--)
                {
                    var xx = _slotRounds[i];
                    var yy = _keyWordLookup[xx[0]];
                    for (var j = 0; j < 2 && count == 0; j++)
                    {
                        if (yy[xx[(i*2) + 1]] == Sassy && yy[xx[(i*2) + 2]] == Sausage)
                            count++;
                    }
                }
                if (count == 0) return false;
            } //Unless there has been a Sassy Sausage in any previous stage.


            x[7] = 1;
            return true; //Keep.
        }

    }
}