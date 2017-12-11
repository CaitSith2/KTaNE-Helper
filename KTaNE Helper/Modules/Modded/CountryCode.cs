using static System.String;

namespace KTaNE_Helper
{
    internal struct CountryCode
    {
        public string Code;
        public string ISO4217;

        public CountryCode(string code, string iso4217)
        {
            Code = code;
            ISO4217 = iso4217;
        }
    }

    public static class CountryCodes
    {
        private static readonly CountryCode[] _countryCodes = {
            new CountryCode("AUD", "036"), new CountryCode("BGN", "975"), new CountryCode("BRL", "986"),
            new CountryCode("CAD", "124"), new CountryCode("CHF", "756"), new CountryCode("CNY", "156"),
            new CountryCode("DKK", "208"), new CountryCode("EUR", "978"), new CountryCode("GBP", "826"),
            new CountryCode("HKD", "344"), new CountryCode("HRK", "191"), new CountryCode("HUF", "348"),
            new CountryCode("IDR", "360"), new CountryCode("ILS", "376"), new CountryCode("INR", "356"),
            new CountryCode("JPY", "392"),
            new CountryCode("KRW", "410"), new CountryCode("MXN", "484"), new CountryCode("MYR", "458"),
            new CountryCode("NOK", "578"), new CountryCode("NZD", "554"), new CountryCode("PHP", "608"),
            new CountryCode("PLN", "985"), new CountryCode("RON", "946"), new CountryCode("RUB", "643"),
            new CountryCode("SEK", "752"), new CountryCode("SGD", "702"), new CountryCode("THB", "764"),
            new CountryCode("TRY", "949"), new CountryCode("USD", "840"), new CountryCode("ZAR", "710")
        };

        public static string GetISO4217NumericCode(string code)
        {
            foreach (var c in _countryCodes)
            {
                if (c.Code == code)
                    return c.ISO4217;
            }
            return Empty;
        }

        public static string GetISO4217CountryCode(string num)
        {
            foreach (var c in _countryCodes)
            {
                if (c.ISO4217 == num)
                    return c.Code;
            }
            return Empty;
        }

        public static string GetConversionURL(string baseCode, string targetCode)
        {
            return "http://api.fixer.io/latest?base=" + baseCode + "&symbols=" + targetCode;
        }
    }
}