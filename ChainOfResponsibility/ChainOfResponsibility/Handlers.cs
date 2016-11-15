using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    interface ICurrencyHandler
    {
        int validate(Money money, Stack<string> stack, int position);
    }

    class TypeHandler : ICurrencyHandler
    {
        public int validate(String input, Stack<string> stack)
        {
            var type = CurrencyType.Invalid;
            foreach (CurrencyType _type in Enum.GetValues(typeof(CurrencyType)))
            {
                if (input.ToUpper().Contains(_type.ToString("G")))
                {
                    type = _type;
                    input = input.ToUpper().Replace(_type.ToString("G"), "");
                    break;
                }
            }
            var amount = -1;
            try {amount = int.Parse(input);} catch {}
            return this.validate(new Money(type, amount), stack, -1);
        }

        public int validate(Money money, Stack<string> stack, int position)
        {
            stack.Push(money.type.ToString("G"));
            return CurrencyFactory.next(money, stack, ++position);
        }
    }

    class DenominationHandler : ICurrencyHandler
    {
        public int Amount {get; private set;}
        
        public DenominationHandler(int amount)
        {
            Amount = amount;
        }

        public int validate(Money money, Stack<string> stack, int position)
        {
            var count = money.amount / Amount;
            stack.Push(count + "x" + Amount);
            return CurrencyFactory.next(new Money(money.type, money.amount - count * Amount), stack, ++position);
        }
    }

    class InvalidHandler : ICurrencyHandler
    {
        public int validate(Money money, Stack<string> stack, int position)
        {
            return -1;
        }
    }
}
