using System;
using System.Collections.Generic;
using System.Text;

namespace State
{
    public interface ICopierState
    {
        ICopierState Handle(Copier context);
    }

    public class HelloState : ICopierState
    {
        public ICopierState Handle(Copier context)
        {            
            Console.WriteLine("**************************");
            Console.WriteLine("*  Копировальная машина  *");
            Console.WriteLine("* Цена одной копии: " + Copier.priceCopy + "р. *");
            Console.WriteLine("**************************");
            return context["giveCash"];
        }
    }

    public class GiveCashState : ICopierState
    {        
        public ICopierState Handle(Copier context)
        {
            Console.WriteLine("Внесено: " + context.Cash);
            Console.Write("Внесите деньги:");
            int money = int.Parse(Console.ReadLine());
            context.Cash += money;
            return context["checkCash"];
        }
    }

    public class CheckCashState : ICopierState
    {
        public ICopierState Handle(Copier context)
        {
            if (context.Cash < Copier.priceCopy)
            {
                Console.WriteLine("Недостаточная сумма.");
                return context["giveCash"];
            }
            else
            {
                Console.WriteLine("Сумма достаточная...");
                return context["selectSource"];
            }

        }
    }

    public class SelectSourceState : ICopierState
    {
        public ICopierState Handle(Copier context)
        {
            Console.WriteLine("Выберите источник: ");
            var source = Menu.Run(new string[] { "Бумага", "Флеш накопитель", "Карта памяти" });
            if (source == 0)
            {
                context.Document = null;
                return context["makeCopy"];
            }
            else
                return context["selectDocument"];
        }
    }

    public class SelectDocumentState : ICopierState
    {
        public ICopierState Handle(Copier context)
        {
            string filename = "";
            while (String.IsNullOrWhiteSpace(filename))
            {
                Console.Write("Введите имя файла: ");
                filename = Console.ReadLine();
            }
            context.Document = filename;
            return context["makeCopy"];
        }
    }

    public class MakeCopyState : ICopierState
    {
        public ICopierState Handle(Copier context)
        {
            if (context.Document == null)
                Console.WriteLine("Делается копия...");
            else
                Console.WriteLine("Печатается файл: " + context.Document);
            context.Cash -= Copier.priceCopy;
            Console.WriteLine("Заберите документ.");
            return context["needRepeat"];
        }
    }

    public class NeedRepeatState : ICopierState
    {
        public ICopierState Handle(Copier context)
        {
            Console.WriteLine(String.Format("Остаток: {0} р.", context.Cash));
            Console.WriteLine("Завершить работу?");
            var result = Menu.Run(new string[] { "Да", "Нет" });
            if (result == 0)
                return context["retrieveCash"];
            else
                return context["checkCash"];
        }
    }

    public class RetrieveCashState : ICopierState
    {
        public ICopierState Handle(Copier context)
        {
            Console.WriteLine(String.Format("Заберите сдачу {0} р.", context.Cash));
            context.Cash = 0;
            context.Document = null;
            return context["hello"];
        }
    }

    static class Menu
    {
        public static int Run(string[] items)
        {
            var item = 100;
            while (item >= items.Length || item < 0)
            {
                for(int i = 0; i < items.Length; i++)
                    Console.WriteLine("  " + (i + 1) + " " + items[i]);            
                Console.Write(">");
                item = int.Parse(Console.ReadLine()) - 1;
            }
            return item;
        }
    }
}
