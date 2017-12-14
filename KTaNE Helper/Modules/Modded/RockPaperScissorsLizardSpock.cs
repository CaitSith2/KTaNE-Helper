using System;
using System.Collections.Generic;
using System.Linq;
using VanillaRuleGenerator.Edgework;

namespace KTaNE_Helper.Modules.Modded
{
    public class RockPaperScissorsLizardSpock
    {
        private KMBombInfo Bomb = new KMBombInfo();


        private static T[] newArray<T>(params T[] array)
        {
            return array;
        }

        public string GetPrimaryAnswer()
        {
            return GetAnswer(GetWinner(-1),-1);
        }

        public string GetPrimaryDecoy()
        {
            return GetDecoy(GetWinner(-1));
        }

        public string GetSecondaryAnswer()
        {
            return GetAnswer(GetWinner(GetWinner(-1)),GetWinner(-1));
        }

        private int GetWinner(int _decoy)
        {
            var serial = Bomb.GetSerialNumber();

            var scores = newArray(
                // Row 1: serial number letter
                serial.Contains('X') || serial.Contains('Y')
                    ? null
                    : newArray(
                        /* Rock */ serial.Count(c => c == 'R' || c == 'O'),
                        /* Paper */ serial.Count(c => c == 'P' || c == 'A'),
                        /* Scissors */ serial.Count(c => c == 'S' || c == 'I'),
                        /* Lizard */ serial.Count(c => c == 'L' || c == 'Z'),
                        /* Spock */ serial.Count(c => c == 'C' || c == 'K')
                    ),

                // Row 2: port
                Bomb.GetPortCount(KMBombInfoExtensions.KnownPortType.PS2) > 0
                    ? null
                    : newArray(
                        /* Rock */ Bomb.GetPortCount(KMBombInfoExtensions.KnownPortType.RJ45),
                        /* Paper */ Bomb.GetPortCount(KMBombInfoExtensions.KnownPortType.Parallel),
                        /* Scissors */ Bomb.GetPortCount(KMBombInfoExtensions.KnownPortType.Serial),
                        /* Lizard */ Bomb.GetPortCount(KMBombInfoExtensions.KnownPortType.DVI),
                        /* Spock */ Bomb.GetPortCount(KMBombInfoExtensions.KnownPortType.StereoRCA)
                    ),

                // Row 3: lit indicator
                Bomb.IsIndicatorOn(KMBombInfoExtensions.KnownIndicatorLabel.TRN)
                    ? null
                    : newArray(
                        /* Rock */ Bomb.GetOnIndicators().Count(i => i == "FRK" || i == "FRQ"),
                        /* Paper */ Bomb.GetOnIndicators().Count(i => i == "BOB" || i == "IND"),
                        /* Scissors */ Bomb.GetOnIndicators().Count(i => i == "CAR" || i == "SIG"),
                        /* Lizard */ Bomb.GetOnIndicators().Count(i => i == "CLR" || i == "NSA"),
                        /* Spock */ Bomb.GetOnIndicators().Count(i => i == "SND" || i == "MSA")
                    ),

                // Row 4: unlit indicator
                Bomb.IsIndicatorOff(KMBombInfoExtensions.KnownIndicatorLabel.TRN)
                    ? null
                    : newArray(
                        /* Rock */ Bomb.GetOffIndicators().Count(i => i == "FRK" || i == "FRQ"),
                        /* Paper */ Bomb.GetOffIndicators().Count(i => i == "BOB" || i == "IND"),
                        /* Scissors */ Bomb.GetOffIndicators().Count(i => i == "CAR" || i == "SIG"),
                        /* Lizard */ Bomb.GetOffIndicators().Count(i => i == "CLR" || i == "NSA"),
                        /* Spock */ Bomb.GetOffIndicators().Count(i => i == "SND" || i == "MSA")
                    ),

                // Row 5: serial number digits
                newArray(
                    /* Rock */ serial.Count(c => c == '0' || c == '5'),
                    /* Paper */ serial.Count(c => c == '3' || c == '6'),
                    /* Scissors */ serial.Count(c => c == '1' || c == '9'),
                    /* Lizard */ serial.Count(c => c == '2' || c == '8'),
                    /* Spock */ serial.Count(c => c == '4' || c == '7')
                )
            );

            var result = scores
                .Select((row, ix) => row == null
                    ? null
                    : row.Max().Apply(maxScore => new
                    {
                        Row = ix,
                        Winners = row.SelectIndexWhere(sc => sc == maxScore).ToArray()
                    }))
                .Where(inf => inf != null && inf.Winners.Length == 1 && inf.Winners[0] != _decoy)
                .FirstOrDefault();

            if (result != null)
                return result.Winners[0];
            return -1;
        }

        private string GetDecoy(int winner)
        {
            switch (winner)
            {
                //private static readonly string[] _names = new[] { "Rock", "Paper", "Scissors", "Lizard", "Spock" };
                case 0: return "When decoy is not rock";
                case 1: return "When decoy is not paper";
                case 2: return "When decoy is not scissors";
                case 3: return "When decoy is not lizard";
                case 4: return "When decoy is not spock";
                default: return "";
            }
        }

        private string GetAnswer(int winner, int decoy)
        {
            if (Bomb.GetSerialNumber().Length != 6) return "";
            if (winner == -1)
            {
                switch (decoy)
                {
                    case 0: return "press paper spock scissors lizard";
                    case 1: return "press scissors lizard rock spock";
                    case 2: return "press rock spock lizard paper";
                    case 3: return "press rock scissors paper spock";
                    case 4: return "press lizard paper rock scissors";
                    default: return "press paper spock scissors lizard rock";
                }
            }


            switch (winner)
            {
                //private static readonly string[] _names = new[] { "Rock", "Paper", "Scissors", "Lizard", "Spock" };
                case 0: return "press paper spock";
                case 1: return "press scissors lizard";
                case 2: return "press rock spock";
                case 3: return "press rock scissors";
                case 4: return "press lizard paper";
                default: return "press Everything but the decoy";
            }
        }

        
    }

    public static class Extensions
    {
        /// <summary>
        ///     Returns a collection of integers containing the indexes at which the elements of the source collection match
        ///     the given predicate.</summary>
        /// <typeparam name="T">
        ///     The type of elements in the collection.</typeparam>
        /// <param name="source">
        ///     The source collection whose elements are tested using <paramref name="predicate"/>.</param>
        /// <param name="predicate">
        ///     The predicate against which the elements of <paramref name="source"/> are tested.</param>
        /// <returns>
        ///     A collection containing the zero-based indexes of all the matching elements, in increasing order.</returns>
        public static IEnumerable<int> SelectIndexWhere<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            return selectIndexWhereIterator(source, predicate);
        }

        private static IEnumerable<int> selectIndexWhereIterator<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            int i = 0;
            using (var e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (predicate(e.Current))
                        yield return i;
                    i++;
                }
            }
        }

        /// <summary>
        ///     Executes the specified function with the specified argument.</summary>
        /// <typeparam name="TSource">
        ///     Type of the argument to the function.</typeparam>
        /// <typeparam name="TResult">
        ///     Type of the result of the function.</typeparam>
        /// <param name="source">
        ///     The argument to the function.</param>
        /// <param name="func">
        ///     The function to execute.</param>
        /// <returns>
        ///     The result of the function.</returns>
        public static TResult Apply<TSource, TResult>(this TSource source, Func<TSource, TResult> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");
            return func(source);
        }
    }
}