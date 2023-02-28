using System;
using System.Threading;

namespace RandomNumbers
{
    class Program
    {

        static void Main(string[] args)
        {
            RandomNumbersOperations randomNumbersOperations = new RandomNumbersOperations();

            Console.WriteLine("\nSTART Thread - " + Thread.CurrentThread.ManagedThreadId);

            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(randomNumbersOperations.Actions);

            Thread thread1 = new Thread(threadStart);
            thread1.Start((object)"1");
            thread1.Join();

            Thread thread2 = new Thread(threadStart);
            thread2.Start((object)"2");
            thread2.Join();

            Thread thread3 = new Thread(threadStart);
            thread3.Start((object)"3");
            thread3.Join();

            Console.WriteLine("\nSTOP Thread - " + Thread.CurrentThread.ManagedThreadId);
        }
    }

    class RandomNumbersOperations
    {
        Mutex mutexObj;
        string catalogName = "C:\\Temp\\";
        string fileName1 = "1-RandomNumbers.txt";
        string fileName2 = "2-PrimeRandomNumbers.txt";
        string fileName3 = "3-PrimeRandomNumbersEndingIn7.txt";

        Random random = new Random();
        int numberMin = 0;
        int numberMax = 100;
        int numbersAmount = 50;
        int numberEnding = 7;

        public RandomNumbersOperations()
        {
            mutexObj = new Mutex(false, "SYNC_FILE");
        }

        public void Actions(object methodIndex) // метод перемикач (в залежності від параметра викликає один з методів)
        {
            mutexObj.WaitOne();
            string indexAction = (string)methodIndex;
            switch (indexAction)
            {
                case "1":
                    {
                        CreateRandomNumbers();
                    }
                    break;
                case "2":
                    {
                        PrimeNumbers();
                    }
                    break;
                case "3":
                    {
                        PrimeNumbersEndingIn7();
                    }
                    break;
            }
            mutexObj.ReleaseMutex();
        }

        private void CreateRandomNumbers() // наповнення 1-го файлу випадковими числами
        {
            Console.WriteLine("\nSTART Thread - " + Thread.CurrentThread.ManagedThreadId);

            int[] numbers = new int[numbersAmount];
            string numbersInString = "";

            for (int i = 0; i < numbersAmount; i++)
            {
                int n = random.Next(numberMin, numberMax + 1);
                numbers[i] = n;
            }

            numbersInString = String.Join(" ", numbers);

            SaveToFile(catalogName + fileName1, numbersInString);

            Console.WriteLine("STOP Thread - " + Thread.CurrentThread.ManagedThreadId);
        }

        private void PrimeNumbers() // читання 1-го файлу та наповнення 2-го файлу простими числами
        {
            Console.WriteLine("\nSTART Thread - " + Thread.CurrentThread.ManagedThreadId);

            try
            {
                string str = ReadFromFile(catalogName + fileName1);

                string[] numbers = str.Split(" ");
                string primeNumbersInString = "";
                int number = 0;

                foreach (string n in numbers)
                {
                    number = int.Parse(n);

                    if (IsPrimeNumber(number))
                    {
                        primeNumbersInString = primeNumbersInString + number + " ";
                    }
                }

                SaveToFile(catalogName + fileName2, primeNumbersInString);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR : " + e.Message);
            }

            Console.WriteLine("STOP Thread - " + Thread.CurrentThread.ManagedThreadId);
        }

        private void PrimeNumbersEndingIn7() // читання 2-го файлу та наповнення 3-го файлу простими числами, що закінчуються на 7
        {
            Console.WriteLine("\nSTART Thread - " + Thread.CurrentThread.ManagedThreadId);

            try
            {
                string str = ReadFromFile(catalogName + fileName2);

                string[] numbers = str.Split(" ");
                string primeNumbersEnding7InString = "";

                foreach (string n in numbers)
                {
                    if (n.EndsWith(numberEnding.ToString()))
                    {
                        primeNumbersEnding7InString = primeNumbersEnding7InString + n + " ";
                    }
                }

                SaveToFile(catalogName + fileName3, primeNumbersEnding7InString);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR : " + e.Message);
            }

            Console.WriteLine("STOP Thread - " + Thread.CurrentThread.ManagedThreadId);
        }


        private void SaveToFile(string fileName, string text) // збереження рядка у файл
        {
            try
            {
                Console.WriteLine($"\tSaving new info to file {fileName}, please wait");

                File.WriteAllText(fileName, text); // запис данх у файл

                Console.WriteLine("\tSaving finished");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        private string ReadFromFile(string fileName) // читання рядка з файлу
        {

            string str = "";

            if (File.Exists(fileName)) // перевірка наявності доступу до файлу
            {
                Console.WriteLine($"\tReading from file {fileName}, please wait");

                str = File.ReadAllText(fileName); // завантаження даних

                Console.WriteLine("\tReading finished");
            }
            else
            {
                throw new FileNotFoundException("File not found");
            }

            return str;
        }

        private bool IsPrimeNumber(int number) // перевірка чи є число простим
        {
            if (number < 2)
            {
                return false;
            }

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
