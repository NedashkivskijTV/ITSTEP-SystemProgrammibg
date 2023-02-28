namespace TaskSamples
{
    class Program
    {
        static object taskMethodLock = new object();

        static void Main()
        {
            ////1 Задачи, использующие пул потоков            
            //TasksUsingThreadPool();

            ////2 Запускается синхронно, в том же самом потоке, в котором она вызвана 
            //RunSynchronousTask();

            ////3 Задачи, использующие отдельный поток не из пула
            //LongRunningTask();

            ////4 Получение результатов из задач
            //ResultsFromTasks();

            ////5 Задачи продолжения
            //ContinuationTask();

            //6 Иерархии задач
            ParentAndChild();

            ////7 Отмена задачи 
            //CancelTask();

            Console.ReadLine();
        }


        //6 Иерархии задач
        static void ParentAndChild()
        {
            var parent = new Task(ParentTask); //Создание родительской задачи
            parent.Start();
            Thread.Sleep(2000);
            Console.WriteLine($"\nparent status {parent.Status}");
            Thread.Sleep(4000);
            Console.WriteLine($"\nparent status {parent.Status}");

            ////Var2
            //Thread.Sleep(2000);
            //Console.WriteLine($"\nparent status {parent.Status}");
            ////Var2

            Console.WriteLine($"\nfinish!!!");
        }

        //Родительская задача
        static void ParentTask()
        {
            lock (taskMethodLock)
            {
                Console.WriteLine("parent");
                Console.WriteLine($"task id {Task.CurrentId}");
                Console.WriteLine($"thread: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
                Console.WriteLine($"is background thread: {Thread.CurrentThread.IsBackground}");

                var child = new Task(ChildTask, TaskCreationOptions.AttachedToParent); //Создание дочерней задачи
                child.Start();

                ////Var2
                //Thread.Sleep(2000);
                ////Var2

                Console.WriteLine("parent started child");
            }

            Thread.Sleep(3000);

        }

        //Дочерняя задача
        static void ChildTask()
        {
            lock (taskMethodLock)
            {
                Console.WriteLine("\n\nchild");
                Console.WriteLine($"task id {Task.CurrentId}");
                Console.WriteLine($"thread: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
                Console.WriteLine($"is background thread: {Thread.CurrentThread.IsBackground}");
            }
            Thread.Sleep(5000);
            Console.WriteLine("child finished");
        }

    }
}