using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace KTaNE_Helper
{
    public class MorseMatics
    {
        private KMBombInfo Info = new KMBombInfo();

        public static int loggingID = 1;
        public int thisLoggingID;

        private static char[] PRIME = new char[]{
            (char)2,
            (char)3,
            (char)5,
            (char)7,
            (char)11,
            (char)13,
            (char)17,
            (char)19,
            (char)23
        };

        private static char[] SQUARE = new char[]{
            (char)1,
            (char)4,
            (char)9,
            (char)16,
            (char)25
        };

        List<string> charList = new List<string>(){
            "A", "B", "C", "D", "E", "F",
            "G", "H", "I", "J", "K", "L",
            "M", "N", "O", "P", "Q", "R",
            "S", "T", "U", "V", "W", "X",
            "Y", "Z"
        };

        public string GenSolution(string morsecharacters)
        {
            if (!SerialNumber.IsSerialValid)
                return "";

            var Answer = "";

            if (morsecharacters.Contains(".") || morsecharacters.Contains("-"))
            {
                var split = morsecharacters.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 3) return "";
                morsecharacters = GetLetter(split[0]) + GetLetter(split[1]) + GetLetter(split[2]);
            }
            if (morsecharacters.Length != 3)
            {
                return "";
            }
            var DisplayCharsRaw = new string[3];

            for (var i = 0; i < 3; i++)
            {
                if (!charList.Contains(morsecharacters[i].ToString()))
                    return "Invalid or Duplicate characters detected";
                charList.Remove(morsecharacters[i].ToString());
                DisplayCharsRaw[i] = morsecharacters[i].ToString();
            }
           

            Debug.Log("[Morsematics #" + thisLoggingID + "] Morsematics display characters: " + DisplayCharsRaw[0] + DisplayCharsRaw[1] + DisplayCharsRaw[2]);

            int disp1base = DisplayCharsRaw[0][0] - 'A' + 1;
            int disp2base = DisplayCharsRaw[1][0] - 'A' + 1;
            int disp3base = DisplayCharsRaw[2][0] - 'A' + 1;

            string serial = "AB1CD2";
            List<string> data = Info.QueryWidgets(KMBombInfo.QUERYKEY_GET_SERIAL_NUMBER, null);
            foreach (string response in data)
            {
                Dictionary<string, string> responseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                serial = responseDict["serial"];
                break;
            }

            List<string> indOn = new List<string>();
            List<string> indOff = new List<string>();

            data = Info.QueryWidgets(KMBombInfo.QUERYKEY_GET_INDICATOR, null);
            foreach (string response in data)
            {
                Dictionary<string, string> responseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

                if (responseDict["on"].Equals("True")) indOn.Add(responseDict["label"]);
                else indOff.Add(responseDict["label"]);
            }

            int batteries = 0;

            data = Info.QueryWidgets(KMBombInfo.QUERYKEY_GET_BATTERIES, null);
            foreach (string response in data)
            {
                Dictionary<string, int> responseDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(response);
                batteries += responseDict["numbatteries"];
            }

            char firstChar = serial[3];
            char secondChar = serial[4];

            Debug.Log("[Morsematics #" + thisLoggingID + "] Initial character pair: " + firstChar + secondChar + "(" + (int)(firstChar - 'A' + 1) + "," + (int)(secondChar - 'A' + 1) + ")");

            foreach (string ind in indOn)
            {
                if (ind.Contains(DisplayCharsRaw[0]) ||
                    ind.Contains(DisplayCharsRaw[1]) ||
                    ind.Contains(DisplayCharsRaw[2]))
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] Matching indicator: " + ind + " (ON)");
                    firstChar++;
                }
            }
            foreach (string ind in indOff)
            {
                if (ind.Contains(DisplayCharsRaw[0]) ||
                    ind.Contains(DisplayCharsRaw[1]) ||
                    ind.Contains(DisplayCharsRaw[2]))
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] Matching indicator: " + ind + " (OFF)");
                    secondChar++;
                }
            }
            if (firstChar > 'Z') firstChar -= (char)26;
            if (secondChar > 'Z') secondChar -= (char)26;

            Debug.Log("[Morsematics #" + thisLoggingID + "] After indicators: " + firstChar + secondChar + "(" + (int)(firstChar - 'A' + 1) + "," + (int)(secondChar - 'A' + 1) + ")");

            int sum = ((firstChar - 'A') + (secondChar - 'A') + 1) % 26 + 1;
            int root = (int)Math.Sqrt(sum);
            if (root * root == sum)
            {
                Debug.Log("[Morsematics #" + thisLoggingID + "] Character sum is square");
                firstChar += (char)4;
                if (firstChar > 'Z') firstChar -= (char)26;
            }
            else
            {
                Debug.Log("[Morsematics #" + thisLoggingID + "] Character sum is not square");
                secondChar -= (char)4;
                if (secondChar < 'A') secondChar += (char)26;
            }

            Debug.Log("[Morsematics #" + thisLoggingID + "] After square check: " + firstChar + secondChar + "(" + (int)(firstChar - 'A' + 1) + "," + (int)(secondChar - 'A' + 1) + ")");

            int largest;
            if (disp1base > disp2base && disp1base > disp3base)
            {
                Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[0] + " is largest");
                largest = disp1base;
            }
            else if (disp2base > disp3base)
            {
                Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[1] + " is largest");
                largest = disp2base;
            }
            else
            {
                Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[2] + " is largest");
                largest = disp3base;
            }
            firstChar += (char)largest;
            if (firstChar > 'Z') firstChar -= (char)26;

            Debug.Log("[Morsematics #" + thisLoggingID + "] After big add: " + firstChar + secondChar + "(" + (int)(firstChar - 'A' + 1) + "," + (int)(secondChar - 'A' + 1) + ")");

            foreach (char p in PRIME)
            {
                if (disp1base == p)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[0] + " is prime");
                    firstChar -= p;
                }
                if (disp2base == p)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[1] + " is prime");
                    firstChar -= p;
                }
                if (disp3base == p)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[2] + " is prime");
                    firstChar -= p;
                }
            }

            while (firstChar < 'A') firstChar += (char)26;
            while (secondChar < 'A') secondChar += (char)26;

            Debug.Log("[Morsematics #" + thisLoggingID + "] After prime: " + firstChar + secondChar + "(" + (int)(firstChar - 'A' + 1) + "," + (int)(secondChar - 'A' + 1) + ")");

            foreach (char s in SQUARE)
            {
                if (disp1base == s)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[0] + " is square");
                    secondChar -= s;
                }
                if (disp2base == s)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[1] + " is square");
                    secondChar -= s;
                }
                if (disp3base == s)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[2] + " is square");
                    secondChar -= s;
                }
            }

            while (firstChar < 'A') firstChar += (char)26;
            while (secondChar < 'A') secondChar += (char)26;

            Debug.Log("[Morsematics #" + thisLoggingID + "] After square: " + firstChar + secondChar + "(" + (int)(firstChar - 'A' + 1) + "," + (int)(secondChar - 'A' + 1) + ")");

            if (batteries > 0)
            {
                if (disp1base % batteries == 0)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[0] + " is divisible");
                    firstChar -= (char)disp1base;
                    secondChar -= (char)disp1base;
                }
                if (disp2base % batteries == 0)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[1] + " is divisible");
                    firstChar -= (char)disp2base;
                    secondChar -= (char)disp2base;
                }
                if (disp3base % batteries == 0)
                {
                    Debug.Log("[Morsematics #" + thisLoggingID + "] " + DisplayCharsRaw[2] + " is divisible");
                    firstChar -= (char)disp3base;
                    secondChar -= (char)disp3base;
                }
            }

            while (firstChar < 'A') firstChar += (char)26;
            while (secondChar < 'A') secondChar += (char)26;

            Debug.Log("[Morsematics #" + thisLoggingID + "] After batteries: " + firstChar + secondChar + "(" + (int)(firstChar - 'A' + 1) + "," + (int)(secondChar - 'A' + 1) + ")");

            if (firstChar == secondChar)
            {
                Answer = "" + firstChar;
                Debug.Log("[Morsematics #" + thisLoggingID + "] Characters match, answer: " + Answer);
            }
            else if (firstChar > secondChar)
            {
                char finalVal = (char)(firstChar - secondChar + 'A' - 1);
                Answer = "" + finalVal;
                Debug.Log("[Morsematics #" + thisLoggingID + "] First character is larger (diff), answer: " + Answer);
            }
            else
            {
                char finalVal = (char)(firstChar + secondChar - 'A' + 1);
                if (finalVal > 'Z') finalVal -= (char)26;
                Answer = "" + finalVal;
                Debug.Log("[Morsematics #" + thisLoggingID + "] Second character is larger (sum), answer: " + Answer);
            }
            var morsecode = Morsify(Answer);
            Answer += ": ";
            return morsecode.Aggregate(Answer, (current, dit) => current + (dit == 1 ? "-" : "."));
        }

        private static int[] Morsify(string text)
        {
            char[] values = text.ToCharArray();
            List<int> data = new List<int>();
            for (int a = 0; a < values.Length; a++)
            {
                if (a > 0) data.Add(-1);
                char c = values[a];
                switch (c)
                {
                    /*case ' ':
                        data.Add(-1);
                        break;*/
                    case 'A':
                        data.Add(0);
                        data.Add(1);
                        break;
                    case 'B':
                        data.Add(1);
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case 'C':
                        data.Add(1);
                        data.Add(0);
                        data.Add(1);
                        data.Add(0);
                        break;
                    case 'D':
                        data.Add(1);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case 'E':
                        data.Add(0);
                        break;
                    case 'F':
                        data.Add(0);
                        data.Add(0);
                        data.Add(1);
                        data.Add(0);
                        break;
                    case 'G':
                        data.Add(1);
                        data.Add(1);
                        data.Add(0);
                        break;
                    case 'H':
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case 'I':
                        data.Add(0);
                        data.Add(0);
                        break;
                    case 'J':
                        data.Add(0);
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        break;
                    case 'K':
                        data.Add(1);
                        data.Add(0);
                        data.Add(1);
                        break;
                    case 'L':
                        data.Add(0);
                        data.Add(1);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case 'M':
                        data.Add(1);
                        data.Add(1);
                        break;
                    case 'N':
                        data.Add(1);
                        data.Add(0);
                        break;
                    case 'O':
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        break;
                    case 'P':
                        data.Add(0);
                        data.Add(1);
                        data.Add(1);
                        data.Add(0);
                        break;
                    case 'Q':
                        data.Add(1);
                        data.Add(1);
                        data.Add(0);
                        data.Add(1);
                        break;
                    case 'R':
                        data.Add(0);
                        data.Add(1);
                        data.Add(0);
                        break;
                    case 'S':
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case 'T':
                        data.Add(1);
                        break;
                    case 'U':
                        data.Add(0);
                        data.Add(0);
                        data.Add(1);
                        break;
                    case 'V':
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        data.Add(1);
                        break;
                    case 'W':
                        data.Add(0);
                        data.Add(1);
                        data.Add(1);
                        break;
                    case 'X':
                        data.Add(1);
                        data.Add(0);
                        data.Add(0);
                        data.Add(1);
                        break;
                    case 'Y':
                        data.Add(1);
                        data.Add(0);
                        data.Add(1);
                        data.Add(1);
                        break;
                    case 'Z':
                        data.Add(1);
                        data.Add(1);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case '1':
                        data.Add(0);
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        break;
                    case '2':
                        data.Add(0);
                        data.Add(0);
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        break;
                    case '3':
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        data.Add(1);
                        data.Add(1);
                        break;
                    case '4':
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        data.Add(1);
                        break;
                    case '5':
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case '6':
                        data.Add(1);
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case '7':
                        data.Add(1);
                        data.Add(1);
                        data.Add(0);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case '8':
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        data.Add(0);
                        data.Add(0);
                        break;
                    case '9':
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        data.Add(0);
                        break;
                    case '0':
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        data.Add(1);
                        break;
                }
            }
            return data.ToArray();
        }

        private string GetLetter(string morse)
        {
            var data = new int[morse.Length];
            for (var i = 0; i < morse.Length; i++)
                data[i] = morse[i] == '.' ? 0 : morse[i] == '-' ? 1 : 2;
            return data.Max() < 2 ? GetLetter(data) : "?";
        }

        private string GetLetter(int[] val)
        {
            if (val.Length == 0) return "?";
            else if (val.Length > 5) return "?";
            else if (val[0] == 0)
            {
                //.
                if (val.Length == 1) return "E";
                else if (val[1] == 0)
                {
                    //..
                    if (val.Length == 2) return "I";
                    else if (val[2] == 0)
                    {
                        //...
                        if (val.Length == 3) return "S";
                        else if (val[3] == 0)
                        {
                            //....
                            if (val.Length == 4) return "H";
                            else if (val[4] == 0)
                            {
                                //.....
                                if (val.Length == 5) return "5";
                                else return "?";
                            }
                            else
                            {
                                //....-
                                if (val.Length == 5) return "4";
                                else return "?";
                            }
                        }
                        else
                        {
                            //...-
                            if (val.Length == 4) return "V";
                            else if (val[4] == 0) return "?";
                            else
                            {
                                //...--
                                if (val.Length == 5) return "3";
                                else return "?";
                            }
                        }
                    }
                    else
                    {
                        //..-
                        if (val.Length == 3) return "U";
                        else if (val[3] == 0)
                        {
                            //..-.
                            if (val.Length == 4) return "F";
                            else return "?";
                        }
                        else
                        {
                            //..--
                            if (val.Length == 4 || val[4] == 0) return "?";
                            else
                            {
                                //..---
                                if (val.Length == 5) return "2";
                                else return "?";
                            }
                        }
                    }
                }
                else
                {
                    //.-
                    if (val.Length == 2) return "A";
                    else if (val[2] == 0)
                    {
                        //.-.
                        if (val.Length == 3) return "R";
                        else if (val[3] == 0)
                        {
                            //.-..
                            if (val.Length == 4) return "L";
                            else return "?";
                        }
                        else return "?";
                    }
                    else
                    {
                        //.--
                        if (val.Length == 3) return "W";
                        else if (val[3] == 0)
                        {
                            //.--.
                            if (val.Length == 4) return "P";
                            else return "?";
                        }
                        else
                        {
                            //.---
                            if (val.Length == 4) return "J";
                            else if (val[4] == 0) return "?";
                            else
                            {
                                //.----
                                if (val.Length == 5) return "1";
                                else return "?";
                            }
                        }
                    }
                }
            }
            else
            {
                //-
                if (val.Length == 1) return "T";
                else if (val[1] == 0)
                {
                    //-.
                    if (val.Length == 2) return "N";
                    else if (val[2] == 0)
                    {
                        //-..
                        if (val.Length == 3) return "D";
                        else if (val[3] == 0)
                        {
                            //-...
                            if (val.Length == 4) return "B";
                            else if (val[4] == 0)
                            {
                                if (val.Length == 5) return "6";
                                else return "?";
                            }
                            else return "?";
                        }
                        else
                        {
                            //-..-
                            if (val.Length == 4) return "X";
                            else return "?";
                        }
                    }
                    else
                    {
                        //-.-
                        if (val.Length == 3) return "K";
                        else if (val[3] == 0)
                        {
                            //-.-.
                            if (val.Length == 4) return "C";
                            else return "?";
                        }
                        else
                        {
                            //-.--
                            if (val.Length == 4) return "Y";
                            else return "?";
                        }
                    }
                }
                else
                {
                    //--
                    //O890
                    if (val.Length == 2) return "M";
                    else if (val[2] == 0)
                    {
                        //--.
                        if (val.Length == 3) return "G";
                        else if (val[3] == 0)
                        {
                            //--..
                            if (val.Length == 4) return "Z";
                            else if (val[4] == 0)
                            {
                                if (val.Length == 5) return "7";
                                else return "?";
                            }
                            else return "?";
                        }
                        else
                        {
                            //--.-
                            if (val.Length == 4) return "Q";
                            else return "?";
                        }
                    }
                    else
                    {
                        //---
                        if (val.Length == 3) return "O";
                        else if (val[3] == 0)
                        {
                            //---.
                            if (val.Length == 4 || val[4] == 1) return "?";
                            else
                            {
                                //---..
                                if (val.Length == 5) return "8";
                                else return "?";
                            }
                        }
                        else
                        {
                            //----
                            if (val.Length == 4) return "?";
                            else if (val[4] == 0)
                            {
                                //----.
                                if (val.Length == 5) return "9";
                                else return "?";
                            }
                            else
                            {
                                //-----
                                if (val.Length == 5) return "0";
                                else return "?";
                            }
                        }
                    }
                }
            }
        }

    }
}