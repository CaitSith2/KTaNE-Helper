using System.Linq;

namespace KTaNE_Helper.Modules.Modded
{
    public static class AdjacentLetters
    {
        private static string[] _leftRight = new[] {
            "GJMOY",
            "IKLRT",
            "BHIJW",
            "IKOPQ",
            "ACGIJ",
            "CERVY",
            "ACFNS",
            "LRTUX",
            "DLOWZ",
            "BQTUW",
            "AFPXY",
            "GKPTZ",
            "EILQT",
            "PQRSV",
            "HJLUZ",
            "DMNOX",
            "CEOPV",
            "AEGSU",
            "ABEKQ",
            "GVXYZ",
            "FMVXZ",
            "DHMNW",
            "DFHMN",
            "BDFKW",
            "BCHSU",
            "JNRSY"
        };
        private static string[] _aboveBelow = new[] {
            "HKPRW",
            "CDFYZ",
            "DEMTU",
            "CJTUW",
            "KSUWZ",
            "AGJPQ",
            "HOQYZ",
            "DKMPS",
            "EFNUV",
            "EHIOS",
            "DIORZ",
            "ABRVX",
            "BFPWX",
            "AFGHL",
            "IQSTX",
            "CFHKR",
            "BDIKN",
            "BNOXY",
            "GMVYZ",
            "CJLSU",
            "BILNY",
            "AEJQX",
            "GLQRT",
            "AJNOV",
            "EGMTW",
            "CLMPV"
        };

        public static string GetAnswer(string letters)
        {
            var availableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToList();
            if (letters.Length < 12) return "";
            var answer = "set ";
            for (var i = 0; i < 12; i++)
            {
                if (!availableLetters.Contains(letters[i]))
                    return "";
                availableLetters.Remove(letters[i]);

                var x = i % 4;
                var y = i / 4;
                if ((x > 0 && _leftRight[letters[i] - 'A'].Contains(letters[i - 1].ToString()) ||
                     (x < 3 && _leftRight[letters[i] - 'A'].Contains(letters[i + 1].ToString()))) ||
                    (y > 0 && _aboveBelow[letters[i] - 'A'].Contains(letters[i - 4].ToString()) ||
                     (y < 2 && _aboveBelow[letters[i] - 'A'].Contains(letters[i + 4].ToString()))))
                answer += letters[i];
                if (i % 4 == 3) answer += " ";
            }
            return answer;
        }
    }
}