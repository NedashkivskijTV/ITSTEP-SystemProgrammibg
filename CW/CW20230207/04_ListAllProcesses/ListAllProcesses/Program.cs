using System;
using System.Diagnostics;
using System.Linq;

namespace ListAllProcesses
{
    class Program
    {
        static void Main(string[] args)
        {
            //устанавливаем заголовок коннсоли
            Console.Title = "Список процессов";
            //изменяем размер буфера консоли и окна на необходимый нам
            Console.WindowWidth = 100;
            Console.BufferWidth = 100;


            //получаем список процессов
            Process[] processes = Process.GetProcesses();
            //выводим заголовок
            Console.WriteLine("  {0,-88}{1,-10}","Имя процесса:","PID:");
            //для каждого процесса выводим имя и PID
            foreach (Process p in processes.OrderBy(p => p.ProcessName))
                Console.Write("  {0,-88}{1,-10}",p.ProcessName, p.Id);
        }
    }
}
