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

        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                myProcess.StartInfo = new ProcessStartInfo(appName);
                myProcess.Start();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Ошибка");
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                var listProcess = Process.GetProcessesByName(myProcess.ProcessName);

                //var listProcess = Process.GetProcessesByName("win32calc");
                //var listProcess = Process.GetProcessesByName("Calculator");
                
                // Мій варіант
                //var listProcess = Process.GetProcessesByName("CalculatorApp");


                myProcess = listProcess[listProcess.Length - 1];
                myProcess.CloseMainWindow();
                myProcess.Kill();
                myProcess.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Ошибка");
            }
        }
    }
}
