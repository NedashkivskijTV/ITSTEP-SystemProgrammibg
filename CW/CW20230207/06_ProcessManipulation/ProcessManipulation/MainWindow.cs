using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProcessManipulation
{
    public partial class MainWindow : Form
    {
        //константа, идентифицирующая сообщение WM_SETTEXT
        const uint WM_SETTEXT = 0x0C;
        
        //импортируем функцию SendMEssage из библиотеки user32.dll
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, int wParam, [MarshalAs(UnmanagedType.LPStr)]string lParam);
        
        /*список, в котором будут храниться объекты, 
          описывающие дочерние процессы приложения*/
        List<Process> Processes = new List<Process>();
        
        /*счётчик запущенных процессов*/
        int Counter = 0;
        
        public MainWindow()
        {
            InitializeComponent();

            LoadAvailableAssemblies();
        }
        
        /*метод, загружающий доступные исполняемые 
         файлы из домашней директории проекта*/
        void LoadAvailableAssemblies()
        {
            //название файла сборки текущего приложения
            string except = new FileInfo(Application.ExecutablePath).Name;
            //получаем название файла без расширения
            except =  except.Substring(0, except.IndexOf("."));
            //получаем все *.exe файлы из домашней директории
            string[] files = Directory.GetFiles(Application.StartupPath, "*.exe");
            foreach (var file in files)
            {
                //получаем имя файла
                string fileName = new FileInfo(file).Name;
                /*если имя файла не содержит имени исполняемого файла проекта, то оно добавляется в список*/
                if (fileName.IndexOf(except) == -1)
                    AvailableAssemblies.Items.Add(fileName);
            }
        }
        
        /*метод, запускающий процесс на исполнение и 
         сохраняющий объект, который его описывает*/
        void RunProcess(string AssamblyName)
        {
            try
            {
                //запускаем процесс на основании исполняемого файла
                Process proc = Process.Start(AssamblyName);
                //добавляем процесс в список
                Processes.Add(proc);
                /*проверяем, стал ли созданный процесс дочерним, 
                 по отношению к текущему и, если стал, выводим MessageBox*/
                if (Process.GetCurrentProcess().Id == GetParentProcessId(proc.Id))
                    MessageBox.Show(proc.ProcessName + " действительно дочерний процесс текущего процесса!");
                /*указываем, что процесс должен генерировать собития*/
                proc.EnableRaisingEvents = true;
                //добавляем обработчик на событие завершения процесса
                proc.Exited += proc_Exited;
                /*устанавливаем новый текст главному окну дочернего процесса*/
                SetChildWindowText(proc.MainWindowHandle, "Childs process #" + (++Counter));
                /*проверяем, запускали ли мы экземпляр такого приложения 
                 и, если нет, то добавляем в список запущенных приложений*/
                if (!StartedAssemblies.Items.Contains(proc.ProcessName))
                    StartedAssemblies.Items.Add(proc.ProcessName);
                /*убираем приложение из списка доступных приложений*/
                if (AssamblyName == AvailableAssemblies.SelectedItem.ToString())
                    AvailableAssemblies.Items.Remove(AvailableAssemblies.SelectedItem);
            }
            catch 
            {
            }
        }
        
        /*метод обёртывания для отправки сообщения WM_SETTEXT*/
        void SetChildWindowText(IntPtr Handle,string text)
        {
            SendMessage(Handle, WM_SETTEXT, 0, text);
        }
        
        /*метод, получающий PID родительского процесса (использует WMI)*/
        int GetParentProcessId(int Id)
        {
            int parentId = 0;
            using (ManagementObject obj = new ManagementObject("win32_process.handle=" + Id.ToString()))
            {
                try
                {
                    obj.Get();
                    parentId = Convert.ToInt32(obj["ParentProcessId"]);
                }
                catch
                {
                }
            }
            return parentId;
        }
        
        /*обработчик события Exited класса Process*/
        void proc_Exited(object sender, EventArgs e)
        {
            Process proc = sender as Process;
            //убираем процесс из списка запущенных приложений
            StartedAssemblies.Items.Remove(proc.ProcessName);
            //добавляем процесс в список доступных приложений
            AvailableAssemblies.Items.Add(proc.ProcessName);
            //убираем процесс из списка дочерних процессов
            Processes.Remove(proc);
            //уменьшаем счётчик дочерних процессов на 1
            Counter--;
            int index = 0;
            /*меняем текст для главных окон всех дочерних процессов*/
            foreach (var p in Processes)
                SetChildWindowText(p.MainWindowHandle, "Child process #" + ++index);
        }
        
        //объявление делегата, принимающего параметр типа Process
        delegate void ProcessDelegate(Process proc);

        /*метод, который выполняет проход по всем дочерним процессам с заданым 
         именем и выполняющий для этих процессов заданый делегатом метод*/
        void ExecuteOnProcessesByName(string ProcessName, ProcessDelegate func)
        {
            /*оплучаем список, запущенных в операционной системе процессов*/
            Process[] processes = Process.GetProcessesByName(ProcessName);
            foreach (var process in processes)
                /*если PID родительского процесса равен PID текущего процесса*/
                if(Process.GetCurrentProcess().Id == GetParentProcessId(process.Id))
                    //запускаем метод
                    func(process);
        }

        /*обработчик события нажания на кнопку Start основного диалога*/
        private void StartButton_Click(object sender, EventArgs e)
        {
            RunProcess(AvailableAssemblies.SelectedItem.ToString());
        }

        void Kill(Process proc)
        {
            proc.Kill();
        }
        
        /*обработчик события нажания на кнопку Stop основного диалога*/
        private void StopButton_Click(object sender, EventArgs e)
        {
            ExecuteOnProcessesByName(StartedAssemblies.SelectedItem.ToString(), Kill);
            StartedAssemblies.Items.Remove(StartedAssemblies.SelectedItem);
        }

        void CloseMainWindow(Process proc)
        {
            proc.CloseMainWindow();
        }
        
        /*обработчик события нажания на кнопку Close основного диалога*/
        private void CloseWindowButton_Click(object sender, EventArgs e)
        {
            ExecuteOnProcessesByName(StartedAssemblies.SelectedItem.ToString(), CloseMainWindow);
            StartedAssemblies.Items.Remove(StartedAssemblies.SelectedItem);
        }

        void Refresh(Process proc)
        {
            proc.Refresh();
        }
        
        /*обработчик события нажания на кнопку Refresh основного диалога*/
        private void Refresh_Click(object sender, EventArgs e)
        {
            ExecuteOnProcessesByName(StartedAssemblies.SelectedItem.ToString(), Refresh);
        }
        
        /*обработчик события изменения индекса выделенного элемента 
         в списке доступных приложений*/
        private void AvailableAssemblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AvailableAssemblies.SelectedItems.Count == 0)
                StartButton.Enabled = false;
            else
                StartButton.Enabled = true;
        }
        
        /*обработчик события изменения индекса выделенного элемента 
         в списке запущенных приложений*/
        private void StartedAssemblies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StartedAssemblies.SelectedItems.Count == 0)
            {
                StopButton.Enabled = false;
                CloseButton.Enabled = false;
                CloseWindowButton.Enabled = false;
            }
            else
            {
                StopButton.Enabled = true;
                CloseButton.Enabled = true;
                CloseWindowButton.Enabled = true;
            }
        }
        
        /*обработчик события зактырия основного окна приложения*/
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var proc in Processes)
                proc.Kill();
        }
        
        /*обработчик собитыя нажатия на кнопку "Run Calc"*/
        private void RunCalc_Click(object sender, EventArgs e)
        {
            RunProcess("notepad.exe");
        }
    }
}
