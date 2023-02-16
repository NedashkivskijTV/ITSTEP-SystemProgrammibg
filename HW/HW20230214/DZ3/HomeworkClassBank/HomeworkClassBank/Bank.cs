using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeworkClassBank
{
    internal class Bank
    {
        private int Money;
        private string Name;
        private int Percent;

        private int TimeToSleep = 1000;

        public Bank() : this(0, "noNameBank", 0) { }

        public Bank(int money, string name, int percent)
        {
            Money = money;
            Name = name;
            Percent = percent;
        }

        public void SetName(string newName)
        {
            Name = newName;
            SaveNewInfo();
        }

        public void SetMoney(int money)
        {
            Money = money;
            SaveNewInfo();
        }

        public void SetPercent(int percent)
        {
            Percent = percent;
            SaveNewInfo();
        }

        public override string ToString()
        {
            return $"Bank info: \n\tBank name : {Name} \n\tmoney ($) : {Money} \n\tpercent (%) : {Percent}";
        }

        public string StringForSaving()
        {
            return $"Bank name : {Name}; money ($) : {Money}; percent (%) : {Percent}";
        }

        private void SaveNewInfo()
        {
            Thread myThread = new Thread(new ThreadStart(SaveClassInfoToFile));
            myThread.Start();

            // Призупинення потоку - інакше генерується помилка доступу до файлу (використовується іншим/попереднім потоком)
            Thread.Sleep(TimeToSleep);
        }

        private void SaveClassInfoToFile()
        {
            string fileName = "C:\\Temp\\BankInfo.txt";
            string newInfo = $"Bank info: {this.StringForSaving()}\n";

            Console.WriteLine("Saving new info to file, please wait");

            // перехват на випадок відсутності доступу до каталога/файла або відсутності самого каталога (в т.ч. через помилку оператора)
            try
            {
                if (!File.Exists(fileName))
                {
                    File.WriteAllText(fileName, newInfo);
                }
                else
                {
                    File.AppendAllText(fileName, newInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            //Console.WriteLine("Save finished");
        }
    }
}
