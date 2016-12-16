using System;
using System.Collections.Generic;
using System.Linq;

namespace KTaNE_Helper
{
    public class _3DMaze
    {
        private static readonly string[][][] MazePaths = new string[10][][];

        public _3DMaze()
        {
            for (var mapIndex = 0; mapIndex < 10; mapIndex++)
            {
                MazePaths[mapIndex] = new string[9][];
                var letters = new List<string>();
                string[] cardinals;
                string[] northWall;
                string[] westWalls;
                switch (mapIndex)
                {
                    case 0:
                        cardinals = new[]{"     A  ", " *A    B", "A  B C  ", " C  *  B", "    A   ", " B C  B ", "* C     ", "    A C "};
                        northWall = new[]{"11000110", "00001000", "01011100", "00100011", "10011001", "00000100", "00110010", "11000001"};
                        westWalls = new[]{"10101001", "10100100", "00010001", "01010110", "01010001", "01100100", "01001101", "00101100"};
                        break;

                    case 1:
                        cardinals = new[]{"A  B  A*", "  D     ", "     D B", " A B    ", "  *   A ", "D   A   ", "  B  D  ", " D  *  B"};
                        northWall = new[]{"10100011", "01100100", "00011010", "00100000", "11100110", "00010100", "10100001", "00011000"};
                        westWalls = new[]{"10000010", "11000100", "00010001", "01000100", "10010001", "00010100", "01000101", "00010100"};
                        break;

                    case 2:
                        cardinals = new[]{"B    A H", "* H     ", "B   B   ", "    * HA", " A H    ", "    A B ", " B   *  ", "A  H    "};
                        northWall = new[]{"11101011", "01011000", "10001101", "01010000", "00001000", "00110100", "00101110", "01100000"};
                        westWalls = new[]{"10010000", "01000001", "00110011", "11010101", "11010010", "11100000", "00010101", "00000001"};
                        break;

                    case 3:
                        cardinals = new[]{"D       ", "  C D* C", " *   C  ", " A      ", "D  C D  ", "  A  * A", "   A  D ", "A    C  "};
                        northWall = new[]{"10111011", "01100110", "00000111", "01000000", "11101110", "01100100", "00010110", "01101100"};
                        westWalls = new[]{"00001000", "11001100", "01110010", "10011001", "10001001", "01100101", "01010000", "10000010"};
                        break;

                    case 4:
                        cardinals = new[]{"H C   A ", "*   H   ", "      *C", " A   H  ", "C H C A ", " *     A", "   C H  ", "  A     "};
                        northWall = new[]{"00111100", "11010000", "01100110", "00000000", "01000100", "00000001", "11100010", "01011011"};
                        westWalls = new[]{"10000110", "10010001", "01010101", "10000001", "11000000", "01010101", "10010000", "01000010"};
                        break;

                    case 5:
                        cardinals = new[]{"D D  *  ", "    H  A", " *H   A ", "A  D    ", "    HD  ", "* H    A", "D       ", "   A H  "};
                        northWall = new[]{"01110101", "00001110", "00000111", "01001100", "00110101", "11100000", "01100000", "11001000"};
                        westWalls = new[]{"10100100", "11110000", "11011000", "01110000", "10000101", "10001110", "00011111", "10000101"};
                        break;

                    case 6:
                        cardinals = new[]{"     B  ", "C D   * ", " * B  C ", " C    B ", "    C  D", "B    D  ", " C  * D ", "D  B    "};
                        northWall = new[]{"01011110", "01101011", "01100100", "10000111", "10110011", "10011001", "01100010", "10111001"};
                        westWalls = new[]{"10010000", "00001010", "11101000", "00001000", "00100010", "00010001", "00000110", "00100001"};
                        break;

                    case 7:
                        cardinals = new[]{"C   H   ", "  C    H", "  * B   ", "B  H*   ", " H   B C", "   *    ", "  B C   ", " C   H B"};
                        northWall = new[]{"10110011", "01010111", "10101100", "00000110", "01111110", "00100010", "10010100", "00000110"};
                        westWalls = new[]{"10000100", "01001000", "01111001", "10001000", "10001000", "01101101", "01101001", "00010010"};
                        break;

                    case 8:
                        cardinals = new[]{"  D B  H", "   *  D ", "  H *  B", "D    B  ", "    D  H", "  B     ", "   H  H*", "D    B  "};
                        northWall = new[]{"00100001", "00010001", "11011000", "11011011", "00010011", "10000000", "01101100", "01001111"};
                        westWalls = new[]{"01101000", "11010111", "00000110", "00100000", "00110100", "10001110", "11000000", "00011010"};
                        break;

                    default:
                        cardinals = new[]{"  H  D  ", "    C*  ", "   H   D", "H    D  ", "  C     ", "C  D C H", "*D  H * ", "       C"};
                        northWall = new[]{"01011010", "00100100", "00000000", "01000010", "01000010", "01100110", "01011010", "10100101"};
                        westWalls = new[]{"11001001", "11110111", "00100010", "00011100", "10011100", "10001000", "11000001", "00101010"};
                        break;
                }
                for (var y = 0; y < 8; y++)
                {
                    MazePaths[mapIndex][y] = new string[8];
                    for (var x = 0; x < 8; x++)
                    {
                        MazePaths[mapIndex][y][x] = "";
                        var letter = cardinals[y].Substring(x, 1);
                        if (!letters.Contains(letter)) letters.Add(letter);
                        var north = northWall[y].Substring(x, 1);
                        var south = northWall[(y + 1)%8].Substring(x, 1);
                        var west = westWalls[y].Substring(x, 1);
                        var east = westWalls[y].Substring((x + 1)%8, 1);
                        if (north == "0") MazePaths[mapIndex][y][x] += "un";
                        if (west == "0") MazePaths[mapIndex][y][x] += "lw";
                        if (east == "0") MazePaths[mapIndex][y][x] += "re";
                        if (south == "0") MazePaths[mapIndex][y][x] += "ds";
                        MazePaths[mapIndex][y][x] += "," + letter;
                    }
                }
                letters.Remove(" ");
                letters.Remove("*");
                letters.Sort();
                MazePaths[mapIndex][8] = new [] {letters[0] + letters[1] + letters[2]};
            }

        }

        public bool IsTravelPossible(string[][] maze, int x, int y, string direction)
        {
            return maze[x][y].Split(',')[0].Contains(direction.ToLower());
        }

        public string MazeLetterAtLocation(string[][] maze, int x, int y)
        {
            return maze[x][y].Split(',')[1];
        }

        public List<int[]> GetLocationList(string line, string maze)
        {
            var locations = new List<int[]>();

            var m = GetMaze(maze);
            if (m == null) return locations;
            

            var rows = true;
            do
            {

                for (var i = 0; i < 8; i++)
                {
                    var j = 0;
                    while (IsTravelPossible(m, rows ? i : j, rows ? j : i, rows ? "l" : "u"))
                    {
                        j++;
                        j %= 8;
                        if (j == 0) break;
                    }

                    var jj = j;
                    do
                    {
                        var mazeline = "";
                        var oj = j;
                        do
                        {
                            mazeline += MazeLetterAtLocation(m, rows ? i : j, rows ? j : i);
                            j++;
                            j %= 8;
                        } while (IsTravelPossible(m, rows ? i : j, rows ? j : i, rows ? "l" : "u") && j != jj);

                        if (line.Length != mazeline.Length) continue;

                        var rj = (oj + line.Length - 1) % 8;
                        if (line.Replace('*', ' ').ToUpper() == mazeline.Replace('*',' '))
                            locations.Add(
                                new[]
                                {
                                    rows ? i : rj,
                                    rows ? rj : i,
                                    rows ? 1 : 2,
                                    mazeline.Contains('*') ? 1 : 0
                                });
                        if (line.Replace('*', ' ').ToUpper() == mazeline.Reverse().Replace('*', ' '))
                            locations.Add(
                                new[]
                                {
                                    rows ? i : oj,
                                    rows ? oj : i,
                                    rows ? 3 : 0,
                                    mazeline.Contains('*') ? 1 : 0
                                });

                    } while (j != jj);
                }
                rows = !rows;  //Switch to columns after first round.  Finished after second round.
            }
            while (!rows);

            return locations;
        }

        public string FindLocation(string line, string maze)
        {
            var walls = new[] { ", Facing North Wall", ", Facing East Wall", ", Facing South Wall", ", Facing West Wall" };
            var cardinal = new[] { "", ", Cardinal present" };

            var m = GetLocationList(line, maze);
            var locations = m.Select(l => "Row: " + l[0] + ", Column: " + l[1] + walls[l[2]] + cardinal[l[3]]).ToList();

            if (locations.Count == 0) return "Location not found";

            var result = "";
            foreach (var s in locations)
            {
                if (string.IsNullOrEmpty(result))
                    result = s;
                else
                {
                    result += Environment.NewLine + s;
                }
            }
            return locations.Count > 6 ? "Too Many Locations possible" : result;
        }

        public string[][] GetMaze(string maze)
        {
            var letters = string.Concat(maze.ToUpper().Distinct().OrderBy(c => c));
            return MazePaths.FirstOrDefault(m => m[8][0] == letters);
        }
    }
}