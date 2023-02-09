using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CalcRunner
{
    public partial class CalcRunner : Form
    {

        string appName = "notepad.exe";

        public CalcRunner()
        {
            InitializeComponent();
        }

        // Метод для запуску процеса
        private void Start_Process()
        {
            myProcess.StartInfo = new ProcessStartInfo(appName);
            myProcess.Start();
        }

        // Старт процесу та очікування на його закриття (з виведенням коду завершення)
        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                Start_Process();

                myProcess.WaitForExit();
                MessageBox.Show($"Процес завершився с кодом: " + myProcess.ExitCode, "Код завершення процесу");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Помилка");
            }
        }

        // Запуск процесу (закриватиметься окремою кнопкою)
        private void StartAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                Start_Process();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Помилка");
            }

        }

        // Закриття процесу по натисканню кнопки
        private void Close_Click(object sender, EventArgs e)
        {
            try
            {
                myProcess.CloseMainWindow();
                myProcess.Kill();
                myProcess.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Помилка");
            }

        }

    }
}
