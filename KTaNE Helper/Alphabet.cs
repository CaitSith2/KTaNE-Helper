using System;
using System.Linq;

namespace KTaNE_Helper
{
    public static class Alphabet
    {
        private static string CanSpell(string query, string with) // Query is the thing you're searching for, With is the string you're checking whether it can spell Query or not
        {

            var can = 0; // tells how many characters we could spell

            var newWith = ""; // this stores the unused characters

            for (var i = with.Length - 1; i >= 0; i--) // start from the end so removing doesn't fuck up stuff if we went up
            {

                var search = query.Substring(0, Math.Min(query.Length, with.Length)); // the portion of the string we're searching for
                // ReSharper disable once InconsistentNaming
                var _Query = with.Substring(i, 1); // the search query
                                                   //Debug.Log (i + ", " + Search + ", " + _Query);

                if (search.Contains(_Query))
                {
                    //Debug.Log ("CONTAINS " + _Query);
                    // we can spell this character
                    can++; // so count it
                }
                else
                {
                    newWith += with.Substring(i, 1); // otherwise, this is an unusable character, so it's a 'leftover'
                }
            }
            //Debug.Log ("> " + Can + ", " + originalSize );
            return can >= query.Length ? newWith : with; // if we managed to spell enough characters, return the leftovers, otherwise return the original With variable
        }

        public static string GetOrder(string label)
        {
            var wordBank = new[]
            {
                "JQXZ","PQJS","OKBV","QYDX","IRNM","ARGF",
                "LXE","QEW","TJL","VCN","HDU","PKD",
                "VSI","DFW","ZNY","YKQ","GS","AC","JR","OP"
            };
            var words4 = wordBank.Where(data => data.Length == 4).ToList();
            var words3 = wordBank.Where(data => data.Length == 3).ToList();
            var words2 = wordBank.Where(data => data.Length == 2).ToList();
            words4.Sort();
            words3.Sort();
            words2.Sort();
            var all = words4;
            all.AddRange(words3);
            all.AddRange(words2);

            var code = ""; // the current code we have
            var labels = label; // this will contain the 'leftovers' at the end, so store it locally in this scope

            foreach (var t in all)
            {
                var remainders = CanSpell(t, labels); // CanSpell will return the characters that weren't used to spell a word. if the word can't be spelled, it will return what was given as the 2nd argument
                if (remainders != labels) // if the remainders have changed
                {
                    labels = remainders; // update them
                    code += t.ToUpper(); // append this word to the code
                }
                if (code.Length >= 3) // if we're 3 or more characters, there's no more room for another word
                    break;
            }

            // finally, sort the remainders by putting them into a list and taking them back out
            // ReSharper disable once InconsistentNaming
            var Remainders = labels.Select((t, i) => labels.Substring(i, 1)).ToList();
            Remainders.Sort();
            labels = Remainders.Aggregate("", (current, t) => current + t);
            return code + labels;
        }
    }
}