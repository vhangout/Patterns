using System;
using System.Collections.Generic;
using System.Text;
using MailBuilder.Builders;

namespace MailBuilder
{
    class Program
    {
        static void nextTest(MailStateBuilder.IMailBuilder builder)
        {
            if (builder != null)
                Console.Write(builder.Build());
            Console.WriteLine("\nPress any key...\n");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Error for init without recepients");
            try
            {
                var invalidBuilder = MailStateBuilder.AddRecepient(null);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            nextTest(null);

            Console.WriteLine("Error for init without body");
            try
            {
                var invalidBuilder2 = MailStateBuilder.AddRecepient("Vasia,Sasha").AddBody("    ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            nextTest(null);


            Console.WriteLine("Init builder");
            var builder = MailStateBuilder.AddRecepient("Vasia,Sasha").AddBody("First line");
            nextTest(builder);

            Console.WriteLine("Add to recepient");
            builder.AddRecepient("Petia");
            nextTest(builder);

            Console.WriteLine("Removing Kolia from recepients");
            builder.RemoveRecepient("Kolia");
            nextTest(builder);

            Console.WriteLine("Removing Sasha from recepients");
            builder.RemoveRecepient("Sasha");            
            nextTest(builder);

            Console.WriteLine("Make recepients is empty");
            try
            {
                builder.RemoveRecepient("Petia, Vasia");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            nextTest(builder);

            Console.WriteLine("Make body is empty");
            try
            {
                builder.SetBody("  ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            nextTest(builder);

            Console.WriteLine("Add to body");
            builder.AddToBody("Second Line");
            nextTest(builder);

            Console.WriteLine("Set subject");
            builder.SetSubject("Mail subject");
            nextTest(builder);

            Console.WriteLine("Add to CC");
            builder.AddCopy("Kolya,Misha");
            nextTest(builder);

            Console.WriteLine("Remove from CC");
            builder.RemoveCopy("Kolya");
            nextTest(builder);

            Console.WriteLine("Final result");
            nextTest(builder);
        }
    }
}
