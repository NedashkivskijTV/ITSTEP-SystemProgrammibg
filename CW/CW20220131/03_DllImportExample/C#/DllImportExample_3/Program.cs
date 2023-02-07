using System;
using System.Runtime.InteropServices;
using static System.Console;

namespace SimpleProject
{
    public class DllImportExample
    {
        [DllImport("SimpleCalc_3.dll")]
        public static extern int add(int a, int b);
        [DllImport("SimpleCalc_3.dll")]
        public static extern int sub(int a, int b);
        [DllImport("SimpleCalc_3.dll")]
        public static extern int mult(int a, int b);
        [DllImport("SimpleCalc_3.dll")]
        public static extern int div(int a, int b);
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Write("Enter the frst number: ");
                int number1 = int.Parse(ReadLine());
                Write("Enter the second number: ");
                int number2 = int.Parse(ReadLine());
                WriteLine($"\t{number1} + {number2} = {DllImportExample.add(number1, number2)}");
                WriteLine($"\t{number1} - {number2} = {DllImportExample.sub(number1, number2)}");
                WriteLine($"\t{number1} * {number2} = {DllImportExample.mult(number1, number2)}");
                WriteLine($"\t{number1} / {number2} = {DllImportExample.div(number1, number2)}");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            Read();
        }
    }
}
