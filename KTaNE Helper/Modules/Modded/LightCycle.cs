using System;
using System.Linq;
using VanillaRuleGenerator.Edgework;

namespace KTaNE_Helper.Modules.Modded
{
    public static class LightCycle
    {
        private static readonly string[][] LightCycleTable = @"
5B;BR;MG;Y5;41;RW;64;16;23;3M;GY;W2
2R;6M;43;5B;R5;Y2;1G;MY;W6;34;BW;G1
MY;24;YR;35;W2;GB;1W;R3;5G;46;BM;61
56;63;14;M2;RY;2M;WR;BG;YW;3B;G1;45
BR;W2;23;14;MB;56;YW;RM;GY;6G;35;41
RY;2G;1M;Y5;5R;WB;63;B1;M4;G6;32;4W
Y1;54;2W;RY;1R;B3;6G;G6;MB;W5;42;3M
35;WY;G2;2B;5G;MR;B3;14;46;YM;6W;R1
RM;45;5W;B1;M6;32;WB;GY;YR;14;6G;23
WB;R6;5Y;41;25;Y3;MW;32;BG;GM;1R;64
64;B2;WG;R5;G1;2Y;YR;MB;16;3W;53;4M
64;B5;W6;1G;R2;4R;GW;3M;2B;Y3;5Y;M1
W3;3G;24;YM;M2;R5;6R;B6;GY;5B;1W;41
1Y;6M;21;GR;3G;5B;R4;43;W2;YW;B5;M6
R5;3G;23;W4;B2;1M;56;M1;4Y;GB;6R;YW
14;4B;62;3W;MR;Y6;BY;2G;5M;G5;R3;W1
5G;MB;4W;Y2;RM;W4;61;36;BY;15;GR;23
MG;56;GM;W5;Y2;R4;B1;1B;2R;43;6W;3Y
RY;65;5G;GB;WM;43;1W;B1;36;24;Y2;MR
G3;B2;6W;MB;15;Y4;5M;WR;46;3Y;2G;R1
51;W3;45;34;YW;1Y;BG;62;M6;GR;2M;RB
M6;6B;1G;35;WR;B4;GM;R1;2W;52;4Y;Y3
YM;B1;53;2G;32;R5;14;W6;4W;GR;MY;6B
42;RB;W5;YM;2Y;51;BR;G3;MG;36;6W;14
GY;1R;54;4G;3B;M6;25;Y2;R1;W3;BW;6M
GB;BG;15;M1;3M;R3;YW;6Y;52;46;WR;24
2R;RB;5G;W2;Y1;4Y;35;1M;BW;G6;64;M3
R4;W6;32;2W;4Y;65;BR;5G;YB;GM;M1;13
4B;B3;64;W1;MY;R6;G5;YW;52;2R;3G;1M
B6;M3;4B;14;25;Y1;GY;RW;WG;52;6M;3R
MR;2B;W5;6Y;B3;42;G1;Y6;5G;3M;RW;14
Y1;56;1W;W4;BG;G5;4M;2B;3R;63;M2;RY
34;WB;YG;5M;R1;GW;12;6Y;BR;M6;43;25
4G;65;Y4;GB;31;MY;53;1M;2R;R2;BW;W6
YB;R2;WR;53;1W;35;BM;G4;6Y;4G;21;M6
GY;31;5M;R2;6W;MB;Y6;24;4G;B5;1R;W3
".Replace("\r", "").Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(line => line.Split(';')).ToArray();

        private static int convert(char ch)
        {
            return ch >= 'A' && ch <= 'Z' ? ch - 'A' : ch - '0' + 26;
        }

        public static string GetAnswer(char[] solution)
        {
            if (!SerialNumber.IsSerialValid) return "";
            var answer = "";


            for (var i = 0; i < 6; i++)
            {
                var ch1 = convert(SerialNumber.Serial[i]);
                var ch2 = convert(SerialNumber.Serial[5 - i]);
                var entry = LightCycleTable[ch1][ch2 / 3];
                if (solution.Length != 6)
                {
                    answer += entry;
                    if (i != 5)
                        answer += " ";
                    continue;
                }
                var ix1 = Array.IndexOf(solution, entry[0]);
                if (ix1 == -1)
                    ix1 = entry[0] - '1';

                var ix2 = Array.IndexOf(solution, entry[1]);
                if (ix2 == -1)
                    ix2 = entry[1] - '1';

                var t = solution[ix1];
                solution[ix1] = solution[ix2];
                solution[ix2] = t;
                answer = $"{solution[0]}{solution[1]}{solution[2]} {solution[3]}{solution[4]}{solution[5]}";
            }
            return answer;
        }
    }
}