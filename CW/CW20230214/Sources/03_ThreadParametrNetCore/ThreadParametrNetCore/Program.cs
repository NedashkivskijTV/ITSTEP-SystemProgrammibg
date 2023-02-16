using System;
using System.Threading;

namespace ThreadParametrNetCore1
{
    class Program
    {
        static void Main(string[] args)
        {

            ////Var1 Передача объекта в качестве параметра
            //Counter1 counter1 = new Counter1();
            //counter1.x = 4;
            //counter1.y = 5;
            //Thread myThread1 = new Thread(new ParameterizedThreadStart(Count));
            //myThread1.Start(counter1);
            ////Var1 Передача объекта в качестве параметра

            //Var2 Вариант типобезопасной передачи параметров
            Counter2 counter2 = new Counter2(4, 5);
            Thread myThread2 = new Thread(new ThreadStart(counter2.Count));
            myThread2.Start();
            //Var2 Вариант типобезопасной передачи параметров

            //...................
        }

        //public static void Count(object obj)
        //{
        //    // Приведення до типу переданого об'єкта
        //    Counter1 c = (Counter1)obj;

        //    for (int i = 1; i <= 9; i++)
        //    {
        //        // Отримання потрібних даних з об'єкта
        //        Console.WriteLine($"Второй поток: {i * c.x * c.y}");
        //    }
        //}
    }

    ////Var1 Передача объекта в качестве параметра
    //public class Counter1
    //{
    //    public int x;
    //    public int y;
    //}
    ////Var1 Передача объекта в качестве параметра

    //Var2 Передача объекта в качестве параметра
    public class Counter2
    {
        private int x;
        private int y;

        public Counter2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public void Count()
        {
            for (int i = 1; i <= 9; i++)
            {
                Console.WriteLine($"Второй поток: {i * x * y}");
                Thread.Sleep(400);
            }
        }
    }
    //Var2 Передача объекта в качестве параметра
}

