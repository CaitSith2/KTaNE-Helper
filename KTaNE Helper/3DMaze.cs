using System;
using System.Collections.Generic;
using System.Linq;

namespace KTaNE_Helper
{
    public static class _3DMaze
    {
        private static readonly string[][][] MazePaths =
        {
            new[] //ABC
            {
                new[] {"dr", "ld", "urd", "uld", "ur", "lrd,a", "ld", "ud"},
                new[] {"urd", "ul,*", "urd,a", "ulr", "l", "ur", "ulrd", "uld,b"},
                new[] {"ulrd,a", "lrd", "ul", "rd,b", "lrd", "lrd,c", "ul", "ur"},
                new[] {"lu", "urd,c", "ld", "ur", "ul,*", "ud", "rd", "lr,b"},
                new[] {"ld", "urd", "uld", "rd", "lrd,a", "ulr", "uld", "rd"},
                new[] {"uld", "ud,b", "ur", "ulr,c", "ul", "rd", "ulr,b", "ulrd"},
                new[] {"ul,*", "ur", "lrd,c", "ld", "ud", "urd", "ld", "ur"},
                new[] {"lr", "l", "urd", "uld", "ud,a", "ur", "ulr,c", "lrd"},
                new[] {"ABC"}
            },
            new[] //ABD
            {
                new[] {"rd,a", "ulr", "lr", "ulrd,b", "ulrd", "ul", "rd,a", "ld,*"},
                new[] {"ud", "rd", "lrd,d", "ulr", "ul", "rd", "ulr", "uld"},
                new[] {"ulrd", "ulrd", "ul", "rd", "lrd", "ulrd,d", "ld", "urd,b"},
                new[] {"ul", "ur,a", "lr", "ulrd,b", "uld", "ur", "ulr", "ulrd"},
                new[] {"rd", "lrd", "ld,*", "ur", "ulrd", "lr", "ld,a", "ud"},
                new[] {"ulr,d", "ulrd", "ul", "rd", "uld,a", "rd", "ulrd", "ulr"},
                new[] {"ld", "urd", "lrd,b", "ulr", "ul", "urd,d", "uld", "rd"},
                new[] {"ulr", "ulrd,d", "ul", "rd", "ld,*", "urd", "ulr", "ulr,b"},
                new[] {"ABD"}
            },
            new[]
            {
                new[] {"rd,b","lr","ld","ur","lr","ulrd,a","lrd","ld,h"},
                new[] {"lu,*","rd","ulrd,h","lrd","lr","ulr","uld","ur"},
                new[] {"lrd,b","ul","ud","ur","lrd,b","ld","ud","rd"},
                new[] {"ud","rd","uld","rd","ul,*","urd","uld,h","ud,a"},
                new[] {"ud","urd,a","ul","ur,h","lrd","ul","urd","uld"},
                new[] {"ud","ud","r","ldr","ulr,a","lr","ulr,b","uld"},
                new[] {"ulrd","ulr,b","l","urd","ld","rd,*","ld","urd"},
                new[] {"ulr,a","lr","lr","ulrd,h","ulr","ulrd","ul","ur"},
                new[] {"ABH"},
            },
            new[]
            {
                new[] {"lrd,d","ulr","lr","ld","rd","ulr","lr","lrd"},
                new[] {"ud","rd","lrd,c","uld","ud,d","r,*","lr","ul,c"},
                new[] {"uld","u,*","ud","urd","ulrd","ld,c","rd","lrd"},
                new[] {"ur","lr,a","ul","ud","ur","ulr","ul","ud"},
                new[] {"rd,d","lr","lr","uld,c","rd","lr,d","ld","ud"},
                new[] {"uld","d","rd,a","ulr","uld","r,*","ul","urd,a"},
                new[] {"uld","ur","ul","rd,a","ulr","lr","lrd,d","ulrd"},
                new[] {"ur,a","lrd","lr","ulr","lr","ld,c","ur","ul"},
                new[] {"ACD"},
            },
            new[]
            {
                new[] {"ur,h","ulr","lrd,c","lr","ld","d","urd,a","uld"},
                new[] {"rd,*","lr","ul","rd","ulrd,h","ulr","ul","ud"},
                new[] {"uld","rd","ld","urd","uld","rd","ld,*","urd,c"},
                new[] {"urd","ulr,a","ulrd","ulrd","ulrd","ulr,h","uld","ud"},
                new[] {"uc,c","rd","ulrd,h","ulrd","ulrd,c","lrd","ulrd,a","ul"},
                new[] {"ul","ur,*","ul","urd","uld","urd","ul","rd,a"},
                new[] {"rd","lr","ld","ur,c","ulr","ulrd,h","lr","ul"},
                new[] {"uld","rd","ulr,a","lr","lr","ul","rd","lrd"},
                new[] {"ACH"},
            },
            new[]
            {
                new[] {"urd,d","ld","rd,d","lrd","ul","r,*","ulr","ld"},
                new[] {"ud","ud","ud","urd","lrd,h","lr","lr","ul,a"},
                new[] {"ud","ur,*","uld,h","ud","ur","lr","lrd,a","ld"},
                new[] {"uld,a","d","u","ur,d","lrd","lr","ulrd","ulr"},
                new[] {"ur","ulr","lr","lrd","uld,h","rd,d","uld","d"},
                new[] {"rd,*","lr","lr,h","uld","ud","ud","urd","uld,a"},
                new[] {"ulr,d","lr","ld","ud","u","ud","ud","urd"},
                new[] {"rd","lr","ulr","ulr,a","ld","ur,h","uld","u"},
                new[] {"ADH"},
            },
            new[]
            {
                new[] {"urd","lr","ul","rd","lr","lrd,b","lr","ul"},
                new[] {"ulrd,c","lr","lr,d","uld","rd","ul","rd,*","lrd"},
                new[] {"u","d,*","rd","uld,b","urd","lr","ulr,c","ul"},
                new[] {"lr","ulrd,c","ulr","ul","urd","lrd","lr,b","lr"},
                new[] {"lr","uld","rd","lr","ulr,c","uld","rd","lr,d"},
                new[] {"lrd,b","ulr","ul","rd","lrd","ulrd,d","ul","rd"},
                new[] {"ulr","lrd","lr","ulr","ul,*","ud","rd,d","ulr"},
                new[] {"lrd,d","ul","rd","lr,b","lr","ulr","ul","rd"},
                new[] {"BCD"},
            },
            new[]
            {
                new[] {"rd,c","ulr","lrd","lr","uld,h","ur","lr","l"},
                new[] {"ul","rd","ulr,c","ld","ur","lr","lrd","lrd,h"},
                new[] {"ld","ud","d,*","ud","rd,b","lr","ul","urd"},
                new[] {"urd,b","ulr","ulr","ul,h","ur,*","lr","lr","uld"},
                new[] {"urd","lrd,h","lr","ld","rd","lrd,b","lr","uld,c"},
                new[] {"ul","ud","rd","ul,*","ud","ur","ld","urd"},
                new[] {"ld","ud","urd,b","ld","urd,c","lr","ul","urd"},
                new[] {"ulr","ulrd,c","ul","ur","ulrd","ld,h","r","ulr,b"},
                new[] {"BCH"},
            },
            new[]
            {
                new[] {"uld","ud","rd,d","ul","urd,b","ulrd","ulrd","lr,h"},
                new[] {"u","ur","uld","r,*","lu","ud","ud,d","d"},
                new[] {"lr","lr","ulrd,h","lr","l,*","ud","ur","ulr"},
                new[] {"lrd,d","ld","urd","lr","lrd","ulrd,b","lr","lr"},
                new[] {"ulr","uld","ud","rd","uld,d","urd","lrd","lrd,h"},
                new[] {"rd","ulr","ulr,b","uld","u","u","urd","uld"},
                new[] {"ud","r","lrd","ulrd,h","lr","lr","ulr,h","ul,*"},
                new[] {"ulrd,d","lrd","ul","ud","rd","ld,b","rd","lr"},
                new[] {"BDH"},
            },
            new[]
            {
                new[] {"ud","rd","ulr,h","ld","rd","ulr,d","ld","ud"},
                new[] {"ud","ud","d","urd","uld,c","d,*","ud","ud"},
                new[] {"ulrd","ul","urd","ulrd,h","ulrd","uld","ur","ulrd,d"},
                new[] {"ulrd,h","lr","uld","ud","ud","urd,d","lr","ulrd"},
                new[] {"urd","lr","ul","ud","ud","ur","lr","uld"},
                new[] {"urd,c","lr","lrd","ul,d","ur","lrd,c","lr","uld,h"},
                new[] {"u,*","rd,d","ulr","lrd","lrd,h","ulr","ld,*","u"},
                new[] {"lrd","ul","rd","ul","ur","ld","ur","lrd,c"},
                new[] {"CDH"},
            },

        };

        public static bool IsTravelPossible(string[][] maze, int x, int y, string direction)
        {
            return maze[x][y].Split(',')[0].Contains(direction);
        }

        public static string MazeLetterAtLocation(string[][] maze, int x, int y)
        {
            var l = maze[x][y].Split(',');
            return l.Length > 1 ? l[1] : " ";
        }

        public static List<int[]> GetLocationList(string line, string maze)
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
                        if (j == 0) return locations;
                    }

                    var jj = j;
                    do
                    {
                        var locationReverse = new [] {(rows ? i : j), (rows ? j : i), (rows ? 3 : 0), 0};
                        var rj = (j + line.Length - 1) % 8;
                        var location = new [] {rows ? i : rj, rows ? rj : i, rows ? 1 : 2, 0};
                        var mazeline = "";
                        do
                        {
                            mazeline += MazeLetterAtLocation(m, rows ? i : j, rows ? j : i);
                            j++;
                            j %= 8;
                        } while (IsTravelPossible(m, rows ? i : j, rows ? j : i, rows ? "l" : "u"));

                        if (line.Length != mazeline.Length) continue;

                        var matched = false;
                        if (line.ToLower() == mazeline)
                        {
                            if (line.Contains('*')) location[3] = 1;
                            locations.Add(location);
                            matched = true;
                        }
                        if (!matched && line.Replace('*', ' ').ToLower() == mazeline.Replace('*',' '))
                        {
                            if (mazeline.Contains(' ')) location[3] = 2;
                            locations.Add(location);
                        }

                        matched = false;
                        if (line.ToLower() == mazeline.Reverse())
                        {
                            if (line.Contains('*')) locationReverse[3] = 1;
                            locations.Add(locationReverse);
                            matched = true;
                        }
                        if (matched || line.Replace('*', ' ').ToLower() != mazeline.Reverse().Replace('*', ' '))
                            continue;
                        if (mazeline.Contains(' ')) locationReverse[3] = 2;
                        locations.Add(locationReverse);
                    } while (j != jj);
                }
                rows = !rows;
            }
            while (!rows);

            return locations;
        }

        public static string FindLocation(string line, string maze)
        {
            var walls = new[] { ", Facing North Wall", ", Facing East Wall", ", Facing South Wall", ", Facing West Wall" };
            var cardinal = new[] { "", ", Cardinal is real", ", Cardinal present" };

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

        public static string[][] GetMaze(string maze)
        {
            var letters = string.Concat(maze.ToUpper().Distinct().OrderBy(c => c));
            return MazePaths.FirstOrDefault(m => m[8][0] == letters);
        }
    }
}