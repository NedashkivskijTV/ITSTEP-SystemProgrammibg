using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms; //Добавляется ссылка на библиотеку
using static System.Console;

namespace SimpleProject
{
    public class DllImportExample
    {
        //В качестве примера мы приведем использование функции SetForegroundWindow() 
        //из библиотеки User32. dll, которая активирует окно, заданное через дескриптор,
        //и переводит это окно в приоритетный режим.
        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(IntPtr point);
    }

    class Program
    {
        static void Main(string[] args)
        {
            string processName = "notepad.exe", text;
            Write("Enter text: ");
            text = ReadLine();
            
            //Запуск процесса "notepad.exe"
            Process p = Process.Start(processName);

            //Переводим этот процесс в состояние ожидания
            p.WaitForInputIdle();
            
            //получаем дескриптор окна запущенного процесса
            IntPtr h = p.MainWindowHandle;

            //Функция SetForegroundWindow активирует окно, заданное через дескриптор,
            //и переводит это окно в приоритетный режим
            DllImportExample.SetForegroundWindow(h);

            //отправляем сообщение о нажатых клавишах в консоль и notepad.exe
            SendKeys.SendWait(text);

            Write("Нажмите любую клавишу для заверщения...");
            Read();

            p.Kill();
            p.Close();
        }
    }
}