namespace TaskSamples
{
    class Program
    {
        static object taskMethodLock = new object();

        static void Main()
        {
            //1 Задачи, использующие пул потоков            
            TasksUsingThreadPool();

            ////2 Запускается синхронно, в том же самом потоке, в котором она вызвана 
            //RunSynchronousTask();

            ////3 Задачи, использующие отдельный поток не из пула
            //LongRunningTask();

            ////4 Получение результатов из задач
            //ResultsFromTasks();

            ////5 Задачи продолжения
            //ContinuationTask();

            ////6 Иерархии задач
            //ParentAndChild();

            ////7 Отмена задачи 
            //CancelTask();

            Console.ReadLine();
        }


        //1 Задачи, использующие пул потоков            
        static void TasksUsingThreadPool()
        {
            TaskMethod("just the main thread");

            //1 подход использование экземпляра класса TaskFactory
            var tf = new TaskFactory();
            Task t1 = tf.StartNew(TaskMethod, "using a task factory");

            //2 подход применении статического свойства Factory класса Task 
            //для получения доступа к TaskFactory и вызов метода StartNew()	    
            Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");

            //3 подход связан с применением конструктора класса Task.	    
            var t3 = new Task(TaskMethod, "using a task constructor and Start");
            t3.Start();

            //4 подход появившийся в .NET 4.5, предполагает вызов метода Run() класса Task, 
            //который немедленно запускает задачу	    
            Task t4 = Task.Run(() => TaskMethod("using the Run method"));
        }


        //2 Запускается синхронно, в том же самом потоке, в котором она вызвана 
        private static void RunSynchronousTask()
        {
            TaskMethod("just the main thread");
            var t1 = new Task(TaskMethod, "run sync");
            t1.RunSynchronously();
        }


        //3 Задачи, использующие отдельный поток не из пула
        private static void LongRunningTask()
        {
            TaskMethod("just the main thread"); 
            var t1 = new Task(TaskMethod, "long running", TaskCreationOptions.LongRunning);
            t1.Start();
        }


        static void TaskMethod(object? title)
        {
            lock (taskMethodLock)
            {
                Console.WriteLine(title);
                Console.WriteLine("Task id: {0}, thread: {1}",
                  Task.CurrentId == null ? "no task" : Task.CurrentId.ToString(),
                  Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine($"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}");
                Console.WriteLine($"is background thread: {Thread.CurrentThread.IsBackground}");
                Console.WriteLine();
            }
        }


        //4 Получение результатов из задач
        static void ResultsFromTasks()
        {
            //1
            var t1 = new Task<Tuple<int, int>?>(TaskWithResult, Tuple.Create(9, 4)); //Кортежи
            
            //// Ошибка
            //var t1 = new Task<Tuple<int, int>?>(TaskWithResult, null); //Кортежи
            
            t1.Start();
            //Свойство Result экземпляра Task по имени tl блокируется и ожидает до тех пор, пока задача не
            //будет завершена. После завершения задачи свойство Result содержит результат выполнения задачи
            t1.Wait();
            Console.WriteLine(t1.Result);
            //Console.WriteLine($"result from task: {t1.Result.Item1} {t1.Result.Item2}");
            //1

            ////2
            //Task[] t2 = new Task[3]
            //    {
            //        new Task(() => { Thread.Sleep(3000); Console.WriteLine("First Task"); }),
            //        new Task(() => { Thread.Sleep(1000); Console.WriteLine("Second Task"); }),
            //        new Task(() => { Thread.Sleep(2000); Console.WriteLine("Third Task"); })
            //    };
            //foreach (var t in t2)
            //    t.Start();
            //Task.WaitAll(t2); // ожидаем завершения всех задач 

            ////Task.WaitAny(t2); // ожидаем завершения  одной из задач

            //Console.WriteLine("Завершение метода ResultsFromTasks");
            ////2

            Console.ReadLine();
        }

        static Tuple<int, int> TaskWithResult(object? division)  //Кортежи
        {
            if (division != null)
            {
                Tuple<int, int> div = (Tuple<int, int>)division;
                int result = div.Item1 / div.Item2;
                int reminder = div.Item1 % div.Item2;
                Console.WriteLine("task creates a result...");
                Thread.Sleep(1000);
                return Tuple.Create(result, reminder);
            }
            else
                return Tuple.Create(0, 0);
        }


        //5 Задачи продолжения
        static void ContinuationTask()
        {
            Task t1 = new Task(DoOnFirst);
            //Конструкция t1.OnContinueWith(DoOnSecond) означает, что новая задача, вызывающая
            //метод DoOnSecond(), должна быть запущена, когда завершится задача tl
            Task t2 = t1.ContinueWith(DoOnSecond, TaskContinuationOptions.NotOnFaulted);
            Task t3 = t1.ContinueWith(DoOnSecond, TaskContinuationOptions.NotOnFaulted);
            Task t4 = t2.ContinueWith(DoOnThird, TaskContinuationOptions.OnlyOnRanToCompletion);
            //С помощью значений из перечисления TaskContinuationOptions можно определить,
            //что задача продолжения должна запускаться, только если порождающая задача завершилась
            //успешно (или же неудачно). В число возможных значений входят OnlyOnFaulted,
            //NotOnFaulted, OnlyOnCanceled, NotOnCanceled и OnlyOnRanToCompletion
            Task t5 = t1.ContinueWith(DoOnError, TaskContinuationOptions.OnlyOnFaulted);
            t1.Start();
            Thread.Sleep(5000);
        }

        static void DoOnFirst()
        {
            ////Эмулируем ошибку
            //int two = 0;
            //int i = 3 / two;
            ////Эмулируем ошибку

            Console.WriteLine($"{nameof(DoOnFirst)} doing some task {Task.CurrentId}");
            Thread.Sleep(3000);
        }

        static void DoOnSecond(Task t)
        {
            lock (taskMethodLock)
            {
                Console.WriteLine($"\n\ntask {t.Id} finished");
                Console.WriteLine($"{nameof(DoOnSecond)} this task id {Task.CurrentId}");
                Console.WriteLine("do some cleanup");
            }
            Thread.Sleep(3000);
        }

        static void DoOnThird(Task t)
        {
            lock (taskMethodLock)
            {
                Console.WriteLine($"\n\ntask {t.Id} finished");
                Console.WriteLine($"{nameof(DoOnThird)} this task id {Task.CurrentId}");
                Console.WriteLine("do some cleanup");
            }
            Thread.Sleep(3000);
        }

        static void DoOnError(Task t)
        {
            lock (taskMethodLock)
            {
                Console.WriteLine($"\ntask {t.Id} had an error!");
                Console.WriteLine($"{nameof(DoOnError)} this task id {Task.CurrentId}");
                Console.WriteLine("do some cleanup");
            }
            Thread.Sleep(3000);
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


        //7 Отмена задачи 
        static void CancelTask()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() => Console.WriteLine("*** task cancelled"));

            ////Выполнение без прерывания
            ////send a cancel after 500 ms
            //cts.CancelAfter(500); //Прерывание выполнения 

            Task t1 = Task.Run(() =>
            {
                Console.WriteLine("in task");
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100);
                    CancellationToken token = cts.Token;
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("cancelling was requested, cancelling from within the task");
                        token.ThrowIfCancellationRequested();
                        break;
                    }
                    Console.WriteLine($"in loop {i}");
                }
                Console.WriteLine("task finished without cancellation");
            }, cts.Token);

            try
            {
                t1.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"exception: {ex.GetType().Name}, {ex.Message}");
                foreach (var innerException in ex.InnerExceptions)
                {
                    Console.WriteLine($"inner excepion: {ex.InnerException?.GetType().Name}, {ex.InnerException.Message}");
                }
            }
        }
    }
}