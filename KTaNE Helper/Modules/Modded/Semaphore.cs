using static System.String;

namespace KTaNE_Helper
{
    public class Semaphore
    {
        private bool _numbers;
        private bool _letters;

        private bool IsAnswer(string letter, string number)
        {
            if (!IsNullOrEmpty(number) && _numbers && SerialNumber.Serial.Contains(number)) return false;
            if (!IsNullOrEmpty(letter) && _letters && SerialNumber.Serial.Contains(letter)) return false;
            if (IsNullOrEmpty(letter) && _letters) return false;
            if (IsNullOrEmpty(number) && _numbers) return false;
            if (IsNullOrEmpty(letter) && IsNullOrEmpty(number)) return false;
            return true;
        }

        public string GetAnswer(string input)
        {
            if (SerialNumber.Serial.Length < 6) return "";
            var flags = input.ToUpper().Split(' ');
            

            var count = 0;
            foreach (var f in flags)
            {
                var letternumber = "";
                count++;
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (f)
                {
                    case "S.S": //Rest / Space
                    case "S-S":
                    case "W.W": //Error / Attention
                    case "W-W":
                    case "NW.SE": //Cancel
                    case "NW-SE":
                        break;
                    case "LETTERS":
                        _letters = true;
                        _numbers = false;
                        break;
                    case "N.NE": //Numerals
                    case "N-NE":
                    case "NUMBERS":
                        _letters = false;
                        _numbers = true;
                        break;
                    case "SW.S": //A or 1
                    case "SW-S":
                    case "A":
                    case "1":
                        letternumber = "A1";
                        break;
                    case "W.S":
                    case "W-S":
                    case "B":
                    case "2":
                        letternumber = "B2";
                        break;
                    case "NW.S":
                    case "NW-S":
                    case "C":
                    case "3":
                        letternumber = "C3";
                        break;
                    case "N.S":
                    case "N-S":
                    case "D":
                    case "4":
                        letternumber = "D4";
                        break;
                    case "S.NE":
                    case "S-NE":
                    case "E":
                    case "5":
                        letternumber = "E5";
                        break;
                    case "S.E":
                    case "S-E":
                    case "F":
                    case "6":
                        letternumber = "F6";
                        break;
                    case "S.SE":
                    case "S-SE":
                    case "G":
                    case "7":
                        letternumber = "G7";
                        break;
                    case "W.SW":
                    case "W-SW":
                    case "H":
                    case "8":
                        letternumber = "H8";
                        break;
                    case "SW.NW":
                    case "SW-NW":
                    case "I":
                    case "9":
                        letternumber = "I9";
                        break;
                    case "N.E":
                    case "N-E":
                    case "J":
                        if (!_letters)
                        {
                            _letters = true;
                            _numbers = false;
                        }
                        else
                        {
                            letternumber = "J";
                        }
                        break;
                    case "SW.N":
                    case "SW-N":
                    case "K":
                    case "0":
                        letternumber = "K0";
                        break;
                    case "SW.NE":
                    case "SW-NE":
                    case "L":
                        letternumber = "L";
                        break;
                    case "SW.E":
                    case "SW-E":
                    case "M":
                        letternumber = "M";
                        break;
                    case "SW.SE":
                    case "SW-SE":
                    case "N":
                        letternumber = "N";
                        break;
                    case "W.NW":
                    case "W-NW":
                    case "O":
                        letternumber = "O";
                        break;
                    case "W.N":
                    case "W-N":
                    case "P":
                        letternumber = "P";
                        break;
                    case "W.NE":
                    case "W-NE":
                    case "Q":
                        letternumber = "Q";
                        break;
                    case "W.E":
                    case "W-E":
                    case "R":
                        letternumber = "R";
                        break;
                    case "W.SE":
                    case "W-SE":
                    case "S":
                        letternumber = "S";
                        break;
                    case "NW.N":
                    case "NW-N":
                    case "T":
                        letternumber = "T";
                        break;
                    case "NW.NE":
                    case "NW-NE":
                    case "U":
                        letternumber = "U";
                        break;
                    case "N.SE":
                    case "N-SE":
                    case "V":
                        letternumber = "V";
                        break;
                    case "NE.E":
                    case "NE-E":
                    case "W":
                        letternumber = "W";
                        break;
                    case "NE.SE":
                    case "NE-SE":
                    case "X":
                        letternumber = "X";
                        break;
                    case "NW.E":
                    case "NW-E":
                    case "Y":
                        letternumber = "Y";
                        break;
                    case "SE.E":
                    case "SE-E":
                    case "Z":
                        letternumber = "Z";
                        break;


                }

                if (IsAnswer(letternumber.Length < 1 ? "" : letternumber.Substring(0, 1),
                    letternumber.Length < 2 ? "" : letternumber.Substring(1, 1)))
                    return "Submit on " + count;
            }
            return "";
        }
    }
}