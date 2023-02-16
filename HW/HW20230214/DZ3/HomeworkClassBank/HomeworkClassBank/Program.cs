using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeworkClassBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.InputEncoding = System.Text.Encoding.Unicode;
            //Console.OutputEncoding = System.Text.Encoding.Unicode;

            Console.WriteLine("\nWelcome to the Banking Information Saving Program\n");

            Bank bank = new Bank();
            //Bank bankOfAmerica = new Bank(1000000, "Bank Of America", 1);

            bank.SetName("MyBank");
            
            bank.SetMoney(7000);
            
            bank.SetPercent(10);

            bank.SetName("Bank Of America");

            bank.SetName("New Fort Knox Bank");

            bank.SetMoney(7000000);

            bank.SetPercent(1);

            Console.WriteLine();
            Console.WriteLine(bank);
            //Console.WriteLine(bankOfAmerica);
        }
    }
}
