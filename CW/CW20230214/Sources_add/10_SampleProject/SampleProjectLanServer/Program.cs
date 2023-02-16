using System;
using System.Threading;

// Приложение не является сетевым, оно только симулирует работу сети.
namespace SampleProjectLanServer
{
    class Program
    {
        // Основной метод программы.
        static void Main(string[] args)
        {
            // Создаем и запускаем "слушатель".
            Thread LisenerThread = new Thread(LisenerClient)
            {
                IsBackground = false
            };
            LisenerThread.Start();
        }

        // Метод который будет выполнятся в отдельном потоке
        // и будет ждать подключений от пользователей.
        static void LisenerClient()
        {
            int Counter = 0;
            while (true)
            {

                // Нажатием кнопки пользователь, симулирует сетевое подключения пользователя
                // к серверу.
                Console.WriteLine("Нажмите любую клавишу для симуляции подключения пользователя");
                Console.ReadKey(true);

                // Создание объекта потока (Для каждого клиента)
                Thread UserWorkThread = new Thread(UserThreadFunk);

                // Запуск потока.
                UserWorkThread.Start(Counter.ToString());
                Counter++;
            }
        }

        // Метод будет выполнятся в отдельном потоке,
        // для каждого подключенного клиента 
        static void UserThreadFunk(object a)
        {
            string UserName = (string)a;
            Console.WriteLine($"Пользователь\t#{UserName} подключился");
            while (true)
            {
                // Ожидание команды пользователя.
                switch (GetUserCommand())
                {
                    case 0:
                        Console.WriteLine($"Пользователь\t#{UserName} подписался на новости");
                        break;
                    case 1:
                        Console.WriteLine($"Пользователь\t#{UserName} начал чат");
                        break;
                    case 2:
                        Console.WriteLine($"Пользователь\t#{UserName} купил продукцию в магазине");
                        break;
                    case 3:
                        Console.WriteLine($"Пользователь\t#{UserName} отправил письмо");
                        break;
                    case 4:
                        Console.WriteLine($"Пользователь\t#{UserName} отключился");
                        return; // Завершение потока
                }
            }
        }


        // Метод имитирующий работу пользователя
        static int GetUserCommand()
        {
            Random Rand = new Random();
            while (true)
            {
                Thread.Sleep(100);
                int Gener = Rand.Next(0, 50);

                switch (Gener)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 1;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                    case 4:
                        return 4;
                }
            }
        }
    }
}
