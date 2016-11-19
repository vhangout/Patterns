using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ChainOfResponsibility
{
    public enum CurrencyType { Invalid = -1, RUB, USD, GBP, EUR, JPY, CNY, CHF, AUD, CAD}

    public struct Money
    {
        public CurrencyType type;
        public int amount;

        public Money(CurrencyType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
    }

    static class CurrencyFactory
    {
        public static readonly Dictionary<CurrencyType, List<int>> config = new Dictionary<CurrencyType, List<int>> {
            {CurrencyType.RUB, new List<int> {5000, 1000, 500, 100}},
            {CurrencyType.USD, new List<int> {100, 50, 20, 10}},
            {CurrencyType.GBP, new List<int> {50, 20, 10, 5}},
            {CurrencyType.EUR, new List<int> {500, 200, 100, 50, 20, 10, 5}},
            {CurrencyType.JPY, new List<int> {10000, 5000, 2000, 1000}},
            {CurrencyType.CNY, new List<int> {100, 50, 20, 10, 5}},
            {CurrencyType.CHF, new List<int> {1000, 200, 100, 50, 20, 10}},
            {CurrencyType.AUD, new List<int> {100, 50, 20, 10, 5}},
            {CurrencyType.CAD, new List<int> {100, 50, 20, 10, 5}}
        };

        private static Dictionary<int, DenominationHandler> amountHandlers = config.Values.SelectMany(x => x)
            .Distinct().Select(x => new DenominationHandler(x))
            .ToDictionary(x => x.Amount);

        public static int next(Money money, Stack<string> stack)
        {
            if (money.amount == 0)
                return new FinalHandler().validate(money, stack);

            var amounts = CurrencyType.Invalid.Equals(money.type) ? null : config[money.type];

            if (amounts == null || money.amount < 0 || amounts.All(a => a > money.amount))
                return new InvalidHandler().validate(money, stack);
            else
                return amountHandlers[amounts.Where(a => a <= money.amount).Max()].validate(money, stack);
        }
    }
}
