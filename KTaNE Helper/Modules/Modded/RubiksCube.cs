using System;
using System.Collections.Generic;
using System.Linq;
using VanillaRuleGenerator.Edgework;

namespace KTaNE_Helper.Modules.Modded
{
    sealed class FaceRotation
    {
        public string Name { get; private set; }
        public FaceRotation(string name)
        {
            Name = name;
        }
        public FaceRotation Reverse { get; set; }
        public FaceRotation[] OppositeSide { get; set; }
    }

    public class RubiksCube
    {
        private KMBombInfo Bomb = new KMBombInfo();

        public string GetAnswer(string faceColors)
        {
            var table = @"L’,F’;D’,U’;U,B’;F,B;L,D;R’,U;U’,F;B’,L’;B,R;D,L;R,D’;F’,R’".Split(';').Select(row => row.Split(',').Select(str => _moves[str]).ToArray()).ToArray();

            var colorLetters = "YBRGOW".ToArray().ToList();
            var colorLettersIndex = "YBRGOW".ToArray().ToList();
            if (faceColors.Length < 5 || Bomb.GetSerialNumber().Length != 6)
                return "Input U L F D R colors";

            var colors = new int[6];
            for (var i = 0; i < 5; i++)
            {
                if (!colorLetters.Contains(faceColors[i])) return "No Duplicate Colors";
                colorLetters.Remove(faceColors[i]);
                colors[i] = colorLettersIndex.IndexOf(faceColors[i]);
            }
            colors[5] = colorLettersIndex.IndexOf(colorLetters[0]);

            var columnShifts = newArray(colors[0] + 1, colors[1] + 1, colors[2] + 1);
            var serialIgnore = colors[3];
            var colR = colors[4];   // color of the R (right) face

            var ser = Bomb.GetSerialNumber().Remove(serialIgnore, 1);
            var rows = ser.Select(ch => ch >= '0' && ch <= '9' ? ch - '0' : ch - 'A' + 10).Select(n => (n / 3 + columnShifts[n % 3]) % table.Length).ToArray();
            var moves1 = (colR >= 1 && colR <= 3)
                ? rows.SelectMany(r => table[r]).ToList()
                : rows.Select(r => table[r][0]).Concat(rows.Select(r => table[r][1])).ToList();

            var moves2 = moves1.ToList();
            switch (colR)
            {
                case 2: // red
                case 0: // yellow
                    for (int i = 0; i < 5; i++)
                        moves2[i] = moves2[i].Reverse;
                    break;

                case 3: // green
                case 5: // white
                    for (int i = 0; i < 5; i++)
                    {
                        var t = moves2[i];
                        moves2[i] = moves2[9 - i];
                        moves2[9 - i] = t;
                    }
                    break;
            }

            // Now try to minimize the sequence
            var ix = 0;
            var moves = moves2.ToList();
            while (ix < moves.Count)
            {
                var n = 1;
                var affected = new List<int>();
                for (int j = ix + 1; j < moves.Count; j++)
                {
                    if (moves[j] == moves[ix])
                    {
                        n++;
                        affected.Add(j);
                    }
                    else if (moves[j] == moves[ix].Reverse)
                    {
                        n--;
                        affected.Add(j);
                    }
                    else if (!moves[ix].OppositeSide.Contains(moves[j]))
                        break;
                }

                switch ((n % 4 + 4) % 4)
                {
                    case 0:
                        // the moves cancel each other out completely.
                        for (int k = affected.Count - 1; k >= 0; k--)
                            moves.RemoveAt(affected[k]);
                        moves.RemoveAt(ix);
                        ix = 0;
                        continue;

                    case 3:
                        // e.g. 3 of the same move ⇒ reverse move
                        for (int k = affected.Count - 1; k >= 0; k--)
                            moves.RemoveAt(affected[k]);
                        moves[ix] = moves[ix].Reverse;
                        ix = 0;
                        continue;
                }

                ix++;
            }

            return moves.Aggregate("", (current, m) => current + (m.Name + " ")).Trim();
        }

        private static T[] newArray<T>(params T[] array)
        {
            return array;
        }

        private static Dictionary<string, FaceRotation> _moves;

        static RubiksCube()
        {
            var moves = newArray(
                new FaceRotation("F"),
                new FaceRotation("F’"),
                new FaceRotation("B"),
                new FaceRotation("B’"),
                new FaceRotation("L"),
                new FaceRotation("L’"),
                new FaceRotation("R"),
                new FaceRotation("R’"),
                new FaceRotation("U"),
                new FaceRotation("U’"),
                new FaceRotation("D"),
                new FaceRotation("D’"));

            for (int i = 0; i < moves.Length; i++)
            {
                moves[i].Reverse = moves[i ^ 1];
                moves[i].OppositeSide = new[] { moves[i ^ 2], moves[i ^ 3] };
            }

            _moves = moves.ToDictionary(f => f.Name, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}