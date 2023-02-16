using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CalcRunner
{
    public partial class CalcRunner : Form
    {
        //string appName = "calc.exe";

        string appName = "notepad.exe";
        public CalcRunner()
        {
            InitializeComponent();
        }


        // запуск додатку по натисканню кнопки Start
        private void Start_Click(object sender, EventArgs e)
        {
            try // блок використовується на випадок коли додаток з вказаним ім'ям не існує (в т.ч. помилка оператора)
            {
                // створення екземпляра класу ProcessStartInfo,
                // якому в параметрах передається назва додатку, який потрібно запустити
                myProcess.StartInfo = new ProcessStartInfo(appName);

                // запуск додатку
                myProcess.Start();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Ошибка");
            }
        }


        // завершення роботи додатку по натисканню кнопки Stop
        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                // отримання колекції процесів,
                // що відповідають назві запущеного процесу (може бути безліч) та запущені на даний момент
                var listProcess = Process.GetProcessesByName(myProcess.ProcessName);


                //var listProcess = Process.GetProcessesByName("win32calc");
                //var listProcess = Process.GetProcessesByName("Calculator");

                // Мій варіант - використовується виключно у разі запуску калькулятора
                //var listProcess = Process.GetProcessesByName("CalculatorApp");


                // надання елементу myProcess значення останнього процесу з отриманої колекції процесів,
                // що запущені на даний момент
                // дана конструкція використовується, оскільки можуть бути одноіменні додатки,
                // запущені до та під час роботи даного застосунку
                myProcess = listProcess[listProcess.Length - 1];

                myProcess.CloseMainWindow(); // ініціює закриття головного вікна процесу
                myProcess.Kill(); // одразу припиняє виконання процесу
                myProcess.Close(); // звільняє усі ресурси, які використовувалися асоційованим процесом
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Ошибка");
            }
        }
    }
}
