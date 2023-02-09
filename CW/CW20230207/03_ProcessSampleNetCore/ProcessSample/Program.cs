using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Приклад запуску процесу";
        //Объявляем объект класса Process
        Process proc = new Process();
        //устанавливаем имя файла, который будет запущен в рамках процесса
        proc.StartInfo.FileName = "notepad.exe";

        try
        {
            //запускаем процесс
            proc.Start();
            //выводим имя процесса
            Console.WriteLine("Запущено процес: " + proc.ProcessName);

            //ожидаем закрытия процесса
            proc.WaitForExit();
            //выводим код, с которым завершился процесс
            Console.WriteLine("Процес завершено з кодом: " + proc.ExitCode);
            //выводим имя текущего процесса
            Console.WriteLine("Поточний процес має ім'я: " + Process.GetCurrentProcess().ProcessName);
        }
        catch (Exception Ex)
        {
            Console.WriteLine($"Помилка {Ex.Message}");
        }

        finally
        {
            proc.Dispose();
        }
    }
}