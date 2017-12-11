using System;
using System.Collections.Generic;
using System.Linq;

namespace KTaNE_Helper
{
    public class Cryptography
    {
        private readonly List<string> _christmasCarol = new List<string>
        {
            "scrooge knew he was dead",
            "of course he did",
            "how could it be otherwise",
            "scrooge and he were partners for i dont know how many years",
            "scrooge was his sole executor his sole administrator his sole assign his sole residuary legatee his sole friend and sole mourner",
            "and even scrooge was not so dreadfully cut up by the sad event but he was an excellent man of business on the very day of the funeral and solemnised it with an undoubted bargain",
            "the mention of marleys funeral brings me back to the point i started from",
            "there is no doubt that marley was dead",
            "this must be disticnctly understood or nothing wonderful can come of the story i am going to relate",
            "if we were not perfectly convinced that hamlets father died before the play began there would be nothing more remarkable in his taking a stroll at night in an easterly wind upon his own ramparts than there would be in any other middleaged gentleman rashly turning out after dark in a breezy spot say saint pauls churchyard for instance literally to astonish his sons weak mind",
            "scrooge never painted out old marleys name",
            "there it stood years afterwards above the warehouse door scrooge and marley",
            "the firm was known as scrooge and marley",
            "sometimes people new to the business called scrooge scrooge and sometimes marley but he answered to both names",
            "it was all the same to him",
            "oh",
            "but he was a tightfisted hand at the grindstone scrooge",
            "a squeezing wrenching grasping scraping clutching covetous old sinner",
            "hard and sharp as flint from which no steel had ever struck out generous fire",
            "secret and selfcontained and solitary as an oyster",
            "the cold within him froze his old features nipped his pointed nose shrivelled his cheek stiffened his gait",
            "made his eyes red his thin lips blue and spoke out shrewdly in his grating voice",
            "a frosty rime was on his head and on his eyebrows and his wiry chin",
            "he carried his own low temperature always about with him",
            "he iced his office in the dogdays",
            "and didnt thaw it one degree at christmas",
            "external heat and cold had little influence on scrooge",
            "no warmth could warm no wintry weather chill him",
            "no wind that blew was bitterer than he no falling snow was more intent upon its purpose no pelting rain less open to entreaty",
            "foul weather didnt know where to have him",
            "the heaviest rain and snow and hail and sleet could boast of the advantage over him in only one respect",
            "they often came down handsomely and scrooge never did",
            "nobody ever stopped him in the street to say with gladsome looks my dear scrooge how are you",
            "when will you come to see me",
            "no beggars implored him to bestow a trifle no children asked him what it was oclock no man or woman ever once in all his life inquired the way to such and such a place of scrooge",
            "even the blind mens dogs appeared to know him",
            "and when they saw him coming on would tug their owners into doorways and up courts",
            "and then would wag their tails as though they said no eye at all is better than an evil eye dark mister",
            "but what did scrooge care",
            "it was the very thing he liked",
            "to edge his way along the crowded paths of life warning all human sympathy to keep its distance was what the knowing ones call nuts to scrooge"
        };

        public string GetLetterOrder(string lengths, string letters)
        {
            var result = "";

            if (lengths.Length == 0) return result;
            if (letters.Length < 5) return result;
            var inputs = lengths.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var lens = new List<int>();
            foreach (var x in inputs)
            {
                int y;
                if (int.TryParse(x, out y))
                    lens.Add(y);
            }
            if (lens.Count == 0) return result;

            var matched = 0;
            try
            {
                foreach (var x in _christmasCarol)
                {
                    var offset = 1;
                    if (x.Split(' ').Length != lens[0]) offset = 0;

                    var flag = true;
                    var words = x.Split(' ');
                    for (var i = offset; i < lens.Count && (i - offset) < words.Length && flag; i++)
                        flag &= x.Split(' ')[i - offset].Length == lens[i];
                    if (!flag) continue;

                    var order = new List<int>();
                    var orderlist = new Dictionary<int, string>();
                    foreach (var y in letters.ToCharArray())
                    {
                        var z = x.IndexOf(y);
                        order.Add(z);
                        if (z < 0) continue;
                        orderlist.Add(z, y.ToString());
                        order.Sort();
                    }
                    if (order.Count != orderlist.Count) continue;

                    result = order.Aggregate(result, (current, y) => current + orderlist[y]);
                    matched++;
                }
            }
            catch
            {
                return "EXCEPTION: no match found";
            }

            if (matched == 0)
                return "No match found";
            return matched > 1 
                ? "Need letter counts of individual words" 
                : result.ToUpper();
        }
    }
}