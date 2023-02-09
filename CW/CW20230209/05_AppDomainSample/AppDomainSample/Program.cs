using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace UsingAppDomains
{

    static class Program
    {
        static AppDomain TextDrawer;    //будет хранить объект домена приложения TextDrawer
        static AppDomain TextWindow;    //будет хранить объект домена приложения TextWindow
        static Assembly DrawerAsm;      //будет хранить объект сборки TextDrawer.exe
        static Assembly TextWindowAsm;  //будет хранить объект сборки TextWindow.exe
        static Form DrawerWindow;       //будет хранить объект окна TextDrawer
        static Form TextWindowWnd;      //будет хранить объект окна TextWindow

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [LoaderOptimization(LoaderOptimization.MultiDomain)]
        static void Main()
        {
            try
            {
                /*создаём необходимые домены приложений с дружественными именами и 
                 сохраняем ссылки на них в соответствующие переменные*/
                TextDrawer = AppDomain.CreateDomain("TextDrawer");
                TextWindow = AppDomain.CreateDomain("TextWindow");

                /*добавляем обработчик события DomainUnload*/
                TextDrawer.DomainUnload += TextDrawer_DomainUnload;

                /*добавляем обработчик события DomainUnload*/
                TextWindow.DomainUnload += TextWindow_DomainUnload;

                /*загружаем сборки с оконными приложениями в соответствующие домены приложений*/
                DrawerAsm = TextDrawer.Load(AssemblyName.GetAssemblyName("TextDrawer.exe"));
                TextWindowAsm = TextWindow.Load(AssemblyName.GetAssemblyName("TextWindow.exe"));

                /*создаём объекты окон на сонове оконных типов данных из загруженных сборок*/
                DrawerWindow = Activator.CreateInstance(DrawerAsm.GetType("TextDrawer.TextDrawerForm")) as Form;
                TextWindowWnd = Activator.CreateInstance(TextWindowAsm.GetType("TextWindow.TextWindowForm"),
                    new object[]
                        {
                        DrawerAsm.GetModule("TextDrawer.exe"),
                        DrawerWindow
                        }) as Form;

                /*запускаем потоки*/
                new Thread(RunVisualizer).Start();
                new Thread(RunDrawer).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void TextDrawer_DomainUnload(object sender, EventArgs e)
        {
            /*открываем MessageBox в котором выводим имя текущего домена приложения*/
            MessageBox.Show("Domain with name " + (sender as AppDomain).FriendlyName + " has been succesfully unloaded!");
        }

        static void TextWindow_DomainUnload(object sender, EventArgs e)
        {
            /*открываем MessageBox в котором выводим имя текущего домена приложения*/
            MessageBox.Show("Domain with name " + (sender as AppDomain).FriendlyName + " has been succesfully unloaded!");
        }

        static void RunDrawer()
        {
            /*запускаем окно модально в текущем потоке*/
            DrawerWindow.ShowDialog();
            TextWindowWnd.Close();
            /*отгружаем домен приложения*/
            AppDomain.Unload(TextDrawer);
        }

        static void RunVisualizer()
        {
            /*запускаем окно модально в текущем потоке*/
            TextWindowWnd.ShowDialog();
            /*отгружаем домен приложения*/
            AppDomain.Unload(TextWindow);

            /*завершаем работу приложения, следствием чего станет закрытие потока*/
            Application.Exit();
        }
    }
}