using System;
using System.Collections.Generic;
using System.Text;

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
        public static Dictionary<CurrencyType, int[]> config = new Dictionary<CurrencyType, int[]> {
            {CurrencyType.RUB, new int[] {5000, 1000, 500, 100}},
            {CurrencyType.USD, new int[] {100, 50, 20, 10}},
            {CurrencyType.GBP, new int[] {50, 20, 10, 5}},
            {CurrencyType.EUR, new int[] {500, 200, 100, 50, 20, 10, 5}},
            {CurrencyType.JPY, new int[] {10000, 5000, 2000, 1000}},
            {CurrencyType.CNY, new int[] {100, 50, 20, 10, 5}},
            {CurrencyType.CHF, new int[] {1000, 200, 100, 50, 20, 10}},
            {CurrencyType.AUD, new int[] {100, 50, 20, 10, 5}},
            {CurrencyType.CAD, new int[] {100, 50, 20, 10, 5}}
        };

        public static TypeHandler FirstHandler = new TypeHandler();
        private static ICurrencyHandler finalHandler = new InvalidHandler();
        private static Dictionary<int, ICurrencyHandler> amountHandlers = new Dictionary<int, ICurrencyHandler>();

        static CurrencyFactory()
        {
            foreach (var amounts in config.Values)
                foreach (var amount in amounts)
                    if (!amountHandlers.ContainsKey(amount))
                        amountHandlers.Add(amount, new DenominationHandler(amount));
        }

        public static int next(Money money, Stack<string> stack, int position)
        {
            if (money.amount == 0)
                return 0;
            if (money.type == CurrencyType.Invalid || money.amount < 0 || position < 0)
                return finalHandler.validate(money, stack, 0);
            var amounts = config[money.type];
            while (position < amounts.Length && amounts[position] > money.amount)
                position++;
            return position == amounts.Length ? finalHandler.validate(money, stack, 0) :
                amountHandlers[amounts[position]].validate(money, stack, position);
        }
    }
}
