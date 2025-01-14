﻿using System;
using System.Diagnostics;

namespace ProcessMethodsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Пример запуска процесса";
            //Объявляем объект класса Process
            Process proc = new Process();
            //устанавливаем имя файла, который будет запущен в рамках процесса
            proc.StartInfo.FileName = "notepad.exe";

            try // блок використовується на випадок відсутності додатку з вказаним ім'ям (в т.ч. помилка оператора)
            {
                //запускаем проуцесс
                proc.Start();
                //выводим имя процесса
                Console.WriteLine("Запущен процесс: " + proc.ProcessName);
                
                //ожидаем закрытия процесса
                proc.WaitForExit();
                //выводим код, с которым завершился процесс
                Console.WriteLine("Процесс завершился с кодом: " + proc.ExitCode);
                //выводим имя текущего процесса
                Console.WriteLine("Текущий процесс имеет имя: " + Process.GetCurrentProcess().ProcessName);
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Ошибка {Ex.Message}");
            }
            finally
            {
                proc.Dispose();
            }
        }
    }
}
