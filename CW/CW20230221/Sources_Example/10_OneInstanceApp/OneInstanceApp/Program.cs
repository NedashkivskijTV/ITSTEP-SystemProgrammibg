using System;
using System.Threading;

namespace OneInstanceApp
{
    class OneInstanceAppClass
    {
        static Mutex m;

        static void Main(string[] args)
        {
            if (Mutex.TryOpenExisting("MY_MUTEX", out m))
            {
                Console.WriteLine("Приложение уже запущено!");
                Console.ReadKey();
                return;
            }

            using (m = new Mutex(true, "MY_MUTEX"))
            {
                Console.WriteLine("Приложение работает.\n" +
                                  "Нажмите любую клавишу для закрытия приложения...");
                Console.ReadKey();
            }
        }
    }
}
