﻿namespace it.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    internal sealed class ConvertActions : ActionBase
    {
        private readonly Regex unitRegex =
        new Regex("(?<number>^[0-9]+([.,][0-9]+)?)(\\s*)(?<from>[a-z]+[2-3]?) (to|naar) (?<to>[a-z]+[2-3]?)", RegexOptions.Compiled);


        NameValueCollection currencies = new NameValueCollection()
        {
            { "usd", "usd" },
            { "unites states dollar", "usd" },
            { "eur", "eur" },
            { "afn", "afn" },
            { "afghani", "afn" }
        };



        public override bool Matches(string clipboardText = null)
        {
            if (clipboardText is null)
            {
                throw new System.ArgumentNullException(nameof(clipboardText));
            }

            Match matches = unitRegex.Match(clipboardText);
            return matches.Success;
        }

        public override ActionResult TryExecute(string clipboardText)
        {
            Match matches = unitRegex.Match(clipboardText);
            ActionResult actionResult = new ActionResult();
            double number = double.Parse(matches.Groups["number"].Value);
            string from = matches.Groups["from"].Value;
            string to = matches.Groups["to"].Value;

            // we should place the conversion of currency here
            string fromCurrency = currencies[from];
            string toCurrency = currencies[to];
            // we have a currency, make a call and get the result.
            return GetCurrencyActionResult(clipboardText, fromCurrency, toCurrency, (decimal)number);


            double meter = 0, gram = 0, liter = 0, oppervlakte = 0, snelheid = 0;
            switch (from)
            {
                case "mm":
                case "millimeter":
                    {
                        meter = number / 1000;
                        break;
                    }
                case "cm":
                case "centimer":
                    {
                        meter = number / 100;
                        break;
                    }
                case "dm":
                case "decimeter":
                    {
                        meter = number / 10;
                        break;
                    }
                case "m":
                case "meter":
                    {
                        meter = number;
                        break;
                    }
                case "dam":
                case "decameter":
                    {
                        meter = number * 1;
                        break;
                    }
                case "hm":
                case "hectometer":
                    {
                        meter = number * 100;
                        break;
                    }
                case "km":
                case "kilometer":
                    {
                        meter = number * 1000;
                        break;
                    }
                case "feet":
                case "ft":
                    {
                        meter = number * 0.3048;
                        break;
                    }
                case "inch":
                    {
                        meter = number * 0.0254;
                        break;
                    }
                case "mile":
                case "miles":
                    {
                        meter = number / 0.00062137;
                        break;
                    }
                case "yard":
                case "yd":
                    {
                        meter = number * 0.9144;
                        break;
                    }                    // gewicht eenheden
                case "mg":
                case "milligram":
                    {
                        gram = number / 1000;
                        break;
                    }
                case "cg":
                case "centigram":
                    {
                        gram = number / 100;
                        break;
                    }
                case "dg":
                case "decigram":
                    {
                        gram = number / 10;
                        break;
                    }
                case "gr":
                case "gram":
                    {
                        gram = number;
                        break;
                    }
                case "dag":
                case "decagram":
                    {
                        gram = number * 10;
                        break;
                    }
                case "hg":
                case "hectogram":
                    {
                        gram = number * 100;
                        break;
                    }
                case "kg":
                case "kilogram":
                    {
                        gram = number * 1000;
                        break;
                    }
                case "ml":
                case "milliliter":
                    {
                        liter = number / 1000;
                        break;
                    }
                case "cl":
                case "centiliter":
                    {
                        liter = number / 100;
                        break;
                    }
                case "dl":
                case "deciliter":
                    {
                        liter = number / 10;
                        break;
                    }
                case "l":
                case "liter":
                    {
                        liter = number;
                        break;
                    }
                case "dal":
                case "decaliter":
                    {
                        liter = number * 10;
                        break;
                    }
                case "hl":
                case "hectoliter":
                    {
                        liter = number * 100;
                        break;
                    }
                case "kl":
                case "kiloliter":
                    {
                        liter = number * 1000;
                        break;
                    }                    // oppervlakte eenheden
                case "mm2":
                    {
                        oppervlakte = number / 1000000;
                        break;
                    }
                case "cm2":
                    {
                        oppervlakte = number / 10000;
                        break;
                    }
                case "dm2":
                    {
                        oppervlakte = number / 100;
                        break;
                    }
                case "m2":
                    {
                        oppervlakte = number;
                        break;
                    }
                case "dam2":
                    {
                        oppervlakte = number * 100;
                        break;
                    }
                case "hm2":
                    {
                        oppervlakte = number * 10000;
                        break;
                    }
                case "km2":
                    {
                        oppervlakte = number * 1000000;
                        break;
                    }
                case "kmh":
                    {
                        snelheid = number;
                        break;
                    }
                case "ms":
                    {
                        snelheid = number * 3.6;
                        break;
                    }
            }

            // oppervlakte eenheden (area units)
            double result = 0;
            switch (to) // naar (to)
            {
                // lengte eenheden
                case "mm":
                case "millimeter":
                    {
                        result = meter * 1000;
                        break;
                    }
                case "cm":
                case "centimer":
                    {
                        result = meter * 100;
                        break;
                    }
                case "dm":
                case "decimeter":
                    {
                        result = meter * 10;
                        break;
                    }
                case "m":
                case "meter":
                    {
                        result = meter;
                        break;
                    }
                case "dam":
                case "decameter":
                    {
                        result = meter / 1;
                        break;
                    }
                case "hm":
                case "hectometer":
                    {
                        result = meter / 100;
                        break;
                    }
                case "km":
                case "kilometer":
                    {
                        result = meter / 1000;
                        break;
                    }
                case "feet":
                case "ft":
                    {
                        result = meter / 0.3048;
                        break;
                    }
                case "inch":
                    {
                        result = meter / 0.0254;
                        break;
                    }
                case "mile":
                case "miles":
                    {
                        result = meter * 0.00062137;
                        break;
                    }
                case "yard":
                case "yd":
                    {
                        result = meter * 0.9144;
                        break;
                    }                    // gewicht eenheden (Weight Units)
                case "mg":
                case "milligram":
                    {
                        result = gram * 1000;
                        break;
                    }
                case "cg":
                case "centigram":
                    {
                        result = gram * 100;
                        break;
                    }
                case "dg":
                case "decigram":
                    {
                        result = gram * 10;
                        break;
                    }
                case "gr":
                case "gram":
                    {
                        result = gram;
                        break;
                    }
                case "dag":
                case "decagram":
                    {
                        result = gram / 10;
                        break;
                    }
                case "hg":
                case "hectogram":
                    {
                        result = gram / 100;
                        break;
                    }
                case "kg":
                case "kilogram":
                    {
                        result = gram / 1000;
                        break;
                    }                    // inhoud (volume units)
                case "ml":
                case "milliliter":
                    {
                        result = liter * 1000;
                        break;
                    }
                case "cl":
                case "centiliter":
                    {
                        result = liter * 100;
                        break;
                    }
                case "dl":
                case "deciliter":
                    {
                        result = liter * 10;
                        break;
                    }
                case "l":
                case "liter":
                    {
                        result = liter;
                        break;
                    }
                case "dal":
                case "decaliter":
                    {
                        result = liter / 10;
                        break;
                    }
                case "hl":
                case "hectoliter":
                    {
                        result = liter / 100;
                        break;
                    }
                case "kl":
                case "kiloliter":
                    {
                        result = liter / 1000;
                        break;
                    }                    // oppervlakte eenheden (Area Units)
                case "mm2":
                    {
                        result = oppervlakte * 1000000;
                        break;
                    }
                case "cm2":
                    {
                        result = oppervlakte * 10000;
                        break;
                    }
                case "dm2":
                    {
                        result = oppervlakte * 100;
                        break;
                    }
                case "m2":
                    {
                        result = oppervlakte;
                        break;
                    }
                case "dam2":
                    {
                        result = oppervlakte / 100;
                        break;
                    }
                case "hm2":
                    {
                        result = oppervlakte / 10000;
                        break;
                    }
                case "km2":
                    {
                        result = oppervlakte / 1000000;
                        break;
                    }
                case "kmh":
                    {
                        result = snelheid;
                        break;
                    }
                case "ms":
                    {
                        result = snelheid / 3.6;
                        break;
                    }
            }

            Clipboard.SetText(result.ToString(CultureInfo.CurrentCulture));
            actionResult.Title = clipboardText;
            actionResult.Description = result + to;

            return actionResult;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConvertActions);
        }



        #region Helpers

        internal class ExchangeRateModel
        {
            public Dictionary<string, decimal> rates { get; set; }
        }


        private ActionResult GetCurrencyActionResult(string clipboardText, string fromCurrency, string toCurrency, decimal amount)
        {
            ActionResult actionResult = new ActionResult();
            if (String.IsNullOrWhiteSpace(fromCurrency) | String.IsNullOrWhiteSpace(toCurrency)) return actionResult;

            using (WebClient client = new WebClient())
            {

                try
                {
                    string json = client.DownloadString($"https://api.exchangeratesapi.io/latest?base={fromCurrency.ToUpper()}&symbols={toCurrency.ToUpper()}");
                    ExchangeRateModel deserializedJson = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeRateModel>(json);
                    decimal rate = deserializedJson.rates[toCurrency.ToUpper()];
                    actionResult.Description = $"{clipboardText} = {amount * rate:N2} {toCurrency}";
                }
                catch (Exception ex)
                {
                    actionResult.Description = $"There was an error getting the exchange rate: {ex.Message}";
                }


            }

            return actionResult;
        }
        #endregion Helpers

    }
}