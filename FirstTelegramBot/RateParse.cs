using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text;

namespace FirstTelegramBot
{
    public class RateParse
    {
        private static readonly Dictionary<TypesOfCurrencies, string> dictionaryOfSites = new Dictionary<TypesOfCurrencies, string>()
        {
            { TypesOfCurrencies.Euro, "https://www.banki.ru/products/currency/eur/" },
            { TypesOfCurrencies.Dollar, "https://www.banki.ru/products/currency/usd/" }
        };

        private static readonly Dictionary<TypesOfCurrencies, string> dictionaryOfNodes = new Dictionary<TypesOfCurrencies, string>()
        {
            { TypesOfCurrencies.Euro, "//td[contains(@class, 'currency-table__rate currency-table__darken-bg')]" },
            { TypesOfCurrencies.Dollar, "//td[contains(@class, 'currency-table__rate currency-table__darken-bg')]" },
        };

        public static Task<string> GetRate(TypesOfCurrencies typeOfCurrency)
        {
            string rateString = string.Empty;

            HtmlWeb ws = new HtmlWeb
            {
                OverrideEncoding = Encoding.UTF8
            };

            HtmlDocument document = ws.Load(dictionaryOfSites[typeOfCurrency]);

            var collectionOfNodes = document.DocumentNode.SelectNodes(dictionaryOfNodes[typeOfCurrency]);

            foreach (var node in collectionOfNodes)
                rateString = Regex.Match(node.InnerText, @"[0-9][0-9]\,[0-9][0-9]").Value;

            return Task.FromResult(rateString);
        }
    }
}
