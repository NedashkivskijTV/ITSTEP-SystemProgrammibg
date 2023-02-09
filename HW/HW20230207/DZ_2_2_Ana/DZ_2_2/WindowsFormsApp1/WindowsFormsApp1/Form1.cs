using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string appName = "notepad.exe";

        List<Process> Processes = new List<Process>();

        public Form1()
        {
            InitializeComponent();
        }

        //функція, яка запускає дочірній процес
        void RunProcess(string AssamblyName)
        {
            try
            {
                Process proc = Process.Start(AssamblyName);
                Processes.Add(proc);
                proc.EnableRaisingEvents = true;
                proc.Exited += proc_Exited;
            }
            catch 
            {
            }
        }

        //обробник, на завершення дочірнього процесу
        void proc_Exited(object sender, EventArgs e)
        {
            Process proc = sender as Process;
            proc.WaitForExit();
            UpdateInformation("Process stop with code : " + proc.ExitCode);
        }

        //функція, яка відповідає за оновлення інформації в label
        public void UpdateInformation(string message)
        {
            if (label1.InvokeRequired)
            {
                label1.Invoke((Action<string>)UpdateInformation, message);
            }
            else
            {
                label1.Text = message;
            }
        }

        //подія, яка відбувається після натиску на кнопку start
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            RunProcess(appName);
        }

        //подія, яка відбувається після натиску на кнопку stop
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Process proc = Processes[Processes.Count - 1];
                proc.CloseMainWindow();
                proc.Kill();
                proc.Close();
                Processes.Remove(proc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
